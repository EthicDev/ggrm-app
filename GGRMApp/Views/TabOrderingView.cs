using GGRMLib;
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

        private void BtnOrderSelected_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewOrder;
        }

        private void BtnManageOrder_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabManagePartOrder;
        }

        // new order controls

        private void BtnNewOrderCreate_Click(object sender, EventArgs e)
        {

        }

        //manage order controls 
        private void BtnManagePartOrderSave_Click(object sender, EventArgs e)
        {
            // @ add code to change order status based on radio button selection.
            mainView.SelectedTab = previousTab;
        }
    }
}
