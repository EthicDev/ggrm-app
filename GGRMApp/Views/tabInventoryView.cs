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
        Inventory selectedInventory = new Inventory();
        private void tabInventory_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventory = GlobalConfig.Connection.GetInventoryDataTable(out status);
            dgvInventoryListOld.DataSource = dtInventory;

            // Set price column to currency format
            dgvInventoryListOld.Columns[6].DefaultCellStyle.Format = "c";
        }
        private void BtnEditItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvDataInventory.SelectedRows[0];
            selectedInventory.ID = (int)selectedRow.Cells["id"].Value;
            selectedInventory.DisplayName = (string)selectedRow.Cells["Name"].Value + "\n" + (string)selectedRow.Cells["Description"].Value + "\n" + (string)selectedRow.Cells["Size"].Value;
            selectedInventory.InvQuantity = (int)selectedRow.Cells["Quantity"].Value;
            selectedInventory.InvPrice = (decimal)selectedRow.Cells["Unit Price"].Value;

            savePreviousTab();
            mainView.SelectedTab = subtabEditInventory;

        }
        private void BtnManageProducts_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabEditProducts;
        }
    }
}
