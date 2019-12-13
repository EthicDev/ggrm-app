using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        ServiceOrder diagnoseItemList = new ServiceOrder();
        private void SubtabDiagnose_Enter(object sender, EventArgs e)
        {

            dgvDiagnoseItemList.DataSource = diagnoseItemList;

            

            /*
              public int ID { get; set; }

        public string ColItemName { get; set; }

        public string ColItemBrand { get; set; }

        public string ColItemDesc { get; set; }

        public decimal ColPrice { get; set; }

        public int ColStockQuantity { get; set; }

        public int ColOrderQuantity { get; set; }

        public bool ColOrderReq { get; set; }

        public string ColNote { get; set; }

        public int InventoryID { get; set; }

        public int? OrderID { get; set; }

        public int? ProdOrderID { get; set; }

        public int? ServiceOrderID { get; set; } */
        }
        private void BtnDiagnoseAddItem_Click(object sender, EventArgs e)
        {
            OpenItemPicker("diagnose");
            savePreviousTab();
        }
    }
}
