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

//Coded by: Cooper Keddy & Macklem Curtis
//Date: Nov/Dec 2019

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        List<Button> buttons = new List<Button>();
        List<Button> dataButtons = new List<Button>();

        //Previous Tab for Back buttons
        public TabPage previousTab = null;
        public Main()
        {
            InitializeComponent();

            //add buttons to button list
            buttons.Add(btnPOS);
            //buttons.Add(btnInventory);
            buttons.Add(btnOrdering);
            buttons.Add(btnRepairs);
            buttons.Add(btnReports);
            buttons.Add(btnUsers);
            buttons.Add(btnData);

            dataButtons.Add(btnDataInventory);
            dataButtons.Add(btnDataProducts);
            dataButtons.Add(btnDataCustomers);
            dataButtons.Add(btnDataOrders);
        }

        //------------------------CUSTOM UI STUFF--------------------------

        //drag and drop
        private bool mouseDown = false;
        private Point lastLocation;
        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorLocation = this.PointToClient(System.Windows.Forms.Cursor.Position);

            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void titleBar_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        //function to reset all button colours
        private void deselectButtons()
        {
            foreach(Button b in buttons)
            {
                b.BackColor = Color.Transparent;
            }
        }

        //resizable
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }

        //--------------------------------------------------------

        //coloring cells of table layout panels
        private void TlpCustomerSearch_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Column == 1)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 48, 48, 48)), e.CellBounds);
            }
        }

        private void TlpMain_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Row == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 48, 48, 48)), e.CellBounds);
            }
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabCustomers;
        }

        //code to make the colours of the buttons change when you click thems
        private void BtnPOS_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabPOS;
            deselectButtons();
            btnPOS.BackColor = Color.FromArgb(255, 64, 64, 64);
        }

        //private void BtnInventory_Click(object sender, EventArgs e)
        //{
        //    mainView.SelectedTab = tabInventory;
        //    deselectButtons();
        //    //btnInventory.BackColor = Color.FromArgb(255, 64, 64, 64);
        //}

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabOrders;
            deselectButtons();
            btnOrdering.BackColor = Color.FromArgb(255, 64, 64, 64);
        }
        private void BtnRepair_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabRepairs;
            deselectButtons();
            btnRepairs.BackColor = Color.FromArgb(255, 64, 64, 64);
        }
        private void BtnReports_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabReports;
            deselectButtons();
            btnReports.BackColor = Color.FromArgb(255, 64, 64, 64);
        }

        private void BtnUsers_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabUsers;
            deselectButtons();
            btnUsers.BackColor = Color.FromArgb(255, 64, 64, 64);
        }

        private void BtnData_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabData;
            deselectButtons();
            btnData.BackColor = Color.FromArgb(255, 64, 64, 64);
            tcDataView.Focus();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            
        }

        private void BtnExpand_Click(object sender, EventArgs e)
        {
            if(this.WindowState==FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnExpand.Text = "2";
            } else
            {
                this.WindowState = FormWindowState.Normal;
                btnExpand.Text = "1";
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }





        //BACK BUTTONS

        private void savePreviousTab()

        {

            previousTab = mainView.SelectedTab;

        }



        public void GoBack(object sender, EventArgs e)

        {

            mainView.SelectedTab = previousTab;

        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {

        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateAndPay_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {

        }

        private void BtnPOSItemListBack_Click(object sender, EventArgs e)
        {

        }

        private void dgvPOSItemLookup_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void btnPOSItemSearch_Click(object sender, EventArgs e)
        {

        }

        private void TlpItemListPOSSearch_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

        }

        private void btnSelectItems_Click(object sender, EventArgs e)
        {

        }

        private void subtabPOSAddItem_Enter(object sender, EventArgs e)
        {

        }

        private void BtnRepairReqBack_Click(object sender, EventArgs e)
        {

        }

        private void btnAddServiceRequest_Click(object sender, EventArgs e)
        {

        }

        private void subtabRepairReq_Enter(object sender, EventArgs e)
        {

        }

        private void dgvItemCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemCart_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void tabPOS_Enter(object sender, EventArgs e)
        {

        }

        private void dgvOrderRequests_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void BtnOrderSelected_Click(object sender, EventArgs e)
        {

        }

        private void BtnManageOrder_Click(object sender, EventArgs e)
        {

        }

        private void tabOrders_Enter(object sender, EventArgs e)
        {

        }

        private void dgvPendingRepairs_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void BtnDiagnose_Click(object sender, EventArgs e)
        {

        }

        private void BtnBeginRepair_Click(object sender, EventArgs e)
        {

        }

        private void tabRepairs_Enter(object sender, EventArgs e)
        {

        }

        private void BtnGenerateNewReport_Click(object sender, EventArgs e)
        {

        }

        private void BtnGenerateReportBack_Click(object sender, EventArgs e)
        {

        }

        private void BtnUsersAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnUsersEdit_Click(object sender, EventArgs e)
        {

        }

        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void tabUsers_Enter(object sender, EventArgs e)
        {

        }

        private void BtnDataInventory_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataProducts_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataCustomers_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataOrders_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataInventoryAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataInventoryEdit_Click(object sender, EventArgs e)
        {

        }

        private void dgvDataInventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void btnDataInventorySearch_Click(object sender, EventArgs e)
        {

        }

        private void TlpDataInventorySearch_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

        }

        private void TabDataInventory_Enter(object sender, EventArgs e)
        {

        }

        private void BtnDataProductsAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataProductsEdit_Click(object sender, EventArgs e)
        {

        }

        private void dgvDataProducts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void btnDataProductsSearch_Click(object sender, EventArgs e)
        {

        }

        private void tabDataProducts_Enter(object sender, EventArgs e)
        {

        }

        private void BtnDataCustomersAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataCustomersEdit_Click(object sender, EventArgs e)
        {

        }

        private void dgvDataCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void btnDataCustomersSearch_Click(object sender, EventArgs e)
        {

        }

        private void tabDataCustomers_Enter(object sender, EventArgs e)
        {

        }

        private void BtnDataOrdersPay_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataOrdersDetails_Click(object sender, EventArgs e)
        {

        }

        private void dgvDataOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void tabDataOrders_Enter(object sender, EventArgs e)
        {

        }

        private void dgvCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {

        }

        private void BtnManageCustomers_Click(object sender, EventArgs e)
        {

        }

        private void BtnSelectCustomer_Click(object sender, EventArgs e)
        {

        }

        private void SubtabCustomers_Enter(object sender, EventArgs e)
        {

        }

        private void BtnCreateCustomer_Click(object sender, EventArgs e)
        {

        }

        private void SubtabNewCustomer_Leave(object sender, EventArgs e)
        {

        }

        private void BtnNewUserCreate_Click(object sender, EventArgs e)
        {

        }

        private void SubtabNewUser_Enter(object sender, EventArgs e)
        {

        }

        private void BtnNewInventoryAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnNewProductAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnNewOrderCreate_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmChanges_Click(object sender, EventArgs e)
        {

        }

        private void subtabEditCustomer_Enter(object sender, EventArgs e)
        {

        }

        private void BtnEditUserConfirm_Click(object sender, EventArgs e)
        {

        }

        private void SubtabEditUser_Enter(object sender, EventArgs e)
        {

        }

        private void BtnEditProductConfirm_Click(object sender, EventArgs e)
        {

        }

        private void subtabEditProducts_Enter(object sender, EventArgs e)
        {

        }

        private void btnEditInventoryConfirm_Click(object sender, EventArgs e)
        {

        }

        private void subtabEditInventory_Enter(object sender, EventArgs e)
        {

        }

        private void BtnManagePartOrderSave_Click(object sender, EventArgs e)
        {

        }
    }
}
