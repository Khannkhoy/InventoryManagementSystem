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
    public partial class Userform : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Userform()
        {
            InitializeComponent();
            loadUser();
        }
        public void loadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("Select * from tbUser",con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserModuleForm userModule = new UserModuleForm();
            userModule.btnSave.Enabled = true;
            userModule.btnUpdate.Enabled = false;
            userModule.ShowDialog();
            loadUser();
                
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            string colname = dgvUser.Columns[e.ColumnIndex].Name;
            if(colname == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.txtUname.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.cmbUserType.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtUfname.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtPassword.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();
                userModule.txtUphone.Text = dgvUser.Rows[e.RowIndex].Cells[5].Value.ToString();

                userModule.btnSave.Enabled = false;
                userModule.btnUpdate.Enabled = true;
                userModule.txtUname.Enabled = false;
                userModule.ShowDialog();
            }
            else if (colname == "Delete"){
                if (MessageBox.Show("Do you want to Delete this User ?", "Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE UserName LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been Succesfully Deleted.");
                }
            }
            loadUser();
        }
    }
}
