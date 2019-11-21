using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp
{
    public partial class SalesForm : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorLocation = this.PointToClient(System.Windows.Forms.Cursor.Position);

            if (mouseDown && cursorLocation.Y < 20)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        public SalesForm()
        {
            InitializeComponent();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            AddItemForm item = new AddItemForm();
            item.Show();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            CustomersFormOld cust = new CustomersFormOld();
            cust.ShowDialog();
        }

        private void WindowClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowMaximize_Click(object sender, EventArgs e)
        {
            
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
