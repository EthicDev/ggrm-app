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
using GGRMLib.DataSet_CustomersTableAdapters;
using GGRMLib.DataAccess;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        CustomerOrder posCurrentOrder = new CustomerOrder();
        Customer posSelectedCust = new Customer();
        private void tabPOS_Enter(object sender, EventArgs e)
        {
            string status;
            //CustomerOrder currentOrder = (CustomerOrder)GlobalData.ViewData["posCurrentOrder"];
            if (posCurrentOrder.ID == -1)
            {
                posCurrentOrder.ID = GlobalConfig.Connection.CustomerOrderNextID(out status);
            }
            lblNewOrder.Text = "New Order " + posCurrentOrder.ID;

            //GlobalData.ViewData["posCurrentOrder"] = currentOrder;

            //Customer selectedCust = (Customer)GlobalData.ViewData["posSelectedCustomer"];

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
            dgvItemCart.Columns["ID"].HeaderText = "#";
            dgvItemCart.Columns["ColPrice"].HeaderText = "Price";
            dgvItemCart.Columns["ColQuantity"].HeaderText = "Quantity";
            dgvItemCart.Columns["ColOrderReq"].HeaderText = "Order Required?";
            dgvItemCart.Columns["ColNote"].HeaderText = "Note";

            dgvRepairCart.DataSource = posCurrentOrder.serviceOrders;
            dgvRepairCart.Columns["ID"].HeaderText = "#";
            dgvRepairCart.Columns["SerOrdDateIn"].HeaderText = "Date In";
            dgvRepairCart.Columns["SerOrdDateOut"].HeaderText = "Date Out";
            dgvRepairCart.Columns["SerOrdIssue"].HeaderText = "Issue Desc.";
            dgvRepairCart.Columns["SerOrdWarranty"].HeaderText = "Warranty?";
            dgvRepairCart.Columns["SerOrdStatus"].HeaderText = "Status";
            dgvRepairCart.Columns["CustOrdID"].HeaderText = "Order ID";

        }

        private void tabPOS_RefreshCart()
        {
            dgvItemCart.DataSource = posCurrentOrder.orderLines;
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

            for (int i = 0; i < posCurrentOrder.orderLines.Count; i++)
            {
                posCurrentOrder.orderLines[i].OrderID = posCurrentOrder.ID;
                posCurrentOrder.orderLines[i] = GlobalConfig.Connection.CreateCustomerOrderLine(posCurrentOrder.orderLines[i], out status);
            }
            for (int i = 0; i < posCurrentOrder.serviceOrders.Count; i++)
            {
                posCurrentOrder.serviceOrders[i].CustOrdID = posCurrentOrder.ID;
                posCurrentOrder.serviceOrders[i] = GlobalConfig.Connection.CreateServiceOrder(posCurrentOrder.serviceOrders[i], out status);
            }
            MessageBox.Show("Order creation complete.");
        }
    }
}
