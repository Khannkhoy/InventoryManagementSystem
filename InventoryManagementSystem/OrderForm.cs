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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FED6CJ\SQLEXPRESS02;Initial Catalog=dbInventory;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public OrderForm()
        {
            InitializeComponent();
            loadOrder();
        }
        public void loadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("Select OId, Odate, O.Pid,P.PName, O.Cid,C.CName, Qty, Price, Total From tbOrder As O Join tbCustomer As C ON O.Cid=C.Cid Join tbProduct As P ON O.Pid=P.Pid Where CONCAT(OId, Odate, O.Pid,P.PName, O.Cid,C.CName, Qty, Price, Total) lIKE'%"+txtSOrder.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(),Convert.ToDateTime(dr[1].ToString()).ToString("dd/MMM/yyyy") , dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            con.Close();
            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
           OrderModuleForm orderModule = new OrderModuleForm();
            orderModule.ShowDialog();
            loadOrder();
           
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colname = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Do you want to Delete this Order ?", "Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE OId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been Succesfully Deleted.");

                    cm = new SqlCommand("Update tbProduct Set PQty=(PQty+@pqty) Where PId Like '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", con); ;
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[7].Value.ToString()));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }
            }
            loadOrder();
        }

        private void txtSOrder_TextChanged(object sender, EventArgs e)
        {
            loadOrder();
        }
    }
}
