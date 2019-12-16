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
    //Coded By: Macklem Curtis & Cooper Keddy
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        ServiceOrder selectedServiceOrder;

        private void tabRepairs_Enter(object sender, EventArgs e)
        {
            selectedServiceOrder = new ServiceOrder();
            btnDiagnose.Enabled = false;
            btnBeginRepair.Enabled = false;
            string status;

            DataTable dtPendingServices = GlobalConfig.Connection.GetPendingServicesDataTable(out status);

            dgvPendingRepairs.DataSource = dtPendingServices;
            dgvPendingRepairs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvPendingRepairs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvPendingRepairs.Columns["id"].HeaderText = "Service #";
            //dgvPendingRepairs.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvPendingRepairs.Columns["Warranty?"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvPendingRepairs.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvPendingRepairs.Columns["Intake Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvPendingRepairs.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvPendingRepairs.Columns["Issue"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgvPendingRepairs.Columns["Issue"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }



        private void BtnDiagnose_Click(object sender, EventArgs e)

        {

            string status;

            int sID = (int)dgvPendingRepairs.SelectedRows[0].Cells["id"].Value;

            selectedServiceOrder = GlobalConfig.Connection.GetServiceOrderByID(sID, out status);

            savePreviousTab();

            mainView.SelectedTab = subtabDiagnose;

        }



        private void dgvPendingRepairs_SelectionChanged(object sender, EventArgs e)

        {
            if (dgvPendingRepairs.SelectedRows.Count >= 1)
            {
                DataGridViewRow selectedRow = dgvPendingRepairs.SelectedRows[0];
                if (selectedRow.Cells["Status"].Value.ToString() == "Pending")
                {
                    btnDiagnose.Enabled = true;
                    btnBeginRepair.Enabled = false;
                }
                else if (selectedRow.Cells["Status"].Value.ToString() != "Repair Complete")
                {
                    btnBeginRepair.Enabled = true;
                    btnDiagnose.Enabled = false;
                }
                else
                {
                    btnBeginRepair.Enabled = false;
                    btnDiagnose.Enabled = false;
                }
            }
        }

        private void BtnBeginRepair_Click(object sender, EventArgs e)
        {
            string status;
            int sID = (int)dgvPendingRepairs.SelectedRows[0].Cells["id"].Value;

            selectedServiceOrder = GlobalConfig.Connection.GetServiceOrderByID(sID, out status);
            selectedServiceOrder.serviceParts = GlobalConfig.Connection.GetServicePartsListByServOrdID(selectedServiceOrder.ID, out status);

            savePreviousTab();
            mainView.SelectedTab = subtabRepair;
        }
    }
}
