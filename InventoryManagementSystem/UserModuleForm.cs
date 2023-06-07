using System;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class UserModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        double val = 0;
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                try
                {
                
                if (txtUname.Text == "")
                {
                    MessageBox.Show("Please Enter UserName!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUname.Focus();
                }
                else if (txtUfname.Text == "")
                {
                    MessageBox.Show("Please Enter FullName!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUfname.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                }
                else if (txtRpassword.Text == "")
                {
                    MessageBox.Show("Please Enter Re-Type Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRpassword.Focus();
   
                }
                else if (txtPassword.Text != txtRpassword.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRpassword.Focus();
                    return;
                }
                else if (txtUphone.Text == "")
                {
                    MessageBox.Show("Please Enter UserPhone!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUphone.Focus();
                }
                else
                {
                    con.Open();
                    SqlCommand CommandtoCheckUsername = new SqlCommand(" Select UserName from tbUser where UserName = '" + txtUname.Text + "'", con);
                    SqlDataAdapter sd = new SqlDataAdapter(CommandtoCheckUsername);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                   
                    if(dt.Rows.Count > 0)
                    {
                        MessageBox.Show("UserName has Already", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                       
                        con.Close();
                        txtUname.Focus();

                    }
                    else
                    {

                        cm = new SqlCommand("InsertUser", con);
                        cm.CommandType = CommandType.StoredProcedure;
                       
                        cm.Parameters.AddWithValue("@username", txtUname.Text);
                        cm.Parameters.AddWithValue("@usertype", cmbUserType.Text);
                        cm.Parameters.AddWithValue("@ufname", txtUfname.Text);
                        cm.Parameters.AddWithValue("@upass", txtPassword.Text);
                        cm.Parameters.AddWithValue("@uphone", txtUphone.Text);

                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("User has been successfully saved.");
                 

                   
                    }
                        
                  
                    
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void Clear()
        {
            //txtID.Clear();
            txtUname.Clear();
            txtUfname.Clear();
            txtPassword.Clear();
            txtRpassword.Clear();
            txtUphone.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtUname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUname.Text))
            {
                e.Cancel = true;
                txtUname.Focus();
                errorProvider1.SetError(txtUname, "Please Enter UserName!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUname, null);
            }
        }

        private void txtUfname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUfname.Text))
            {
                e.Cancel = true;
                txtUfname.Focus();
                errorProvider2.SetError(txtUfname, "Please Enter FullName!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(txtUfname, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider3.SetError(txtPassword, "Please Enter Password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(txtPassword, null);
            }
        }
        private void txtRpassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRpassword.Text))
            {
                e.Cancel = true;
                txtRpassword.Focus();
                errorProvider4.SetError(txtRpassword, "Please Enter Re-Type Password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(txtRpassword, null);
            }
        }

        private void txtUphone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUphone.Text))
            {
                e.Cancel = true;
                txtUphone.Focus();
                errorProvider5.SetError(txtUphone, "Please Enter UserPhone!");
            }
            else
            {
                e.Cancel = false;
                errorProvider5.SetError(txtUphone, null);
            }
        }

        private void txtUname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUfname.Focus();
            }
        }

        private void txtUfname_KeyDown(object sender, KeyEventArgs e)
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
                txtRpassword.Focus();
            }
        }
        private void txtRpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUphone.Focus();
            }
        }

        private void txtUphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.PerformClick();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                 if (txtUfname.Text == "")
                {
                    MessageBox.Show("Please Enter FullName!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUfname.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                }
                else if (txtRpassword.Text == "")
                {
                    MessageBox.Show("Please Enter Re-Type Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRpassword.Focus();

                }
                else if (txtPassword.Text != txtRpassword.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRpassword.Focus();
                    return;
                }
                else if (txtUphone.Text == "")
                {
                    MessageBox.Show("Please Enter UserPhone!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUphone.Focus();
                }
                else
                {
                    if (MessageBox.Show("Do you want to Update this User ?", "Record Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cm = new SqlCommand("Update tbUser set Ufname = @ufname, Upass = @upass, Uphone = @uphone where UserName like '" + txtUname.Text + "'", con);
                        cm.Parameters.AddWithValue("@ufname", txtUfname.Text);
                        cm.Parameters.AddWithValue("@upass", txtPassword.Text);
                        cm.Parameters.AddWithValue("@uphone", txtUphone.Text);
                        con.Open();
                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("User has been Succesfully Updated.");
                        this.Dispose();
                    }
                }
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
