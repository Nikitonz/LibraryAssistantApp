using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data;
using System.IO.Packaging;

namespace LibA
{
    public enum WindowType { 
        REGISTER,
        AUTHORIZE
    }
    public partial class AuthForm : Form
    {
        public event EventHandler RegAuthSuccess;

      
        List<string> accessLevels= new List<string>();
        public void WhichWindow(WindowType winType)
        {
            this.AutoSize = true;
            if (winType == WindowType.REGISTER)
            {
                authbox.Visible = false;
                regbox.Visible = true;
                regbox.Location = new Point(5, 5);
               
                regbox.AutoSize = true;
                this.Text = "Окно регистрации";
            }
            else if (winType == WindowType.AUTHORIZE)
            {
                regbox.Visible = false;
                authbox.Visible = true;
                authbox.Location = new Point(5, 5);
                this.Text = "Окно авторизации";
            }
        }

        public AuthForm(UserPanel userPanel)
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
          
        }

        


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string transfer = textBox1.Text;
                transfer = transfer.Replace(' ', '_').TrimEnd('_');
                transfer += "_" + comboBox1.Text;
                /*string connectionString = $"Data Source={Properties.Settings.Default.dbConnStrMain};Initial Catalog={Properties.Settings.Default.ICatalog};Integrated Security=True";
                
                string query = $"CREATE LOGIN {textBox2.Text} WITH PASSWORD = '{textBox5.Text}'; " +
                    $"CREATE USER {transfer} FOR LOGIN {textBox2.Text}; " +
                    $"GRANT SELECT ON Авторы TO {textBox2.Text};";

                using (SqlConnection connectionTRUSTABLE = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connectionTRUSTABLE);
                    connectionTRUSTABLE.Open();
                    command.ExecuteNonQueryAsync();
                    connectionTRUSTABLE.Close();
                }
                MessageBox.Show($"Пользователь {textBox2.Text} успешно зарегистрирован", "RegGud", MessageBoxButtons.OK, MessageBoxIcon.Information);
                */
                await ConnectionManager.Instance.SetupConnection(transfer, textBox2.Text, textBox5.Text);
                await ConnectionManager.Instance.OpenConnection();
                RegAuthSuccess?.Invoke(this, EventArgs.Empty);
            }
            catch (SqlException er)
            {
                MessageBox.Show($"Невозможно добавить такого пользователя\n" + er, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WhichWindow(WindowType.AUTHORIZE);
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            AcceptButton = button1;
            AcceptButton = button2;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        

        }
        

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                {
                    
                    MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query = $"USE Библиотека SELECT DP2.name as DatabaseRoleName FROM sys.server_principals AS SP JOIN sys.database_principals AS DP ON SP.sid = DP.sid JOIN sys.database_role_members AS DRM ON DP.principal_id = DRM.member_principal_id JOIN sys.database_principals AS DP2 ON DRM.role_principal_id = DP2.principal_id WHERE SP.name = '{textBox3.Text}' ORDER BY DP2.name;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            

                            while (await reader.ReadAsync()){
                                string roleName = reader["DatabaseRoleName"].ToString();
                                accessLevels.Add(roleName);
                            }


                            RegAuthSuccess?.Invoke(this, EventArgs.Empty);
         
                            
                        }
                    }
                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
                textBox4.Text = "";
                textBox3.Focus();
            }
        }

        internal static void Disconnect()
        {
           

        }
    }
    

    file enum AcсessLevelsPrivelegued
    {
        db_owner,
        db_securityadmin,
        db_accessadmin,
        db_backupoperator,
        db_ddladmin,
        db_datareader,
        db_datawriter,
        db_denydatareader,
        db_denydatawriter,
        db_executor,
        db_owner_sid,
        db_securityadmin_sid,
        db_accessadmin_sid,
        db_backupoperator_sid,
        db_ddladmin_sid,
        db_datareader_sid,
        db_datawriter_sid,
        db_denydatareader_sid,
        none
    }
    

       


    
}
