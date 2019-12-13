using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        string context;

        public void OpenItemPicker(string context)
        {
            this.context = context;
            mainView.SelectedTab = subtabItemPicker;
        }
        private void SubtabItemPicker_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtInventory = GlobalConfig.Connection.GetInventoryDataTable(out status);
            dgvItemPicker.DataSource = dtInventory;
            dgvItemPicker.Focus();
            btnItemPickerSelect.Visible = false;
            dgvItemPicker.ClearSelection();


        }
        private void DgvItemPicker_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvItemPicker.SelectedRows != null)
            {
                btnItemPickerSelect.Visible = true;
            }

            if(dgvItemPicker.SelectedRows.Count > 1)
            {
                btnItemPickerSelect.Text = "Select Items";
            } else
            {
                btnItemPickerSelect.Text = "Select Item";
            }
        }
        private void BtnItemPickerSelect_Click(object sender, EventArgs e)
        {
            switch (context)
            {
                case "diagnose":
                    foreach (DataGridViewRow row in dgvItemPicker.SelectedRows)
                    {
                        OrderLine ol = new OrderLine();
                        ol.ID = selectedServiceOrder.serviceParts.Count + 1;
                        ol.ServiceOrderID = selectedServiceOrder.ID;
                        ol.ColItemName = (string)row.Cells["Name"].Value;
                        ol.ColItemDesc = (string)row.Cells["Description"].Value;
                        ol.ColItemBrand = (string)row.Cells["Brand"].Value;
                        ol.InventoryID = (int)row.Cells["Inventory ID"].Value;
                        ol.ColOrderQuantity = 1;
                        ol.ColStockQuantity = (int)row.Cells["Quantity"].Value;
                        ol.ColPrice = (decimal)row.Cells["Price"].Value;
                        ol.ColNote = "hi";
                        ol.ColOrderReq = false;
                        diagnoseItemList.serviceParts.Add(ol);

                        context = "";
                    }
                    break;
            }
        }

    }
}
