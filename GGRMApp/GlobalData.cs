using GGRMLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGRMApp
{
    internal static class GlobalData
    {
        internal static Hashtable ViewData = new Hashtable();
        
        static GlobalData()
        {
            InitializePOS();
        }

        static void InitializePOS()
        {
            ViewData.Add("posSelectedCustomer", new Customer());
            ViewData.Add("editSelectedCustomer", new Customer());
            ViewData.Add("editSelectedCustomerID", 0);
        }
        
    }
}
