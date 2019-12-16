using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    //Coded By: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        Product productToEdit;
        private void tabDataProducts_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtProducts = GlobalConfig.Connection.GetProductsDataTable(out status);
            dgvDataProducts.DataSource = dtProducts;
        }

        private void dgvDataProducts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDataProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvDataProducts.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDataProducts.Columns["id"].Visible = false;
            dgvDataProducts.Columns["prodSize"].Visible = false;
            dgvDataProducts.Columns["prodMeasure"].Visible = false;
            dgvDataProducts.Columns["prodPrice"].DefaultCellStyle.Format = "c";
        }

        private void btnDataProductsSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtDataProductsSearch.Text;
            string status;

            DataTable dtProducts = GlobalConfig.Connection.GetProductsDataTable(out status, searchString);
            dgvDataProducts.DataSource = dtProducts;
        }
        private void BtnDataProductsEdit_Click(object sender, EventArgs e)
        {
            int selectedID = (int)dgvDataProducts.SelectedRows[0].Cells["id"].Value;
            productToEdit = GlobalConfig.Connection.GetProductByID(selectedID, out string status);
            MessageBox.Show(status);
            savePreviousTab();
            mainView.SelectedTab = subtabEditProducts;
            
        }

        private void BtnDataProductsAdd_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewProduct;
        }
    }
}
