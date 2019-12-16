using GGRMLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GGRMLib.Models;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        Employee empToEdit;
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
            string status;
            int empID = (int)dgvUsers.SelectedRows[0].Cells[0].Value;

            empToEdit = GlobalConfig.Connection.GetEmployeeByID(empID, out status);

            savePreviousTab();
            mainView.SelectedTab = subtabEditUser;
        }





        //new user controls


        private void SubtabNewUser_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtPosition = GlobalConfig.Connection.GetPositionDataTable(out status);
            ddlNewUserPosition.DataSource = dtPosition;
            ddlNewUserPosition.DisplayMember = "posName";
            ddlNewUserPosition.ValueMember = "id";
        }

        private void BtnNewUserCreate_Click(object sender, EventArgs e)
        {

            string status;
            Employee emp = new Employee();
            emp.EmpFirst = txtNewUserFirst.Text;
            emp.EmpLast = txtNewUserLast.Text;
            emp.PosID = (int)ddlNewUserPosition.SelectedValue;
            emp.EmpUser = txtNewUserUsername.Text;
            emp.EmpPassword = txtNewUserPassword.Text;

            

            emp = GlobalConfig.Connection.CreateEmployee(emp, out status);
            lblNewCustomerStatus.Text = status;
        }


        // edit user controls

        private void SubtabEditUser_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtPosition = GlobalConfig.Connection.GetPositionDataTable(out status);
            ddlEditUserPosition.DataSource = dtPosition;
            ddlEditUserPosition.DisplayMember = "posName";
            ddlEditUserPosition.ValueMember = "id";

            txtEditUserFirst.Text = empToEdit.EmpFirst;
            txtEditUserLast.Text = empToEdit.EmpLast;
            ddlEditUserPosition.SelectedValue = empToEdit.PosID;
            txtEditUserUsername.Text = empToEdit.EmpUser;

            switch(empToEdit.EmpDisabled)
            {
                case false:
                    btnEditUserDisable.Text = "Disable Employee";
                    break;
                case true:
                    btnEditUserDisable.Text = "Enable Employee";
                    break;
            }
        }
        private void BtnEditUserConfirm_Click(object sender, EventArgs e)
        {
            string status;

            empToEdit.EmpFirst = txtEditUserFirst.Text;
            empToEdit.EmpLast = txtEditUserLast.Text;
            empToEdit.PosID = (int)ddlEditUserPosition.SelectedValue;
            empToEdit.EmpUser = txtEditUserUsername.Text;
            empToEdit.EmpPassword = txtEditUserPassword.Text;

            GlobalConfig.Connection.EditEmployee(empToEdit, out status);
            lblEditUserStatus.Text = status;
        }

        private void BtnEditUserDisable_Click(object sender, EventArgs e)
        {
            string status;
            GlobalConfig.Connection.ToggleEmployeeDisabled(empToEdit, out status);
            lblEditUserStatus.Text = status;

            switch (empToEdit.EmpDisabled)
            {
                case true:
                    btnEditUserDisable.Text = "Disable Employee";
                    break;
                case false:
                    btnEditUserDisable.Text = "Enable Employee";
                    break;
            }
        }
    }
}
