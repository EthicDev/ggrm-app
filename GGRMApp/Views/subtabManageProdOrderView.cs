using GGRMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void subtabManagePartOrder_Enter(object sender, EventArgs e)
        {
            int[] ids = { orderToManage.ID };
            string status;
            dgvPartOrderItems.DataSource = GlobalConfig.Connection.GetOrderLinesByProdOrdIDs(ids, out status);
        }

        private void dgvPartOrderItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPartOrderItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvPartOrderItems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvPartOrderItems.Columns["id"].Visible = false;
            dgvPartOrderItems.Columns["pordNumber"].HeaderText = "PO #";
            dgvPartOrderItems.Columns["Requesting Employee"].Visible = false;
            dgvPartOrderItems.Columns["serOrdID"].Visible = false;
            dgvPartOrderItems.Columns["prodOrdID"].Visible = false;
            dgvPartOrderItems.Columns["custOrdID"].Visible = false;
            dgvPartOrderItems.Columns["inventoryID"].Visible = false;
            dgvPartOrderItems.Columns["invQuantity"].Visible = false;
            dgvPartOrderItems.Columns["orlOrderQuantity"].Visible = false;

            dgvPartOrderItems.Columns["prodDescription"].Visible = false;

            dgvPartOrderItems.Columns["orlPrice"].HeaderText = "Unit Price";
            dgvPartOrderItems.Columns["orlPrice"].ReadOnly = true;
            dgvPartOrderItems.Columns["orlPrice"].DefaultCellStyle.Format = "c";

        }
    }
}
