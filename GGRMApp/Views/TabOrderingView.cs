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
    }
}
