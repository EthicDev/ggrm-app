using GGRMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void subtabEditInventory_Enter(object sender, EventArgs e)
        {
            txtEditItemQuantity.Text = selectedInventory.InvQuantity.ToString();
            txtEditItemPrice.Text = selectedInventory.InvPrice.ToString("0.00");
            lblDisplayItemName.Text = selectedInventory.DisplayName;
        }

        private void btnEditInventoryConfirm_Click(object sender, EventArgs e)
        {
            selectedInventory.InvQuantity = int.Parse(txtEditItemQuantity.Text);
            selectedInventory.InvPrice = decimal.Parse(txtEditItemPrice.Text);

            string status;
            GlobalConfig.Connection.EditInventoryItem(selectedInventory, out status);
            lblEditItemStatus.Text = status;
        }
    }
}
