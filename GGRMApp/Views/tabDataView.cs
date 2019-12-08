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

        //paint search bar to be dark
        private void TlpDataInventorySearch_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 1)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 48, 48, 48)), e.CellBounds);
            }
        }
    }
}
