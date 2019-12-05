using GGRMLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {

        private void deselectDataButtons()
        {
            foreach (Button b in dataButtons)
            {
                b.BackColor = Color.FromArgb(255, 48, 48, 48);
            }
        }
        private void BtnDataInventory_Click(object sender, EventArgs e)
        {
            deselectDataButtons();
            btnDataInventory.BackColor = Color.FromArgb(255, 90, 90, 90);
            tcDataView.SelectedTab = tabDataInventory;
            tcDataView.Focus();
        }
        private void BtnDataProducts_Click(object sender, EventArgs e)
        {
            deselectDataButtons();
            btnDataProducts.BackColor = Color.FromArgb(255, 90, 90, 90);
            tcDataView.SelectedTab = tabDataProducts;
            tcDataView.Focus();
        }
        private void BtnDataCustomers_Click(object sender, EventArgs e)
        {
            deselectDataButtons();
            btnDataCustomers.BackColor = Color.FromArgb(255, 90, 90, 90);
            tcDataView.SelectedTab = tabDataCustomers;
            tcDataView.Focus();
        }
        private void BtnDataOrders_Click(object sender, EventArgs e)
        {
            deselectDataButtons();
            btnDataOrders.BackColor = Color.FromArgb(255, 90, 90, 90);
            tcDataView.SelectedTab = tabDataOrders;
            tcDataView.Focus();
        }


        //Inventory Tab
        private void TabDataInventory_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventory = GlobalConfig.Connection.GetInventoryDataTable(out status);
            dgvInventoryList.DataSource = dtInventory;

            // Set price column to currency format
            dgvInventoryList.Columns[6].DefaultCellStyle.Format = "c";
        }
    }
}
