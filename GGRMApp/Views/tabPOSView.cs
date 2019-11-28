﻿using System;
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
        private void tabPOS_Enter(object sender, EventArgs e)
        {
            Customer selectedCust = (Customer)GlobalData.ViewData["posSelectedCustomer"];
            lblSelectedCustomer.Text = selectedCust.CustFirst != null ? "Customer: " + selectedCust.CustFirst + " " + selectedCust.CustLast : "No customer selected.";
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

        private void BtnPOSItemListBack_Click(object sender, EventArgs e)
        {
            tcPOSSidebar.SelectedTab = tabPOSButtons;
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabRepairRequest;
        }
    }
}
