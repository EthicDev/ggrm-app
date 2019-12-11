using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GGRMLib;
using GGRMLib.Models;
using GGRMLib.DataAccess;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        CustomerOrder posCurrentOrder = new CustomerOrder();
        Customer posSelectedCust = new Customer();
        ProductOrder posProductOrder = new ProductOrder();
        private void tabPOS_Enter(object sender, EventArgs e)
        {
            string status;

            if (posCurrentOrder.ID == -1)
            {
                posCurrentOrder.ID = GlobalConfig.Connection.CustomerOrderNextID(out status);
            }
            lblNewOrder.Text = "New Order " + posCurrentOrder.ID;

            ToolTip ttAddItemBtn = new ToolTip();
            ttAddItemBtn.AutoPopDelay = 5000;
            ttAddItemBtn.InitialDelay = 500;
            ttAddItemBtn.ReshowDelay = 500;
            //ttAddItemBtn.ShowAlways = true;

            btnAddItem.Enabled = false;
            btnAddService.Enabled = false;

            if (posSelectedCust.CustFirst == null)
            {   
                //ttAddItemBtn.SetToolTip(btnAddItem, "You must select a customer.");
                //ttAddItemBtn.SetToolTip(btnAddService, "You must select a customer.");
                //subtabPOSButtons.Refresh();
            } else
            {
                ttAddItemBtn.SetToolTip(btnAddItem, null);
                ttAddItemBtn.SetToolTip(btnAddService, null);
                btnAddItem.Enabled = true;
                btnAddService.Enabled = true;
            }
            lblSelectedCustomer.Text = posSelectedCust.CustFirst != null ? "Customer: " + posSelectedCust.CustFirst + " " + posSelectedCust.CustLast : "No customer selected.";

            dgvItemCart.DataSource = posCurrentOrder.orderLines;
        }


        private void dgvItemCart_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvItemCart.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvItemCart.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvItemCart.Columns["ID"].HeaderText = "#";
            dgvItemCart.Columns["ID"].ReadOnly = true;

            dgvItemCart.Columns["ColPrice"].HeaderText = "Unit Price";
            dgvItemCart.Columns["ColPrice"].ReadOnly = true;
            dgvItemCart.Columns["ColPrice"].DefaultCellStyle.Format = "c";

            dgvItemCart.Columns["ColOrderQuantity"].HeaderText = "Quantity";

            dgvItemCart.Columns["ColStockQuantity"].HeaderText = "Stock Quantity";
            dgvItemCart.Columns["ColStockQuantity"].ReadOnly = true;

            dgvItemCart.Columns["ColOrderReq"].Visible = false;

            dgvItemCart.Columns["ColNote"].HeaderText = "Note";

            dgvItemCart.Columns["InventoryID"].Visible = false;

            dgvItemCart.Columns["ColItemName"].HeaderText = "Item Name";
            dgvItemCart.Columns["ColItemName"].ReadOnly = true;

            dgvItemCart.Columns["ColItemDesc"].HeaderText = "Item Desc";
            dgvItemCart.Columns["ColItemDesc"].ReadOnly = true;

            dgvItemCart.Columns["ColItemBrand"].HeaderText = "Item Brand";
            dgvItemCart.Columns["ColItemBrand"].ReadOnly = true;

            dgvItemCart.Columns["OrderID"].Visible = false;

            dgvItemCart.Columns["ProdOrderID"].Visible = false;

            dgvRepairCart.DataSource = posCurrentOrder.serviceOrders;

            dgvRepairCart.Columns["ID"].HeaderText = "#";
            dgvRepairCart.Columns["ID"].ReadOnly = true;

            dgvRepairCart.Columns["ServiceName"].HeaderText = "Service";
            dgvRepairCart.Columns["ServiceName"].ReadOnly = true;

            dgvRepairCart.Columns["SerOrdDateIn"].HeaderText = "Date In";

            dgvRepairCart.Columns["SerOrdDateOut"].Visible = false;
            dgvRepairCart.Columns["SerOrdIssue"].HeaderText = "Issue Desc."
                ;
            dgvRepairCart.Columns["SerOrdWarranty"].HeaderText = "Warranty?";

            dgvRepairCart.Columns["SerOrdStatus"].HeaderText = "Status";
            dgvRepairCart.Columns["SerOrdStatus"].ReadOnly = true;

            dgvRepairCart.Columns["CustOrdID"].Visible = false;
            dgvRepairCart.Columns["ServiceID"].Visible = false;
            dgvRepairCart.Columns["EmpID"].Visible = false;
            dgvRepairCart.Columns["EquipID"].Visible = false;

            UpdateOrderTotal();
        }

        private void dgvItemCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Only update total if the line's quantity was changed.
            if (e.ColumnIndex == 6)
            {
                UpdateOrderTotal();
            }
        }

        private void UpdateOrderTotal()
        { 
            posCurrentOrder.OrdTotal = 0m;
            foreach(OrderLine line in posCurrentOrder.orderLines)
            {
                posCurrentOrder.OrdTotal += line.ColPrice * line.ColOrderQuantity;
            }
            posCurrentOrder.OrdPartyPlan = posCurrentOrder.OrdTotal * 0.02m;

            lblOrderTotal.Text = posCurrentOrder.OrdTotal.ToString("c");
        }

        private void TlpItemListPOSSearch_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 48, 48, 48)), e.CellBounds);
        }

        private void BtnAddItem_Click(object sender, EventArgs e)

        {

            tcPOSSidebar.SelectedTab = subtabPOSAddItem;

        }



        private void BtnPOSItemListBack_Click(object sender, EventArgs e)

        {

            tcPOSSidebar.SelectedTab = subtabPOSButtons;

        }
        private void BtnRepairReqBack_Click(object sender, EventArgs e)
        {
            tcPOSSidebar.SelectedTab = subtabPOSButtons;
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            tcPOSSidebar.SelectedTab = subtabRepairReq;
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            string status;
            posCurrentOrder.EmpID = 1;
            posCurrentOrder.CustID = posSelectedCust.ID;
            posCurrentOrder.OrdCreationDate = DateTime.UtcNow;
            posCurrentOrder.OrdNumber = posCurrentOrder.ID;
            posCurrentOrder.PaymentID = 1;

            posCurrentOrder = GlobalConfig.Connection.CreateCustomerOrder(posCurrentOrder, out status);

            bool madeProdOrder = false;

            for (int i = 0; i < posCurrentOrder.orderLines.Count; i++)
            {
                OrderLine currentLine = posCurrentOrder.orderLines[i];
                currentLine.OrderID = posCurrentOrder.ID;

                if (currentLine.ColOrderQuantity > currentLine.ColStockQuantity)
                {
                    currentLine.ColOrderReq = true;
                    if (!madeProdOrder)
                    {
                        posProductOrder = GlobalConfig.Connection.CreateProductOrder(posProductOrder, out status);
                        madeProdOrder = true;

                        currentLine.ProdOrderID = posProductOrder.ID;
                    } else
                    {
                        currentLine.ProdOrderID = posProductOrder.ID;
                    }
                } else
                {
                    currentLine.ProdOrderID = null;
                }

                currentLine = GlobalConfig.Connection.CreateOrderLine(currentLine, out status);
            }
            for (int i = 0; i < posCurrentOrder.serviceOrders.Count; i++)
            {
                posCurrentOrder.serviceOrders[i].CustOrdID = posCurrentOrder.ID;
                posCurrentOrder.serviceOrders[i] = GlobalConfig.Connection.CreateServiceOrder(posCurrentOrder.serviceOrders[i], out status);
            }
            MessageBox.Show("Order creation complete.");
            posCurrentOrder = new CustomerOrder();
            posSelectedCust = new Customer();
            posProductOrder = new ProductOrder();
            tabPOS_Enter(this, null);
        }

        private void btnCreateAndPay_Click(object sender, EventArgs e)
        {

        }
    }
}
