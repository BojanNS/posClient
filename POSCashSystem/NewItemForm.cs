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
    public partial class NewItemForm : Form
    {
        MySqlConnection connection;
        MySqlDataAdapter adapter;
        DataTable dt;
        string connnectionString = ConfigurationManager.ConnectionStrings["POSCashSystem.Properties.Settings.posConnectionString"].ConnectionString;
        bool isActive = false;
        public NewItemForm()
        {
            InitializeComponent();
        }
        private void NewItemForm_Load(object sender, EventArgs e)
        {
            FillCategoriesComboBox();
            FillColorComboBox();
            FillBrandComboBox();
            
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            string imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
                
                if(dialog.ShowDialog() ==  System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    image1.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtBoxPrizmKod_Enter(object sender, EventArgs e)
        {
            string text = "Prizm kod";
            PlaceHolderLogicEnterTextBox(txtBoxPrizmKod, text);
        }

        private void txtBoxPrizmKod_Leave(object sender, EventArgs e)
        {
            string text = "Prizm kod";
            PlaceHolderLogicLeaveTextBox(txtBoxPrizmKod, text);
        }

        private void txtBoxBarcode_Enter(object sender, EventArgs e)
        {
            string text = "Barcode";
            PlaceHolderLogicEnterTextBox(txtBoxBarcode, text);
        }

        private void txtBoxBarcode_Leave(object sender, EventArgs e)
        {
            string text = "Barcode";
            PlaceHolderLogicLeaveTextBox(txtBoxBarcode, text);
        }
        private void txtBoxName_Enter(object sender, EventArgs e)
        {
            string text = "Name";
            PlaceHolderLogicEnterTextBox(txtBoxName, text);
        }

        private void txtBoxName_Leave(object sender, EventArgs e)
        {
            string text = "Name";
            PlaceHolderLogicLeaveTextBox(txtBoxName, text);
        }

        private void txtBoxDescription_Enter(object sender, EventArgs e)
        {
            string text = "Description";
            PlaceHolderLogicEnterTextBox(txtBoxDescription, text);
        }

        private void txtBoxDescription_Leave(object sender, EventArgs e)
        {
            string text = "Description";
            PlaceHolderLogicLeaveTextBox(txtBoxDescription, text);
        }
        private void comboBoxBrand_Enter(object sender, EventArgs e)
        {
            string text = "Brand";
            PlaceHolderLogicEnterComboBox(comboBoxBrand, text);
        }

        private void comboBoxBrand_Leave(object sender, EventArgs e)
        {
            string text = "Brand";
            PlaceHolderLogicLeaveComboBox(comboBoxBrand, text);
        }

        private void comboBoxCategory_Enter(object sender, EventArgs e)
        {
            string text = "Category";
            PlaceHolderLogicEnterComboBox(comboBoxCategory, text);
        }

        private void comboBoxCategory_Leave(object sender, EventArgs e)
        {
            string text = "Category";
            PlaceHolderLogicLeaveComboBox(comboBoxCategory, text);
        }

        private void comboBoxColor_Enter(object sender, EventArgs e)
        {
            string text = "Color";
            PlaceHolderLogicEnterComboBox(comboBoxColor, text);
        }

        private void comboBoxColor_Leave(object sender, EventArgs e)
        {
            string text = "Color";
            PlaceHolderLogicLeaveComboBox(comboBoxColor, text);
        }



        private void PlaceHolderLogicEnterTextBox(TextBox textBox, string text)//pomocna metoda za enter event: textbox 
        {
            if (textBox.Text == text)
                textBox.Text = "";

            textBox.ForeColor = Color.Black;
        }
        private void PlaceHolderLogicLeaveTextBox(TextBox textBox, string text)// pomocna metoda za leave event: textbox
        {
            if (textBox.Text == "")
                textBox.Text = text;

            textBox.ForeColor = Color.DarkGray;
        }
        private void PlaceHolderLogicEnterComboBox(ComboBox comboBox, string text)
        {
            if (comboBox.Text == text)
                comboBox.Text = "";

            comboBox.ForeColor = Color.Black;
        }//pomocna metoda za enter event: combobox
        private void PlaceHolderLogicLeaveComboBox(ComboBox comboBox, string text)
        {
            if (comboBox.Text == "")
                comboBox.Text = text;

            comboBox.ForeColor = Color.DarkGray;
        }//pomocna metoza za enter event: combobox
        private void FillCategoriesComboBox()
        {
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT name FROM pos.categories", connection);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    comboBoxCategory.Items.Add(row["name"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void FillBrandComboBox()
        {
            comboBoxBrand.Items.Add("SAMSUNG");
            comboBoxBrand.Items.Add("ACER");
            comboBoxBrand.Items.Add("APPLE");
            comboBoxBrand.Items.Add("DELL");
            comboBoxBrand.Items.Add("LENOVO");
            comboBoxBrand.Items.Add("HP");
        }
        private void RestartInformationAboutItemWhennSuccessfullyAdded()
        {
            txtBoxPrizmKod.Text = "Prizm kod";
            txtBoxBarcode.Text = "Barcode";
            txtBoxDescription.Text = "Description";
            txtBoxName.Text = "Name";
            comboBoxBrand.Text = "Brand";
            comboBoxCategory.Text = "Category";
            comboBoxColor.Text = "Color";
            txtBoxPrice.Text = string.Empty;
            txtBoxPDV.Text = string.Empty;
            comboBoxTaxCode.Text = string.Empty;
            txtBoxQuantity.Text = string.Empty;
            txtBoxUnitOfMeasure.Text = string.Empty;


        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            bool isSuccessfull = false;
            try
            {
                connection = new MySqlConnection(connnectionString);
                connection.Open();
                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "INSERT INTO items(prizm_code,upc,name,description,brand_id,color,price,tax,tax_id,stock_in_hand,jedinica_mere,is_active) VALUES(?prizm_code, ?upc, ?name, ?description, ?brand_id, ?color, ?price, ?tax, ?tax_id, ?stock_in_hand, ?jedinica_mere, ?is_active)";
                mySqlCommand.Parameters.AddWithValue("?prizm_code", txtBoxPrizmKod.Text);
                mySqlCommand.Parameters.AddWithValue("?upc", txtBoxBarcode.Text);
                mySqlCommand.Parameters.AddWithValue("?name", txtBoxName.Text);
                mySqlCommand.Parameters.AddWithValue("?description", txtBoxDescription.Text);
                mySqlCommand.Parameters.AddWithValue("?brand_id", 1);
                mySqlCommand.Parameters.AddWithValue("?color", comboBoxColor.Text);
                mySqlCommand.Parameters.AddWithValue("?price", txtBoxPrice.Text);
                mySqlCommand.Parameters.AddWithValue("?tax", txtBoxPDV.Text);
                mySqlCommand.Parameters.AddWithValue("?tax_id", comboBoxTaxCode.Text);
                mySqlCommand.Parameters.AddWithValue("?stock_in_hand", txtBoxQuantity.Text);
                mySqlCommand.Parameters.AddWithValue("?jedinica_mere", txtBoxUnitOfMeasure.Text);
                if (isActive)
                    mySqlCommand.Parameters.AddWithValue("?is_active", 1);
                else
                    mySqlCommand.Parameters.AddWithValue("?is_active", 0);
                mySqlCommand.ExecuteNonQuery();
                connection.Close();
                isSuccessfull = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while adding item!", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
            if (isSuccessfull)
            {
                MessageBox.Show("You have successfully added item!", "SUCCESS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestartInformationAboutItemWhennSuccessfullyAdded();
            }
            

        }

        private void btnIsActive_Click(object sender, EventArgs e)
        {
            if (isActive == false)
            {
                isActive = true;
                btnIsActive.BackColor = Color.DarkTurquoise;
                btnIsActive.Text = "Active";
            }
            else if (isActive == true)
            {
                isActive = false;
                btnIsActive.BackColor = Color.Red;
                btnIsActive.Text = "Is Active?";
            }
        }
        
    }
}
