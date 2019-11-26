using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {

        private void btnConfirmChanges_Click(object sender, EventArgs e)
        {

            bool valid = true;
            string validStatus = "";
            Regex pv = new Regex(@"^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$");
            Regex ev = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Regex phv = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");

            //check if fields are empty
            if (txtEditCustomerFirst.Text == ""
                || txtEditCustomerLast.Text == ""
                || txtEditCustomerEmail.Text == ""
                || txtEditCustomerPhone.Text == ""
                || txtEditCustomerAddress.Text == ""
                || txtEditCustomerCity.Text == ""
                || txtEditCustomerPostal.Text == "")
            {
                validStatus = "All required fields must be entered.";
                valid = false;
            }

            //phone validation
            Match phoneMatch = phv.Match(txtEditCustomerPhone.Text);
            if (!phoneMatch.Success)
            {
                valid = false;
                validStatus = "Phone number must be a 10-digit numeric value";
            }

            //postal validation
            Match postalMatch = pv.Match(txtEditCustomerPostal.Text);
            if (!postalMatch.Success)
            {
                validStatus = "Must be a valid postal code";
                valid = false;
            }

            //email validation
            Match emailMatch = ev.Match(txtEditCustomerEmail.Text);
            if (txtEditCustomerEmail.Text != "")
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
                    txtEditCustomerFirst.Text,
                    txtEditCustomerLast.Text,
                    txtEditCustomerPhone.Text,
                    txtEditCustomerAddress.Text,
                    txtEditCustomerCity.Text,
                    txtEditCustomerPostal.Text,
                    txtEditCustomerEmail.Text);
                cust.ID = (int)GlobalData.ViewData["editSelectedCustomerID"];
                GlobalConfig.Connection.EditCustomer(cust, out status);
                if (status == "Customer update succeeded.")
                {
                    EditCustomerClearInfo();
                }
                lblEditCustomerStatus.Text = status;
            }
            else
            {
                lblEditCustomerStatus.Text = validStatus;
            }
        }

        private void EditCustomerClearInfo()
        {
            txtEditCustomerAddress.Text = "";
            txtEditCustomerCity.Text = "";
            txtEditCustomerEmail.Text = "";
            txtEditCustomerFirst.Text = "";
            txtEditCustomerLast.Text = "";
            txtEditCustomerPhone.Text = "";
            txtEditCustomerPostal.Text = "";
        }
        private void subtabEditCustomer_Leave(object sender, EventArgs e)
        {
            GlobalData.ViewData["editSelectedCustomerID"] = 0;
        }

    }
}
