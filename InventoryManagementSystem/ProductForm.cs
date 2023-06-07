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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
             cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(PName,PPrice,PDescription,PCategory) LIKE '%"+txtSearch.Text+"%'", con);
           // cm = new SqlCommand("Select * from tbProduct ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductModuleForm ProductModule = new ProductModuleForm();
            ProductModule.btnSave.Enabled = true;
            ProductModule.btnUpdate.Enabled = false;
            ProductModule.ShowDialog();
            LoadProduct();
            
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colname == "Edit")
            {
                ProductModuleForm ProductModule = new ProductModuleForm();
                ProductModule.lblPId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                ProductModule.txtPname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                ProductModule.txtPQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                ProductModule.txtPPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                ProductModule.txtPDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                ProductModule.cmbPcategory.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();



                ProductModule.btnSave.Enabled = false;
                ProductModule.btnUpdate.Enabled = true;
                ProductModule.ShowDialog();
                LoadProduct();
            }
            else if (colname == "Delete")
            {
                if (MessageBox.Show("Do you want to Delete this Product ?", "Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE PId LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been Succesfully Deleted.");
                }
            }
            LoadProduct();
        }

      /* private void txtPSearch_Enter(object sender, EventArgs e)
        {
            if(txtPSearch.Text=="Search Product")
            {
                txtPSearch.Text = "";
                txtPSearch.ForeColor = Color.Black;
            }
        }

          private void txtPSearch_Leave(object sender, EventArgs e)
         {
             if (txtPSearch.Text == "")
             {
                 txtPSearch.Text = "Search Product";
                 txtPSearch.ForeColor = Color.Gray;
                 LoadProduct();
             }

         }*/


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
