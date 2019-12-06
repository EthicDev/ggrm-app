using GGRMLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGRMApp
{
    public static class GlobalData
    {
        internal static Hashtable ViewData = new Hashtable();

        public static CustomerOrder posCurrentOrder;

        static GlobalData()
        {
            InitializePOS();
        }

        static void InitializePOS()
        {
            
            ViewData.Add("posSelectedCustomer", new Customer());
            ViewData.Add("editSelectedCustomer", new Customer());
            ViewData.Add("editSelectedCustomerID", 0);
            ViewData.Add("posCurrentOrder", new CustomerOrder());
            ViewData.Add("posLineItems", new List<OrderLine>());
            ViewData.Add("posAddedInvItems", new List<Inventory>());

        }
        
    }
}
