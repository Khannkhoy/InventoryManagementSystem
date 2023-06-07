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

namespace InventoryManagementSystem
{
    public partial class MainForm1 : Form
    {

        public string checkType { get; set; }
        public string name { get; set; }
       
        public MainForm1(string type)
        {
            checkType = type;
            InitializeComponent();
        }
        private Form activeform = null;
        private void openChildform(Form childForm)
        {
            if (activeform != null)
                activeform.Close();
            activeform = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanelMainForm.Controls.Add(childForm);
            PanelMainForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            openChildform(new ProductForm());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            openChildform(new CustomerForm());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            openChildform(new CategoryForm());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            openChildform(new Userform());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            openChildform(new OrderForm());
        }

        private void MainForm1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome : " + name + " : " + checkType + " ", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (checkType.Trim() == "Admin")
            {
                guna2Button1.Enabled = true;
                guna2Button2.Enabled = true;
                guna2Button3.Enabled = true;
                guna2Button4.Enabled = true;
                guna2Button5.Enabled = true;
            }
            else if (checkType.Trim() == "User")
            {
                guna2Button1.Enabled = false;
                guna2Button2.Enabled = false;
                guna2Button3.Enabled = false;
                guna2Button4.Enabled = false;
                guna2Button5.Enabled = true;
            }
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);

        }

        //private void guna2Button6_Click(object sender, EventArgs e)
        //{
        //    openChildform(new HomeForm());
        //}

      
    }
}
