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
        private void subtabPOSAddItem_Enter(object sender, EventArgs e)
        {
            string status;

            //Pull inventory data from DB and display in DataGridView
            DataTable dtInventoryShort = GlobalConfig.Connection.GetInventoryDataTableShort(out status);
            dgvPOSItemLookup.DataSource = dtInventoryShort;
        }

        private void dgvPOSItemLookup_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPOSItemLookup.Columns["id"].Visible = false;
            dgvPOSItemLookup.Columns["invQuantity"].Visible = false;
        }

        private void btnSelectItems_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dgvPOSItemLookup.SelectedRows)
            {                
                OrderLine ol = new OrderLine();
                ol.ID = posCurrentOrder.orderLines.Count + 1;
                ol.OrderID = posCurrentOrder.ID;
                ol.ColItemName = (string)row.Cells["Name"].Value;
                ol.ColItemDesc = (string)row.Cells["Description"].Value;
                ol.ColItemBrand = (string)row.Cells["Brand"].Value;
                ol.InventoryID = (int)row.Cells["id"].Value;
                ol.ColOrderQuantity = 1;
                ol.ColStockQuantity = (int)row.Cells["invQuantity"].Value;
                ol.ColPrice = (decimal)row.Cells["Price"].Value;
                ol.ColNote = "hi";
                ol.ColOrderReq = false;
                posCurrentOrder.orderLines.Add(ol);
            }

            tcPOSSidebar.SelectedTab = subtabPOSButtons;
        }

        private void btnPOSItemSearch_Click(object sender, EventArgs e)
        {
            string status;
            string searchString = txtPOSItemSearch.Text;
            DataTable dtInventoryShort = GlobalConfig.Connection.GetInventoryDataTableShort(out status, searchString);
            dgvPOSItemLookup.DataSource = dtInventoryShort;
        }

    }
}
