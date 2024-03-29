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
using GGRMLib.DataAccess;

//Coded by: Cooper Keddy & Macklem Curtis
//Date: Nov/Dec 2019

namespace GGRMApp.Views
{
    //Coded by: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019

    public partial class Main : Form
    {
        List<Button> buttons = new List<Button>();
        List<Button> dataButtons = new List<Button>();

        Employee currentUser;

        //Previous Tab for Back buttons
        public TabPage previousTab = null;
        public Main(int empID)
        {
            InitializeComponent();

            string status;
            currentUser = GlobalConfig.Connection.GetEmployeeByID(empID, out status);

            lblCurrentUser.Text = currentUser.EmpFirst + " " + currentUser.EmpLast;
            lblCurrentUserRole.Text = currentUser.PosName;
            //user icon
            switch(currentUser.PosID)
            {
                case 1:
                    picUserIcon.Image = GGRMApp.Properties.Resources.iconSales;
                    break;
                case 2:
                    picUserIcon.Image = GGRMApp.Properties.Resources.iconTechnician;
                    break;
                case 3:
                    picUserIcon.Image = GGRMApp.Properties.Resources.iconAdmin;
                    break;
                case 4:
                    picUserIcon.Image = GGRMApp.Properties.Resources.iconManager;
                    break;
                case 5:
                    picUserIcon.Image = GGRMApp.Properties.Resources.iconOrdering;
                    break;
            }

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

    }
}
