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
        CustomerOrder orderViewing = new CustomerOrder();


        private void SubtabDetailsOrder_Enter(object sender, EventArgs e)
        {
            lblDetailsOrderNumber.Text = orderViewing.OrdNumber.ToString();
            lblDetailsOrderDate.Text = orderViewing.OrdCreationDate.ToString();
            lblDetailsOrderTotal.Text = orderViewing.OrdTotal.ToString();
            lblDetailsOrderPaid.Text = orderViewing.OrdPaid.ToString();
            lblDetailsOrderPayType.Text = orderViewing.PaymentID.ToString();
            lblDetailsOrderCustomer.Text = orderViewing.CustID.ToString();
            lblDetailsOrderEmployee.Text = orderViewing.EmpID.ToString();
        }
    }
}
