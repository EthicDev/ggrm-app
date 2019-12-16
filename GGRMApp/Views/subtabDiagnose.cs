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

    //Coded by: Macklem Curtis & Cooper Keddy
    //Date: Nov/Dec 2019

    public partial class Main : Form
    {
        ServiceOrder diagnoseItemList = new ServiceOrder();
        private void SubtabDiagnose_Enter(object sender, EventArgs e)
        {
            lblDiagnoseServiceNumber.Text = "Service #SO-" + selectedServiceOrder.SerOrdNumber;
            dgvDiagnoseItemList.DataSource = selectedServiceOrder.serviceParts;
        }

        private void dgvDiagnoseItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDiagnoseItemList.Columns["ID"].Visible = false;
            dgvDiagnoseItemList.Columns["ColItemName"].HeaderText = "Name";
            dgvDiagnoseItemList.Columns["ColItemDesc"].Visible = false;
            dgvDiagnoseItemList.Columns["ColItemBrand"].HeaderText = "Brand";
            dgvDiagnoseItemList.Columns["ColItemSize"].Visible = false;
            dgvDiagnoseItemList.Columns["ColItemMeasure"].Visible = false;
            dgvDiagnoseItemList.Columns["ColPrice"].HeaderText = "Unit Price";
            dgvDiagnoseItemList.Columns["ColPrice"].DefaultCellStyle.Format = "c";
            dgvDiagnoseItemList.Columns["ColStockQuantity"].HeaderText = "No. In Stock";
            dgvDiagnoseItemList.Columns["ColOrderQuantity"].HeaderText = "Quantity Needed";
            dgvDiagnoseItemList.Columns["ColOrderReq"].Visible = false;
            dgvDiagnoseItemList.Columns["ColNote"].HeaderText = "Note";
            dgvDiagnoseItemList.Columns["ColWarranty"].HeaderText = "Warranty";
            dgvDiagnoseItemList.Columns["InventoryID"].Visible = false;
            dgvDiagnoseItemList.Columns["OrderID"].Visible = false;
            dgvDiagnoseItemList.Columns["ProdOrderID"].Visible = false;
            dgvDiagnoseItemList.Columns["ServiceOrderID"].Visible = false;
        }
        private void BtnDiagnoseAddItem_Click(object sender, EventArgs e)
        {
            OpenItemPicker("diagnose");
            savePreviousTab();
        }

        private void btnDiagnoseSubmit_Click(object sender, EventArgs e)
        {
            string status;
            bool prodOrderMade = false;
            ProductOrder po = new ProductOrder();
            foreach (OrderLine ol in selectedServiceOrder.serviceParts)
            {
                if (ol.ColOrderQuantity > ol.ColStockQuantity)
                {
                    if (!prodOrderMade)
                    {
                        po.PordStatus = "Requested";
                        po.RequestingEmpID = currentUser.ID;
                        po.PordRequestSource = "SO-" + selectedServiceOrder.ID.ToString();
                        po = GlobalConfig.Connection.CreateProductOrder(po, out status);
                        prodOrderMade = true;
                    }
                    ol.ProdOrderID = po.ID;
                }
                GlobalConfig.Connection.CreateOrderLine(ol, out status);
            }
            selectedServiceOrder.SerOrdDiagnosis = txtDiagnoseDescription.Text;
            selectedServiceOrder.TechnicianID = currentUser.ID;
            selectedServiceOrder.SerOrdStatus = "Diagnosed";
            GlobalConfig.Connection.EditServiceOrder(selectedServiceOrder, out status);
            MessageBox.Show(status);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Hardcoded go back to repair tab

            mainView.SelectedTab = tabRepairs;
        }
    }
}
