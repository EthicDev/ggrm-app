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
        }



    }
}
