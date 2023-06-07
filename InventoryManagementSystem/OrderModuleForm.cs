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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadProduct();
            loadCustomer();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(PId,PName,PQty,PPrice,PDescription,PCategory) LIKE '%" + txtPSearch.Text + "%'", con);
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
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("Select * from tbCustomer WHERE CONCAT(Cid,CName) LIKE '%" + txtSCustomer.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();

        }
        private void txtSCustomer_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();

        }

        private void txtPSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
       

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPprice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           // GetQty();
            if (Convert.ToInt16(numericUpDown1.Value) > qty)
            {
                MessageBox.Show("Instock Quantity is not Enough !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown1.Value = numericUpDown1.Value - numericUpDown1.Value;
                txtTotal.Clear();
                return;
            }
            if (Convert.ToInt16(numericUpDown1.Value) > 0)
            {
                int total = Convert.ToInt16(txtPprice.Text) * Convert.ToInt16(numericUpDown1.Value);
                txtTotal.Text = total.ToString();
            }
        }

        private void btnOrderSave_Click(object sender, EventArgs e)
        {

            try
            {

                if (txtCName.Text == "")
                {
                    MessageBox.Show("Please Click DataGrideview Customer !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtPName.Text == "")
                {
                    MessageBox.Show("Please Click DataGrideview Product !","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else if (Convert.ToInt16(numericUpDown1.Value) ==0)
                {
                    MessageBox.Show("Please Insert Quantity !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                else
                {
                    cm = new SqlCommand("InsertOrder", con);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@odate", dtOrder.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt16(txtPId.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt16(txtCId.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt16(numericUpDown1.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(txtPprice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt16(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfully saved.");

                    cm = new SqlCommand("Update tbProduct Set PQty=(PQty-@pqty) Where PId Like '" + txtPId.Text + "'", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(numericUpDown1.Value));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCId.Clear();
            txtCName.Clear();
            txtPId.Clear();
            txtPName.Clear();
            dtOrder.Value = DateTime.Now;
            txtPprice.Clear();
            numericUpDown1.Value = 0;
            txtTotal.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void GetQty()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT PQty FROM tbProduct WHERE PId ='" + txtPId.Text + "'", con);
            // cm = new SqlCommand("Select * from tbProduct ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString()) ;
            }
            dr.Close();
            con.Close();
        }
    }
}
