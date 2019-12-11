using GGRMLib;
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
        private void tabUsers_Enter(object sender, EventArgs e)
        {
            string searchString;
            string status;
            DataTable dtEmployees = GlobalConfig.Connection.GetEmployeeDataTable(out status);

            dgvUsers.DataSource = dtEmployees;
        }

        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvUsers.ReadOnly = true;
            dgvUsers.Columns["posName"].HeaderText = "Position";
            dgvUsers.Columns["empFirst"].HeaderText = "First Name";
            dgvUsers.Columns["empLast"].HeaderText = "Last Name";
            dgvUsers.Columns["empUser"].HeaderText = "Username";
            dgvUsers.Columns["id"].Visible = false;
        }


        private void BtnUsersAdd_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewUser;
        }


        private void BtnUsersEdit_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabEditUser;
        }





        //new user controls

        private void BtnNewUserCreate_Click(object sender, EventArgs e)
        {

        }



        private void BtnEditUserConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
