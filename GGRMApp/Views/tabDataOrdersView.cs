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
    //Coded By: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        private void tabDataOrders_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtCustomerOrders = GlobalConfig.Connection.GetCustomerOrdersDataTable(out status);
            dgvDataOrders.DataSource = dtCustomerOrders;
        }

        private void dgvDataOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDataOrders.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            
            dgvDataOrders.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDataOrders.Columns["id"].Visible = false;
            dgvDataOrders.Columns["ordTotal"].DefaultCellStyle.Format = "c";
            dgvDataOrders.Columns["ordTotal"].HeaderText = "Total";
            dgvDataOrders.Columns["ordPartyPlan"].DefaultCellStyle.Format = "c";
            dgvDataOrders.Columns["ordPartyPlan"].HeaderText = "2% Plan";
        }

        private void BtnDataOrdersDetails_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabDetailsOrder;

            //orderViewing = GlobalConfig.Connection.GetCustomerOrder
        }

        private void BtnDataOrdersPay_Click(object sender, EventArgs e)
        {
            //@ add code to fill POS tab with info from the selected previous order
            mainView.SelectedTab = tabPOS;
        }

    }
}
