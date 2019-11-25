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
using GGRMLib.DataSet_CustomersTableAdapters;
using GGRMLib.DataAccess;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void tabPOS_Enter(object sender, EventArgs e)
        {
            Customer selectedCust = (Customer)GlobalData.ViewData["posSelectedCustomer"];
            lblSelectedCustomer.Text = selectedCust.CustFirst ?? "No customer selected.";
        }
    }
}
