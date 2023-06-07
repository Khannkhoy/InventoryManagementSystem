using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCname.Text == "")
                {
                    MessageBox.Show("Please Enter Name !");
                    txtCname.Focus();
                }
                else if (txtCphone.Text == "")
                {
                    MessageBox.Show("Please Enter PhoneNumber !");
                    txtCphone.Focus();
                }
                else
                {
                    con.Open();
                    cm = new SqlCommand("InsertCustomer", con);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@cname", txtCname.Text);
                    cm.Parameters.AddWithValue("@cphone", txtCphone.Text);

                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been successfully saved.");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCname.Text == "")
                {
                    MessageBox.Show("Please Enter Name !");
                    txtCname.Focus();
                }
                else if (txtCphone.Text == "")
                {
                    MessageBox.Show("Please Enter PhoneNumber !");
                    txtCphone.Focus();
                }
                else
                {    if (MessageBox.Show("Do you want to Update this Customer ?", "Record Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            cm = new SqlCommand("Update tbCustomer set Cname = @cname, Cphone = @cphone where Cid like '" + lblCid.Text + "'", con);
                            cm.Parameters.AddWithValue("@cname", txtCname.Text);
                            cm.Parameters.AddWithValue("@cphone", txtCphone.Text);
                            con.Open();
                            cm.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Customer has been Succesfully Updated.");
                            this.Dispose();
                        }
                    

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtCname.Clear();
            txtCphone.Clear();
        }

        private void txtCname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCname.Text))
            {
                e.Cancel = true;
                txtCname.Focus();
                errorProvider1.SetError(txtCname, "Please Enter Name!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCname, null);
            }
        }

        private void txtCphone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCphone.Text))
            {
                e.Cancel = true;
                txtCphone.Focus();
                errorProvider2.SetError(txtCphone, "Please Enter PhoneNumber!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(txtCphone, null);
            }
        }

        private void txtCname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCphone.Focus();
            }
        }

        private void txtCphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
    }
}
