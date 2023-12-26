using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data;

namespace LibA
{

    public partial class AuthForm : Form
    {
        public SqlConnection connection;
        UserPanel userPanel;
        List<string> accessLevels= new List<string>();
        public void WhichWindow(string op)
        {
            this.AutoSize = true;
            if (op == "register")
            {
                authbox.Visible = false;
                regbox.Visible = true;
                regbox.Location = new Point(5, 5);
                //regbox.Size = new Size(213, 340);
                regbox.AutoSize = true;
                this.Text = "Окно регистрации";
            }
            else if (op == "authorize")
            {
                regbox.Visible = false;
                authbox.Visible = true;
                authbox.Location = new Point(5, 5);
                //authbox.Size = new Size(213, 340);
                this.Text = "Окно авторизации";
            }
        }

        public AuthForm(UserPanel userPanel)
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
            this.userPanel = userPanel;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = $"Data Source={Properties.Settings.Default.dbConnStrMain};Initial Catalog=Библиотека;Integrated Security=True";
                string transfer = textBox1.Text;
                transfer = transfer.Replace(' ', '_').TrimEnd('_');
                transfer += "_" + comboBox1.Text;
                string query = $"CREATE LOGIN {textBox2.Text} WITH PASSWORD = '{textBox5.Text}'; " +
                    $"CREATE USER {transfer} FOR LOGIN {textBox2.Text}; " +
                    $"GRANT SELECT ON Авторы TO {textBox2.Text};";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQueryAsync();
                    connection.Close();
                }
                MessageBox.Show($"Пользователь {textBox2.Text} успешно зарегистрирован", "RegGud", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException er)
            {
                MessageBox.Show($"Невозможно добавить такого пользователя\n" + er, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WhichWindow("authorize");
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            AcceptButton = button1;
            AcceptButton = button2;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        

        }
        //$"USE Библиотека SELECT DP2.name as DatabaseRoleName FROM sys.server_principals AS SP JOIN sys.database_principals AS DP ON SP.sid = DP.sid JOIN sys.database_role_members AS DRM ON DP.principal_id = DRM.member_principal_id JOIN sys.database_principals AS DP2 ON DRM.role_principal_id = DP2.principal_id WHERE SP.name = '{textBox3.Text}' ORDER BY DP2.name;", connection)



        /*private async void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                string connectionString = $"Data Source={dateSource};Initial Catalog=Библиотека;User ID={textBox3.Text};Password='{textBox4.Text}';"; ;
                connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userPanel.statText.Text = $"Пользователь {textBox3.Text}. Уроверь доступа: не реалтзовано";
                List<string> resultList = 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //DialogResult = DialogResult.Retry;

                textBox3.Text = "";
                textBox4.Text = "";
                textBox3.Focus();
            }
        }
        */

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = $"Data Source={Properties.Settings.Default.dbConnStrMain};Initial Catalog=Библиотека;User ID={textBox3.Text};Password='{textBox4.Text}';";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query = $"USE Библиотека SELECT DP2.name as DatabaseRoleName FROM sys.server_principals AS SP JOIN sys.database_principals AS DP ON SP.sid = DP.sid JOIN sys.database_role_members AS DRM ON DP.principal_id = DRM.member_principal_id JOIN sys.database_principals AS DP2 ON DRM.role_principal_id = DP2.principal_id WHERE SP.name = '{textBox3.Text}' ORDER BY DP2.name;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            

                            while (await reader.ReadAsync())
                            {
                                string roleName = reader["DatabaseRoleName"].ToString();
                                accessLevels.Add(roleName);
                            }

                           

                            userPanel.statText.Text = $"Пользователь {textBox3.Text}. Роли базы данных: {string.Join(", ", accessLevels)}";
                            this.Close();
                        }
                    }
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
