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
    public partial class CategoryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm()
        {
            InitializeComponent();
            loadCategory();
        }
        public void loadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("Select * from tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }

        

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colname == "Edit")
            {
                CategoryModuleForm CategoryModule = new CategoryModuleForm();
                CategoryModule.lblCategoryID.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                CategoryModule.txtCategoryName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();



                CategoryModule.btnSave.Enabled = false;
                CategoryModule.btnUpdate.Enabled = true;
                CategoryModule.ShowDialog();
            }
            else if (colname == "Delete")
            {
                if (MessageBox.Show("Do you want to Delete this Category ?", "Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE CategoryId LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been Succesfully Deleted.");
                }
            }
            loadCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            
                CategoryModuleForm categoryModule = new CategoryModuleForm();
                categoryModule.btnSave.Enabled = true;
                categoryModule.btnUpdate.Enabled = false;
                categoryModule.ShowDialog();
                loadCategory();
            
        }
    }
}
