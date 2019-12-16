using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        List<ProductOrder> selectedProdOrderRequests = new List<ProductOrder>();
        ProductOrder orderToManage;
        private void tabOrders_Enter(object sender, EventArgs e)
        {
            string status;

            DataTable dtProductOrderRequests = GlobalConfig.Connection.GetProductOrderRequestsDataTable(out status);

            dgvOrderRequests.DataSource = dtProductOrderRequests;

            DataTable dtPendingProductOrders = GlobalConfig.Connection.GetPendingProductOrdersDataTable(out status);

            dgvPendingOrders.DataSource = dtPendingProductOrders;
        }

        private void dgvOrderRequests_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvOrderRequests.Columns["id"].Visible = false;
        }

        private void dgvPendingOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPendingOrders.Columns["id"].Visible = false;
        }

        private void BtnOrderSelected_Click(object sender, EventArgs e)
        {
            selectedProdOrderRequests = new List<ProductOrder>();
            foreach (DataGridViewRow row in dgvOrderRequests.SelectedRows)
            {
                string status;
                int selectedID = (int)row.Cells["id"].Value;
                ProductOrder selectedRequest = GlobalConfig.Connection.GetProductOrderByID(selectedID, out status);
                //BindingList<OrderLine> prodOrderLines = GlobalConfig.Connection.GetOrderLinesByProdOrdID(selectedRequest.ID, out status);
                //selectedRequest.orderLines = prodOrderLines;
                selectedProdOrderRequests.Add(selectedRequest);
            }

            savePreviousTab();
            mainView.SelectedTab = subtabNewOrder;
        }

        private void BtnManageOrder_Click(object sender, EventArgs e)
        {
            int orderToManageID = (int)dgvPendingOrders.SelectedRows[0].Cells["id"].Value;
            string status;

            orderToManage = GlobalConfig.Connection.GetProductOrderByID(orderToManageID, out status);

            savePreviousTab();
            mainView.SelectedTab = subtabManagePartOrder;
        }
    }
}
