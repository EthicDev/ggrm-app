using GGRMLib;
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

namespace GGRMApp
{
    public partial class CustomersFormOld : Form
    {
        public CustomersFormOld()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            NewCustomerForm add = new NewCustomerForm();
            add.Show();
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            
            using (SqlConnection conn = new SqlConnection(GlobalConfig.ConString("GGRM")))
            {
                conn.Open();
                string sqlCommand = "SELECT id, custLast+', '+custFirst AS custName, custPhone, custAddress, custCity, custPostal, custEmail FROM customer";
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCommand,conn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgvCustomer.DataSource = dtbl;
            }
            
        }
    }
}
