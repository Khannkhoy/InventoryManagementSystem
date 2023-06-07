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
   
    public partial class frmlogin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        SqlDataAdapter da;

        private string userType;
        public frmlogin()
        {
            InitializeComponent();
            txtUserName.Focus();
        }

        private void CheckboxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxPass.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
                
            }

            else
            {
                txtPassword.UseSystemPasswordChar = false;
               
            }
    
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUserName.Clear();
            txtUserName.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //try
            //{
                con.Open();
            cm = new SqlCommand("Select *from tbUser where UserName=@username and Upass=@upass",con);
                
                cm.Parameters.AddWithValue("@username", txtUserName.Text);
                cm.Parameters.AddWithValue("@upass", txtPassword.Text);
                da = new SqlDataAdapter(cm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                
                if (i == 1)
              
                {
                    dr = cm.ExecuteReader();
                    dr.Read();
               

                if (dr["Usertype"].ToString().Trim() == "Admin")
                {
                    
                    MainForm1 mainform = new MainForm1("A");
                    mainform.checkType = dr["Usertype"].ToString();
                    mainform.name = dr["Ufname"].ToString();
                    mainform.ShowDialog();

                }
                else if (dr["Usertype"].ToString().Trim() == "User")
                {
                    MainForm1 mainform = new MainForm1("U");
                    mainform.checkType = dr["Usertype"].ToString();
                    mainform.name = dr["Ufname"].ToString();
                    mainform.ShowDialog();

                }
               
                con.Close();

                }
                else
                {
                    MessageBox.Show("Invalid Username or Password !", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        

       
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }
    }
}
