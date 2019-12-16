using GGRMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    //Coded by: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        private void subtabEditInventory_Enter(object sender, EventArgs e)
        {
            txtEditItemQuantity.Text = invToEdit.InvQuantity.ToString();
            txtEditItemPrice.Text = invToEdit.InvPrice.ToString("0.00");
            lblDisplayItemName.Text = invToEdit.DisplayName;
        }

        private void btnEditInventoryConfirm_Click(object sender, EventArgs e)
        {
            invToEdit.InvQuantity = int.Parse(txtEditItemQuantity.Text);
            invToEdit.InvPrice = decimal.Parse(txtEditItemPrice.Text);

            string status;
            GlobalConfig.Connection.EditInventoryItem(invToEdit, out status);
            lblEditItemStatus.Text = status;
        }
    }
}
