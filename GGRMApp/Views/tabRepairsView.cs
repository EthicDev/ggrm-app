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
        private void tabRepairs_Enter(object sender, EventArgs e)
        {
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
            savePreviousTab();
            mainView.SelectedTab = subtabDiagnose;
        }
    }
}
