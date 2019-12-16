using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    //Coded by: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
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
            bool valid = true;
            string validStatus = "";
            Regex pv = new Regex(@"^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$");
            Regex ev = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Regex phv = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");

            //check if fields are empty
            if (txtNewCustomerFirst.Text == ""
                || txtNewCustomerLast.Text == ""
                || txtNewCustomerEmail.Text == ""
                || txtNewCustomerPhone.Text == ""
                || txtNewCustomerAddress.Text == ""
                || txtNewCustomerCity.Text == ""
                || txtNewCustomerPostal.Text == "")
            {
                validStatus = "All required fields must be entered.";
                valid = false;
            }

            //phone validation
            Match phoneMatch = phv.Match(txtNewCustomerPhone.Text);
            if (!phoneMatch.Success)
            {
                valid = false;
                validStatus = "Phone number must be a 10-digit numeric value";
            }

            //postal validation
            Match postalMatch = pv.Match(txtNewCustomerPostal.Text);
            if (!postalMatch.Success)
            {
                validStatus = "Must be a valid postal code";
                valid = false;
            }

            //email validation
            Match emailMatch = ev.Match(txtNewCustomerEmail.Text);
            if (txtNewCustomerEmail.Text != "")
            {
                if (!emailMatch.Success)
                {
                    validStatus = "Must be a valid email address";
                    valid = false;
                }
            }

            if (valid)
            {
                string status;
                Customer cust = new Customer(
                    txtNewCustomerFirst.Text,
                    txtNewCustomerLast.Text,
                    txtNewCustomerPhone.Text,
                    txtNewCustomerAddress.Text,
                    txtNewCustomerCity.Text,
                    txtNewCustomerPostal.Text,
                    txtNewCustomerEmail.Text);
                GlobalConfig.Connection.CreateCustomer(cust, out status);
                if (status == "Customer insertion succeeded.")
                {
                    NewCustomerClearInfo();
                }
                lblNewCustomerStatus.Text = status;
            } else
            {
                lblNewCustomerStatus.Text = validStatus;
            }
        }
    }
}
