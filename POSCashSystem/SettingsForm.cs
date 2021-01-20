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

namespace POSCashSystem
{
    public partial class SettingsForm : Form
    {
        SqlHelper sqlHelper;
        public SettingsForm()
        {
            InitializeComponent();
        }
       
        private void Form3_Load(object sender, EventArgs e)
        {
            cboServer.Items.Add(".");
            cboServer.Items.Add("localhost");
            cboServer.Items.Add("(local)");
            cboServer.Items.Add(Environment.MachineName);

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            string connectionString = string.Format("server={0};user id={1};password={2};persistsecurityinfo=True;database={3};", cboServer.Text, txt_Username.Text, txt_Password.Text, txt_Database.Text);
            try
            {
                sqlHelper = new SqlHelper(connectionString);
                if (sqlHelper.IsConnection)
                {
                    MessageBox.Show("Connection is successfull!");
                }
               
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Connection failed! Description: " + ex.Message);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string connectionString = string.Format("server={0};user id={1};password={2};persistsecurityinfo=True;database={3};", cboServer.Text, txt_Username.Text, txt_Password.Text, txt_Database.Text);
            try
            {
                sqlHelper = new SqlHelper(connectionString);
                if (sqlHelper.IsConnection)
                {
                    AppSetting appSetting = new AppSetting();
                    appSetting.SaveConnectionString("POSCashSystem.Properties.Settings.posConnectionString", connectionString);
                    MessageBox.Show("Your connection string has been successfully changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);                 

                 
                    this.Close();

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Connection failed! Description: " + ex.Message);
            }
            
            
        }
    }
}
