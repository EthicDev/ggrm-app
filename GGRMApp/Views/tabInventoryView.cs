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
        private void tabInventory_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventory = GlobalConfig.Connection.GetInventoryDataTable(out status);
            dgvInventoryList.DataSource = dtInventory;

            // Set price column to currency format
            dgvInventoryList.Columns[6].DefaultCellStyle.Format = "c";
        }
        private void BtnEditItem_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabEditItem;
        }
    }
}
