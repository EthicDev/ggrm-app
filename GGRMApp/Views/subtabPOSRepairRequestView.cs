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
    //Coded By: Macklem Curtis & Cooper Keddy
    //Date: Nov/Dec 2019
    public partial class Main : Form
    {
        private void subtabRepairReq_Enter(object sender, EventArgs e)
        {
            string status;
            DataTable dtEquipment = GlobalConfig.Connection.GetEquipmentByCustID(posSelectedCust.ID, out status);
            ddlRepairRequestEquipment.ValueMember = "id";
            ddlRepairRequestEquipment.DisplayMember = "Info";
            ddlRepairRequestEquipment.DataSource = dtEquipment;

            DataTable dtServiceTypes = GlobalConfig.Connection.GetServiceTypesDataTable(out status);
            ddlRepairRequestServiceType.ValueMember = "id";
            ddlRepairRequestServiceType.DisplayMember = "serName";
            ddlRepairRequestServiceType.DataSource = dtServiceTypes;
        }

        private void btnAddServiceRequest_Click(object sender, EventArgs e)
        {
            ServiceOrder serviceOrder = new ServiceOrder();
            serviceOrder.ID = posCurrentOrder.serviceOrders.Count + 1;
            serviceOrder.CustOrdID = posCurrentOrder.ID;
            serviceOrder.RequestingEmpID = 1;
            serviceOrder.EquipID = (int)ddlRepairRequestEquipment.SelectedValue;
            serviceOrder.SerOrdDateIn = dtpRepairRequestDate.Value;
            serviceOrder.SerOrdIssue = txtRepairRequestDescription.Text;
            serviceOrder.SerOrdStatus = "Pending";
            serviceOrder.SerOrdWarranty = cbRepairRequestWarranty.Checked;
            serviceOrder.ServiceID = (int)ddlRepairRequestServiceType.SelectedValue;
            serviceOrder.ServiceName = ddlRepairRequestServiceType.Text;
            posCurrentOrder.serviceOrders.Add(serviceOrder);

            tcPOSSidebar.SelectedTab = subtabPOSButtons;
        }
    }
}
