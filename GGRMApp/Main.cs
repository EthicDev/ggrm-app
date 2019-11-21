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
using GGRMLib.DataSet_CustomersTableAdapters;
using GGRMLib.DataAccess;

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

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = subtabCustomers;
        }

        private void BtnCustomersBack_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabPOS;
        }

        private void TlpCustomerSearch_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Column == 1)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 48, 48, 48)), e.CellBounds);
            }
        }
    }
}
