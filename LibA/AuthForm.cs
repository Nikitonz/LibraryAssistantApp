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
using System.Threading.Tasks;

namespace LibA
{
    public enum WindowType { 
        REGISTER,
        AUTHORIZE
    }
    public partial class AuthForm : Form
    {
        public event EventHandler RegAuthSuccess;
        //private ProgressBar progressBar;

        List<string> accessLevels= new List<string>();
        public AuthForm(UserPanel userPanel)
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;

        }





        public void WhichWindow(WindowType winType)
        {
            this.AutoSize = true;
            if (winType == WindowType.REGISTER)
            {
                this.AcceptButton = button1;
                authbox.Visible = false;
                regbox.Visible = true;
                regbox.Location = new Point(5, 5);
                this.Text = "Окно регистрации";
                
            }
            else if (winType == WindowType.AUTHORIZE)
            {
                this.AcceptButton = button2;
                regbox.Visible = false;
                authbox.Visible = true;
                authbox.Location = new Point(5, 5);
                this.Text = "Окно авторизации";
                
            }
        }


        /*private void AddProgressBarToRegBox()
        {
            
            progressBar = new ProgressBar();
            progressBar.Visible = false; 
            progressBar.Dock = DockStyle.Bottom; 

           
            progressBar.BackColor = Color.Green; 
            progressBar.ForeColor = Color.White; 

          
            regbox.Controls.Add(progressBar);
        }

        */


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string transfer = textBox1.Text;
                transfer = transfer.Replace(' ', '_').TrimEnd('_');
                transfer += "_" + comboBox1.Text;
                //AddProgressBarToRegBox();
                //await Task.Delay(10000);
                await ConnectionManager.Instance.SendRegData(transfer, textBox2.Text, textBox5.Text);
                ConnectionManager.Instance.SetupConnectionString(textBox2.Text, textBox5.Text);
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

        
        

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionManager.Instance.SetupConnectionString(textBox3.Text, textBox4.Text);
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                {
                    if (connection == null)
                        throw new Exception();
                       
                    if (connection.State is ConnectionState.Open)
                    {
                        MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else throw new Exception();
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
                //MessageBox.Show("Ошибка подключения: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
                textBox4.Text = "";
                textBox3.Focus();
            }
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
