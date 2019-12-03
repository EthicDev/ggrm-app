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
        private void SubtabCustomers_Enter(object sender, EventArgs e)
        {
            string status;
            btnEditCustomer.Enabled = false;
            
            List<Customer> listCustomers = GlobalConfig.Connection.GetCustomersList(out status);
            dgvCustomers.DataSource = listCustomers;

        }

        // Code to run after customer data is populated
        private void dgvCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCustomers.Columns["ID"].Visible = false;
            dgvCustomers.Columns["CustFirst"].HeaderText = "First Name";
            dgvCustomers.Columns["CustLast"].HeaderText = "Last Name";
            dgvCustomers.Columns["CustPhone"].HeaderText = "Phone";
            dgvCustomers.Columns["CustAddress"].HeaderText = "Address";
            dgvCustomers.Columns["CustEmail"].HeaderText = "Email";
            dgvCustomers.Columns["CustPostal"].HeaderText = "Postal";
            dgvCustomers.Columns["CustCity"].HeaderText = "City";
        }

        private void BtnCustomersBack_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabPOS;
        }

        private void BtnSelectCustomer_Click(object sender, EventArgs e)
        {
            Customer selectedCust = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;

            GlobalData.ViewData["posSelectedCustomer"] = selectedCust;

            mainView.SelectedTab = tabPOS;
        }
        private void BtnNewCustomer_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewCustomer;
        }
        private void dgvCustomers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnEditCustomer.Enabled = true;
        }
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            string status;
            GlobalData.ViewData["editSelectedCustomer"] = GlobalConfig.Connection.GetCustomerByID((int)dgvCustomers.SelectedRows[0].Cells["id"].Value,out status);
            mainView.SelectedTab = subtabEditCustomer;
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string status;

            List<Customer> listCustomers = GlobalConfig.Connection.GetCustomersList(out status, txtCustomerSearch.Text);
            dgvCustomers.DataSource = listCustomers;
        }
    }
}
