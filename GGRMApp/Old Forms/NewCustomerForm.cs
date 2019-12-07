using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GGRMLib;
using GGRMLib.Models;

//Coded by: Cooper Keddy & Macklem Curtis
//Date: Nov 2019

namespace GGRMApp
{
    public partial class NewCustomerForm : Form
    {
        public NewCustomerForm()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string validStatus = "";
            Regex pv = new Regex(@"^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$");
            Regex ev = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Regex phv = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");

            //check if fields are empty
            if (txtFirstName.Text == ""
                || txtLastName.Text == ""
                || txtEmail.Text == ""
                || txtPhone.Text == ""
                || txtAddress.Text == ""
                || txtCity.Text == ""
                || txtPostal.Text == "")
            {
                validStatus = "All required fields must be entered.";
                valid = false;
            }

            //phone validation
            Match phoneMatch = phv.Match(txtPhone.Text);
            if (!phoneMatch.Success) {
                valid = false;
                validStatus = "Phone number must be a 10-digit numeric value";
            }

            //postal validation
            Match postalMatch = pv.Match(txtPostal.Text);
            if(!postalMatch.Success)
            {
                validStatus = "Must be a valid postal code";
                valid = false;
            }

            //email validation
            Match emailMatch = ev.Match(txtEmail.Text);
            if(txtEmail.Text != "")
            {
                if(!emailMatch.Success)
                {
                    validStatus = "Must be a valid email address";
                    valid = false;
                }
            }

            if (valid)
            {
                string status;
                Customer cust = new Customer(
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtPhone.Text,
                    txtAddress.Text,
                    txtCity.Text,
                    txtPostal.Text,
                    txtEmail.Text);
                GlobalConfig.Connection.CreateCustomer(cust, out status);
                MessageBox.Show(status, "Database Result");
            } else
            {
                MessageBox.Show(validStatus);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
