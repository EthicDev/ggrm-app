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

//Coded by: Cooper Keddy & Macklem Curtis
//Date: Nov/Dec 2019

namespace GGRMApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void SubtabCustomers_Enter(object sender, EventArgs e)
        {
            // Display list of customer data
            using (SqlConnection conn = new SqlConnection(GlobalConfig.ConString("GGRM")))
            {
                conn.Open();
                string sqlCommand = "SELECT id, custLast+', '+custFirst AS custName, custPhone, custAddress, custCity, custPostal, custEmail FROM customer";
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCommand, conn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgvCustomers.DataSource = dtbl;
            }
        }

        // Code to run after customer data is populated
        private void dgvCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCustomers.Columns["id"].Visible = false;
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = subtabCustomers;
        }

        private void BtnCustomersBack_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabPOS;
        }



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

        private void BtnSelectCustomer_Click(object sender, EventArgs e)
        {

        }
    }
}
