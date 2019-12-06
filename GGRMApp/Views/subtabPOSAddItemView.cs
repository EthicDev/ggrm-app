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
        private void subtabPOSAddItem_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventoryShort = GlobalConfig.Connection.GetInventoryDataTableShort(out status);
            dgvPOSItemLookup.DataSource = dtInventoryShort;
        }

        private void dgvPOSItemLookup_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPOSItemLookup.Columns["id"].Visible = false;
            //dgvCustomers.Columns["CustFirst"].HeaderText = "First Name";
        }

        private void btnSelectItems_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dgvPOSItemLookup.SelectedRows)
            {
                
                OrderLine col = new OrderLine();
                col.ID = posCurrentOrder.orderLines.Count + 1;
                col.OrderID = posCurrentOrder.ID;
                col.InventoryID = (int)row.Cells["id"].Value;
                col.ColQuantity = 1;
                col.ColPrice = (decimal)row.Cells["Price"].Value;
                col.ColNote = "hi";
                col.ColOrderReq = false;
                posCurrentOrder.orderLines.Add(col);
                //stomerOrderLine newCol = (CustomerOrderLine)GlobalConfig.Connection.CreateCustomerOrderLine(row.Cells["id"].Value, out status);
                //GlobalData.ViewData["posAddedInvItems"].Add(invItem);
            }

            //dgvItemCart.DataSource = typeof(List<CustomerOrderLine>);
            //dgvItemCart.DataSource = posCurrentOrder.orderLines;

            tcPOSSidebar.SelectedTab = subtabPOSButtons;
        }

        private void btnPOSItemSearch_Click(object sender, EventArgs e)
        {
            string status;
            string searchString = txtPOSItemSearch.Text;
            DataTable dtInventoryShort = GlobalConfig.Connection.GetInventoryDataTableShort(out status, searchString);
            dgvPOSItemLookup.DataSource = dtInventoryShort;
        }

    }
}
