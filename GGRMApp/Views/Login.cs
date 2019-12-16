using GGRMApp.Views;
using GGRMLib;
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
    //Coded by: Cooper Keddy & Macklem Curtis
    //Date: Nov/Dec 2019
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string status;
            int empID = GlobalConfig.Connection.AuthenticateLogin(txtUsername.Text, txtPassword.Text, out status);
            if (empID != -1)
            {
                Main main = new Main(empID);
                main.Show();
                this.Hide();
            } else
            {
                MessageBox.Show(status, "Authentication Result");
            }
            
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        bool mouseDown = false;
        private Point lastLocation;
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorLocation = this.PointToClient(System.Windows.Forms.Cursor.Position);

            if (mouseDown && cursorLocation.Y < 20)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
