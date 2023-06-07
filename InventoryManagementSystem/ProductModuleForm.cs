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
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            loadCategory();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loadCategory()
        {
            cmbPcategory.Items.Clear();
            cm = new SqlCommand("Select CategoryName from tbCategory ",con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cmbPcategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPname.Text == "")
                {
                    MessageBox.Show("Please Enter ProductName !");
                    txtPname.Focus();
                }
                else if (txtPQuantity.Text == "")
                {
                    MessageBox.Show("Please Enter ProductQty !");
                    txtPQuantity.Focus();
                }
                else if (txtPPrice.Text == "")
                {
                    MessageBox.Show("Please Enter Price !");
                    txtPPrice.Focus();
                }
                else if (cmbPcategory.Text == "")
                {
                    MessageBox.Show("Please Chooses Category !");
                    cmbPcategory.Focus();
                }

                else
                {                        
                        cm = new SqlCommand("InsertProduct", con);
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pname", txtPname.Text);
                        cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQuantity.Text) );
                        cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPPrice.Text));
                        cm.Parameters.AddWithValue("@pdescription", txtPDescription.Text);
                        cm.Parameters.AddWithValue("@pcategory", cmbPcategory.Text);
                        con.Open();
                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Product has been successfully saved.");
                    

                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPname.Text == "")
                {
                    MessageBox.Show("Please Enter ProductName !");
                    txtPname.Focus();
                }
                else if (txtPQuantity.Text == "")
                {
                    MessageBox.Show("Please Enter ProductQty !");
                    txtPQuantity.Focus();
                }
                else if (txtPPrice.Text == "")
                {
                    MessageBox.Show("Please Enter Price !");
                    txtPPrice.Focus();
                }
                else if (cmbPcategory.Text == "")
                {
                    MessageBox.Show("Please Chooses Category !");
                    cmbPcategory.Focus();
                }

                else
                {
                    if (MessageBox.Show("Do you want to Update this Product ?", "Record Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cm = new SqlCommand("Update tbProduct set PName = @pname, PQty = @pqty, PPrice = @pprice, PDescription = @pdescription, PCategory=@pcategory where PId like '" + lblPId.Text + "'", con);

                        cm.Parameters.AddWithValue("@pname", txtPname.Text);
                        cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQuantity.Text));
                        cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPPrice.Text));
                        cm.Parameters.AddWithValue("@pdescription", txtPDescription.Text);
                        cm.Parameters.AddWithValue("@pcategory", cmbPcategory.Text);
                        con.Open();
                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Product has been successfully Updated.");
                        this.Dispose();
                        
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled=true;
            btnUpdate.Enabled=false;
            txtPname.Focus();
        }
        public void Clear()
        {
            txtPname.Clear();
            txtPQuantity.Clear();
            txtPPrice.Clear();
            txtPDescription.Clear();
        }

        private void txtPname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPname.Text))
            {
                e.Cancel = true;
                txtPname.Focus();
                errorProvider1.SetError(txtPname, "Please Enter ProductName!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPname, null);
            }
        }

        private void txtPQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPQuantity.Text))
            {
                e.Cancel = true;
                txtPQuantity.Focus();
                errorProvider2.SetError(txtPQuantity, "Please Enter ProductQTY!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(txtPQuantity, null);
            }
        }

        private void txtPPrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPPrice.Text))
            {
                e.Cancel = true;
                txtPPrice.Focus();
                errorProvider3.SetError(txtPPrice, "Please Enter ProductPrice!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(txtPPrice, null);
            }
        }

        private void cmbPcategory_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPcategory.Text))
            {
                e.Cancel = true;
                cmbPcategory.Focus();
                errorProvider4.SetError(cmbPcategory, "Please Chooses Category!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(cmbPcategory, null);
            }
        }

        private void txtPname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPQuantity.Focus();
            }
        }

        private void txtPQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPPrice.Focus();
            }
        }

        private void txtPPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPDescription.Focus();
            }
        }

        private void txtPDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPcategory.Focus();
            }
        }

        private void cmbPcategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPcategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
    }
}
