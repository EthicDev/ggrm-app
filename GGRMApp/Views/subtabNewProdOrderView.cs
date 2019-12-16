using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void subtabNewOrder_Enter(object sender, EventArgs e)
        {
            int numSelected = selectedProdOrderRequests.Count;
            int[] ids = new int[numSelected];
            for (int i = 0; i < numSelected; i++)
            {
                ids[i] = selectedProdOrderRequests[i].ID;
            }
            string message = "";
            foreach (int i in ids)
            {
                message += i.ToString() + " ";
            }
            string status;
            DataTable dtSelectedProdOrderItems = GlobalConfig.Connection.GetOrderLinesByProdOrdIDs(ids, out status);
            lblNewOrderStatus.Text = status;
            dgvNewOrderItems.DataSource = dtSelectedProdOrderItems;
        }

        private void dgvNewOrderItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvNewOrderItems.ReadOnly = false;
            dgvNewOrderItems.Columns["id"].Visible = false;
            dgvNewOrderItems.Columns["orlPrice"].Visible = false;
            dgvNewOrderItems.Columns["inventoryID"].Visible = false;
            dgvNewOrderItems.Columns["custOrdID"].Visible = false;
            dgvNewOrderItems.Columns["prodOrdID"].Visible = false;
            dgvNewOrderItems.Columns["serOrdID"].Visible = false;
            foreach (DataGridViewColumn col in dgvNewOrderItems.Columns)
            {
                col.ReadOnly = true;
            }
            dgvNewOrderItems.Columns["orlSupplierQuantityOrdered"].ReadOnly = false;
        }

        private void BtnNewOrderCreate_Click(object sender, EventArgs e)
        {
            bool createOrder = true;
            string status = "Order creation failed.";
            foreach (DataGridViewRow row in dgvNewOrderItems.Rows)
            {
                var quantity = row.Cells["orlSupplierQuantityOrdered"].Value;
                if (quantity == null)
                {
                    createOrder = false;
                    break;
                } else if ((int)quantity < 1)
                {
                    createOrder = false;
                    break;
                }
            }

            if (!createOrder)
            {
                status = "Invalid Supplier Quantity Ordered provided for one or more lines.";
            }
            else
            {
                foreach (DataGridViewRow row in dgvNewOrderItems.Rows)
                {
                    int newQuantity = (int)row.Cells["orlSupplierQuantityOrdered"].Value;
                    int lineID = (int)row.Cells["id"].Value;
                    GlobalConfig.Connection.EditOrderLineSupplierQuantity(lineID, newQuantity, out status);
                }
                foreach(ProductOrder po in selectedProdOrderRequests)
                {
                    po.PordStatus = "Ordered";
                    po.PordSupplierOrderNum = txtNewOrderID.Text;
                    po.PordDateOrdered = DateTime.UtcNow;
                    po.OrderingEmpID = currentUser.ID;
                    GlobalConfig.Connection.EditProductOrder(po, out status);
                }
            }
            lblNewOrderStatus.Text = status;
        }

    }
}
