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
    //Coded By: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        Inventory invToEdit;
        private void TabDataInventory_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventory = GlobalConfig.Connection.GetInventoryDataTable(out status);
            dgvDataInventory.DataSource = dtInventory;

            // Set price column to currency format
            dgvDataInventory.Columns[6].DefaultCellStyle.Format = "c";
        }

        private void dgvDataInventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDataInventory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvDataInventory.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDataInventory.Columns["id"].Visible = false;
        }

        private void btnDataInventorySearch_Click(object sender, EventArgs e)
        {
            string status;
            string searchString = txtDataInventorySearch.Text;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventory = GlobalConfig.Connection.GetInventoryDataTable(out status, searchString);
            dgvDataInventory.DataSource = dtInventory;

            // Set price column to currency format
            dgvDataInventory.Columns[6].DefaultCellStyle.Format = "c";
        }
        private void BtnDataInventoryAdd_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewInventory;
        }

        private void BtnDataInventoryEdit_Click(object sender, EventArgs e)
        {
            string status;
            int selectedID = (int)dgvDataInventory.SelectedRows[0].Cells["id"].Value;
            invToEdit = GlobalConfig.Connection.GetInventoryByID(selectedID, out status);
            savePreviousTab();
            //MessageBox.Show(invToEdit.InvPrice.ToString());
            mainView.SelectedTab = subtabEditInventory;
        }
    }
}
