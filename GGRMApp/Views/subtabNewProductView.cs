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
    public partial class Main : Form
    {
        private void BtnNewProductAdd_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ProdName = txtNewProductName.Text;
            p.ProdDescription = txtNewProductDescription.Text;
            p.ProdBrand = txtNewProductBrand.Text;
            p.ProdPrice = decimal.Parse(txtNewProductPrice.Text);
            p.ProdSize = decimal.Parse(txtNewProductMeasure.Text);
            p.ProdMeasure = txtNewProductMeasure.Text;

            GlobalConfig.Connection.CreateProduct(p, out string status);
            lblNewProductStatus.Text = status;
        }


    }
}
