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
        private void SubtabDiagnose_Enter(object sender, EventArgs e)
        {

            dgvDiagnoseItemList.DataSource = 
            
            dgvDiagnoseItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvDiagnoseItemList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvDiagnoseItemList.Columns["ID"].HeaderText = "#";
            dgvDiagnoseItemList.Columns["ID"].ReadOnly = true;

            dgvDiagnoseItemList.Columns["ColPrice"].HeaderText = "Unit Price";
            dgvDiagnoseItemList.Columns["ColPrice"].ReadOnly = true;
            dgvDiagnoseItemList.Columns["ColPrice"].DefaultCellStyle.Format = "c";

            dgvDiagnoseItemList.Columns["ColOrderQuantity"].HeaderText = "Quantity";

            dgvDiagnoseItemList.Columns["ColStockQuantity"].HeaderText = "Stock Quantity";
            dgvDiagnoseItemList.Columns["ColStockQuantity"].ReadOnly = true;

            dgvDiagnoseItemList.Columns["ColOrderReq"].Visible = false;

            dgvDiagnoseItemList.Columns["ColNote"].HeaderText = "Note";

            dgvDiagnoseItemList.Columns["InventoryID"].Visible = false;

            dgvDiagnoseItemList.Columns["ColItemName"].HeaderText = "Item Name";
            dgvDiagnoseItemList.Columns["ColItemName"].ReadOnly = true;

            dgvDiagnoseItemList.Columns["ColItemDesc"].HeaderText = "Item Desc";
            dgvDiagnoseItemList.Columns["ColItemDesc"].ReadOnly = true;

            dgvDiagnoseItemList.Columns["ColItemBrand"].HeaderText = "Item Brand";
            dgvDiagnoseItemList.Columns["ColItemBrand"].ReadOnly = true;

            dgvDiagnoseItemList.Columns["OrderID"].Visible = false;

            dgvDiagnoseItemList.Columns["ProdOrderID"].Visible = false;
        }
        private void BtnDiagnoseAddItem_Click(object sender, EventArgs e)
        {
            OpenItemPicker("diagnose");
            savePreviousTab();
        }
    }
}
