using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GGRMLib;

namespace GGRMApp.Views
{
    //Coded by: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        private void subtabEditProducts_Enter(object sender, EventArgs e)
        {
            txtEditProductName.Text = productToEdit.ProdName.ToString();
            txtEditProductDescription.Text = productToEdit.ProdDescription.ToString();
            txtEditProductBrand.Text = productToEdit.ProdBrand.ToString();
            txtEditProductSize.Text = productToEdit.ProdSize.ToString();
            txtEditProductPrice.Text = productToEdit.ProdSize.ToString();
        }

        private void BtnEditProductConfirm_Click(object sender, EventArgs e)
        {
            productToEdit.ProdName = txtEditProductName.Text;
            productToEdit.ProdDescription = txtEditProductDescription.Text;
            productToEdit.ProdBrand = txtEditProductBrand.Text;
            productToEdit.ProdSize = decimal.Parse(txtEditProductSize.Text);
            productToEdit.ProdPrice = decimal.Parse(txtEditProductPrice.Text);
            productToEdit.ProdMeasure = txtEditProductMeasurement.Text;
            GlobalConfig.Connection.EditProduct(productToEdit, out string status);
            lblEditProductStatus.Text = status;
        }
    }
}
