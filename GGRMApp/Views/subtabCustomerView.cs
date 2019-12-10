using GGRMLib;
using GGRMLib.Models;
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
        private void SubtabCustomers_Enter(object sender, EventArgs e)
        {
            string status;
            btnManageCustomers.Enabled = false;
            
            DataTable dtCustomers = GlobalConfig.Connection.GetCustomersDataTable(out status);
            dgvCustomers.DataSource = dtCustomers;

        }

        // Code to run after customer data is populated
        private void dgvCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCustomers.Columns["ID"].Visible = false;
            //dgvCustomers.Columns["CustFirst"].HeaderText = "First Name";
            //dgvCustomers.Columns["CustLast"].HeaderText = "Last Name";
            //dgvCustomers.Columns["CustPhone"].HeaderText = "Phone";
            //dgvCustomers.Columns["CustAddress"].HeaderText = "Address";
            //dgvCustomers.Columns["CustEmail"].HeaderText = "Email";
            //dgvCustomers.Columns["CustPostal"].HeaderText = "Postal";
            //dgvCustomers.Columns["CustCity"].HeaderText = "City";

            //DataColumn nameColumn = new DataColumn();
            //nameColumn.DataType = typeof(string);
            //nameColumn.ColumnName = "Name";
            //nameColumn.DefaultValue = "Name";
            //nameColumn.Expression = "CustLast + ' ' + CustFirst";

            //dgvCustomers.Columns.Add(nameColumn);
        }

        private void BtnCustomersBack_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabPOS;
        }

        private void BtnSelectCustomer_Click(object sender, EventArgs e)
        {
            string status;
            int id = (int)dgvCustomers.SelectedRows[0].Cells["id"].Value;
            posSelectedCust = GlobalConfig.Connection.GetCustomerByID(id, out status);

            mainView.SelectedTab = tabPOS;
        }
        
        private void dgvCustomers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnManageCustomers.Enabled = true;
        }

        private void BtnManageCustomers_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = tabData;
            tcDataView.SelectedTab = tabDataCustomers;
            deselectButtons();
            btnData.BackColor = Color.FromArgb(255, 64, 64, 64);
            tcDataView.Focus();
        }
        

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string status;

            List<Customer> listCustomers = GlobalConfig.Connection.GetCustomersList(out status, txtCustomerSearch.Text);
            dgvCustomers.DataSource = listCustomers;
        }
    }
}
