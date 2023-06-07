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
    public partial class CustomerForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            loadCustomer();
        }
        public void loadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("Select * from tbCustomer", con);
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
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerModuleForm customerModule = new CustomerModuleForm();
            customerModule.btnSave.Enabled = true;
            customerModule.btnUpdate.Enabled = false;
            customerModule.ShowDialog();
            loadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colname == "Edit")
            {
                CustomerModuleForm CustomerModule = new CustomerModuleForm();
                CustomerModule.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                CustomerModule.txtCname.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                CustomerModule.txtCphone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();


                CustomerModule.btnSave.Enabled = false;
                CustomerModule.btnUpdate.Enabled = true;
                CustomerModule.ShowDialog();
            }
            else if (colname == "Delete")
            {
                if (MessageBox.Show("Do you want to Delete this Customer ?", "Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE Cid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been Succesfully Deleted.");
                }
            }
            loadCustomer();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
