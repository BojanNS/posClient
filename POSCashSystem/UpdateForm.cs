using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POSCashSystem
{
    public partial class UpdateForm : Form
    {
        MySqlConnection connection;
        MySqlDataAdapter adapter;
        DataTable dt;
        string connnectionString = ConfigurationManager.ConnectionStrings["POSCashSystem.Properties.Settings.posConnectionString"].ConnectionString;
        public int ItemId;
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            FillColorComboBox();
            DisplayData();

        }

        private void FillColorComboBox()
        {
            comboBoxColor.Items.Add("WHITE");
            comboBoxColor.Items.Add("BLACK");
            comboBoxColor.Items.Add("BLUE");
            comboBoxColor.Items.Add("RED");
            comboBoxColor.Items.Add("GREEN");
            comboBoxColor.Items.Add("PURPLE");
            comboBoxColor.Items.Add("YELLOW");
        }

        private void dataGridViewUpdate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ItemId = Convert.ToInt32(dataGridViewUpdate.Rows[0].Cells[0].Value);
                
                    txtBoxPrizmKod.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["prizm_code"].FormattedValue.ToString();
                    txtBoxBarcode.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["upc"].FormattedValue.ToString();
                    txtBoxName.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                    txtBoxDescription.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["description"].FormattedValue.ToString();
                    comboBoxColor.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["color"].FormattedValue.ToString();
                    txtBoxPrice.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();
                    txtBoxPDV.Text= dataGridViewUpdate.Rows[e.RowIndex].Cells["tax"].FormattedValue.ToString();
                    txtBoxQuantity.Text = dataGridViewUpdate.Rows[e.RowIndex].Cells["stock_in_hand"].FormattedValue.ToString();
                    txtBoxUnitOfMeasure.Text= dataGridViewUpdate.Rows[e.RowIndex].Cells["jedinica_mere"].FormattedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nesto nije u redu! Description: " + ex.Message);
            }
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "update items set prizm_code= ?prizm_code, upc= ?upc, name= ?name, description= ?description , color= ?color, price= ?price, tax= ?tax, stock_in_hand= ?stock_in_hand, jedinica_mere= ?jedinica_mere where id= ?id";
                mySqlCommand.Parameters.AddWithValue("?prizm_code", txtBoxPrizmKod.Text);
                mySqlCommand.Parameters.AddWithValue("?upc", txtBoxBarcode.Text);
                mySqlCommand.Parameters.AddWithValue("?name", txtBoxName.Text);
                mySqlCommand.Parameters.AddWithValue("?description", txtBoxDescription.Text);
                mySqlCommand.Parameters.AddWithValue("?color", comboBoxColor.Text);
                mySqlCommand.Parameters.AddWithValue("?price", txtBoxPrice.Text);
                mySqlCommand.Parameters.AddWithValue("?tax", txtBoxPDV.Text);
                mySqlCommand.Parameters.AddWithValue("?stock_in_hand", txtBoxQuantity.Text);
                mySqlCommand.Parameters.AddWithValue("?jedinica_mere", txtBoxUnitOfMeasure.Text);
                mySqlCommand.Parameters.AddWithValue("?id", this.ItemId);
                mySqlCommand.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("You have successfully updated an item!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while updating item,you must select one item!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "DELETE FROM items WHERE id=?id";
                mySqlCommand.Parameters.AddWithValue("?id", this.ItemId);
                mySqlCommand.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("You have successfully deleted an item!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while deleting item,you must select one item!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplayData()
        {
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT id,prizm_code,upc,name,description,brand_id,color,price,tax,tax_id,stock_in_hand,jedinica_mere,is_active FROM pos.items", connection);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewUpdate.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error while displaying items!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
