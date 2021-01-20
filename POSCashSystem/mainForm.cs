using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace POSCashSystem
{
    public partial class MainForm : Form
    {
        DataTable dt;
        DataTable dt2;
        string connnectionString = ConfigurationManager.ConnectionStrings["POSCashSystem.Properties.Settings.posConnectionString"].ConnectionString;
        MySqlConnection connection;
        MySqlDataAdapter adapter;
        List<Product> products = new List<Product>();
        int rowIndex = 0;
        HelpForm helpForm = new HelpForm();
        SettingsForm connectionForm = new SettingsForm();
        NewItemForm newItemForm = new NewItemForm();
        UpdateForm updateForm = new UpdateForm();

        public MainForm()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT id, name, description, price FROM pos.items", connection);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed! Description: " + ex.Message);

            }


        }
        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT id, name, description, price FROM pos.items WHERE name LIKE '" + textBox_Search.Text + "%'", connection);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed! Description: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;

                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["description"].FormattedValue.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString() + "$";
                    textBox6.Text = "1";

                    rowIndex = e.RowIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nesto nije u redu! Description: " + ex.Message);
            }
        }
        private void btn_AddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                string name = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                string price = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();

                products.Add(new Product(name, price));
                dt2 = new DataTable();
                dt2.Columns.Add("Name");
                dt2.Columns.Add("Price");

                for (int i = 0; i < products.Count; i++)
                {
                    dt2.Rows.Add(products[i].Name, products[i].Price);
                }

                dataGridView2.DataSource = dt2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong! Description: " + ex.Message);
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                dataGridView2.Rows.RemoveAt(rowIndex);
                products.RemoveAt(rowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Removing item was unsuccessful! Description: " + ex.Message);
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {
            helpForm.ShowDialog();
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            connectionForm.ShowDialog();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            newItemForm.ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateForm.ShowDialog();
        }
    }
}
