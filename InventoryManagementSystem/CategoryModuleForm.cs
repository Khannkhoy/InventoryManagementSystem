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
    public partial class CategoryModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public CategoryModuleForm()
        {
            InitializeComponent();
        }
   

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


       
        public void Clear()
        {
            txtCategoryName.Clear();
        }

        

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryName.Text == "")
                {
                    MessageBox.Show("Please Enter Category Name !");
                    txtCategoryName.Focus();
                }
                else
                {
                    con.Open();
                    cm = new SqlCommand("InsertCategory", con);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@categoryname", txtCategoryName.Text);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been successfully saved.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryName.Text == "")
                {
                    MessageBox.Show("Please Enter Name !");
                    txtCategoryName.Focus();
                }

                else
                {
                    if (MessageBox.Show("Do you want to Update this Category ?", "Record Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cm = new SqlCommand("Update tbCategory set CategoryName = @categoryname where CategoryId like '" + lblCategoryID.Text + "'", con);
                        cm.Parameters.AddWithValue("@categoryname", txtCategoryName.Text);

                        con.Open();
                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Category has been Succesfully Updated.");
                        this.Dispose();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtCategoryName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text))
            {
                e.Cancel = true;
                txtCategoryName.Focus();
                errorProvider1.SetError(txtCategoryName, "Please Enter Category Name!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCategoryName, null);
            }
        }
    }
}
