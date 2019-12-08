using GGRMLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void tabDataProducts_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtProducts = GlobalConfig.Connection.GetProductsDataTable(out status);
            dgvDataProducts.DataSource = dtProducts;
        }

        private void dgvDataProducts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDataProducts.Columns["id"].Visible = false;
            dgvDataProducts.Columns["prodSize"].Visible = false;
            dgvDataProducts.Columns["prodMeasure"].Visible = false;
        }

        private void btnDataProductsSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtDataProductsSearch.Text;
            string status;

            DataTable dtProducts = GlobalConfig.Connection.GetProductsDataTable(out status, searchString);
            dgvDataProducts.DataSource = dtProducts;
        }


    }
}
