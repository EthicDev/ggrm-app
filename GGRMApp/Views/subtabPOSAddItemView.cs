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
        private void subtabPOSAddItem_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventoryShort = GlobalConfig.Connection.GetInventoryDataTableShort(out status);
            dgvPOSItemLookup.DataSource = dtInventoryShort;
        }
    }
}
