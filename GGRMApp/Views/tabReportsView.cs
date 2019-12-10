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
        private void BtnGenerateNewReport_Click(object sender, EventArgs e)
        {
            tcReportsSidebar.SelectedTab = tabReportsGenerate;
        }

        private void BtnGenerateReportBack_Click(object sender, EventArgs e)
        {
            tcReportsSidebar.SelectedTab = tabReportsMain;
        }
    }
}
