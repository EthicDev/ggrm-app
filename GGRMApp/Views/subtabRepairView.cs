using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
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
        private void subtabRepair_Enter(object sender, EventArgs e)
        {
            if (selectedServiceOrder.SerOrdStatus == "Pending" || selectedServiceOrder.SerOrdStatus == "Diagnosed")
            {
                selectedServiceOrder.SerOrdStatus = "Repairing";
            }
            lblRepairServiceNumber.Text = "Service #SO-"+selectedServiceOrder.SerOrdNumber;
            lblRepairEquipName.Text = selectedServiceOrder.EquipName;
            dgvRepairItemList.DataSource = selectedServiceOrder.serviceParts;
        }

        private void dgvRepairItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvRepairItemList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRepairItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvRepairItemList.Columns["ID"].Visible = false;
            dgvRepairItemList.Columns["ColOrderReq"].Visible = false;
            dgvRepairItemList.Columns["ColOrderQuantity"].HeaderText = "Quantity Used";
            //dgvRepairItemList.Columns["ColSupplierQuantityOrdered"].Visible = false;
            dgvRepairItemList.Columns["InventoryID"].Visible = false;
            dgvRepairItemList.Columns["OrderID"].Visible = false;
            dgvRepairItemList.Columns["ProdOrderID"].Visible = false;
            dgvRepairItemList.Columns["ServiceOrderID"].Visible = false;
        }

        private void btnRepairComplete_Click(object sender, EventArgs e)
        {
            selectedServiceOrder.SerOrdStatus = "Repair Complete";
            selectedServiceOrder.SerOrdRepairNote = txtRepairNotes.Text;
            string status;
            foreach (OrderLine part in selectedServiceOrder.serviceParts)
            {
                int numUsed = -1 * part.ColOrderQuantity;
                int idToUpdate = part.InventoryID;

                GlobalConfig.Connection.UpdateStockByID(idToUpdate, numUsed, out status);
                MessageBox.Show(status);
            }
            GlobalConfig.Connection.EditServiceOrder(selectedServiceOrder, out status);
            MessageBox.Show(status);
        }
    }
}
