using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void NewCustomerClearInfo()
        {
            txtNewCustomerAddress.Text = "";
            txtNewCustomerCity.Text = "";
            txtNewCustomerEmail.Text = "";
            txtNewCustomerFirst.Text = "";
            txtNewCustomerLast.Text = "";
            txtNewCustomerPhone.Text = "";
            txtNewCustomerPostal.Text = "";
        }
        private void SubtabNewCustomer_Leave(object sender, EventArgs e)
        {
            NewCustomerClearInfo();
        }
        private void TlpCustomerInfoEntry_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 48, 48, 48)), e.CellBounds);
        }
        private void BtnCreateCustomer_Click(object sender, EventArgs e)
        {
            NewCustomerClearInfo();
        }
    }
}
