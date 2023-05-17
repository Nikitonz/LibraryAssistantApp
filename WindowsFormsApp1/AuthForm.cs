using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class AuthForm : Form
    {

        private SqlConnection connection;

        public SqlConnection GetConnection
        {
            get { return connection; }

        }
        private List<string> acessLevels = new List<string>();
        public string[] GetAcessLevels {
            get { return acessLevels.ToArray(); }
        }
        private string DateSourceBind ;


        

        public void WhichWindow(string op)
        {
            this.AutoSize = true;
            if (op == "register")
            {

                regbox.Visible = true;
                regbox.Location = new Point(5, 5);
                regbox.Size = new Size(213, 340);


            }

            else if (op == "authorize")
            {
                authbox.Visible = true;
                authbox.Location = new Point(5, 5);
                authbox.Size = new Size(213, 340);


            }



        }

        public AuthForm()
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
            DateSourceBind = Form1.GetDataSources;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = $"Data Source={DateSourceBind};Initial Catalog=Библиотека;Integrated Security=True";
                string transfer = textBox1.Text;
                transfer = transfer.Replace(" ", "_");
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

       

        private async void button2_Click(object sender, EventArgs e)
        {
            string PerformConnectionString(string name, string code)
            {

                string cd = $"Data Source={DateSourceBind};Initial Catalog=Библиотека;User ID={name};Password='{code}';";
                return cd;
            }
            try
            {
                string connectionString = PerformConnectionString(textBox3.Text, textBox4.Text);
                connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                using (SqlCommand command = new SqlCommand($"USE Библиотека SELECT DP2.name as DatabaseRoleName FROM sys.server_principals AS SP JOIN sys.database_principals AS DP ON SP.sid = DP.sid JOIN sys.database_role_members AS DRM ON DP.principal_id = DRM.member_principal_id JOIN sys.database_principals AS DP2 ON DRM.role_principal_id = DP2.principal_id WHERE SP.name = '{textBox3.Text}' ORDER BY DP2.name;", connection))
                {
                   
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string roleName = reader["DatabaseRoleName"].ToString();
                                acessLevels.Add(roleName);
                            }
                        }
                        
                    }
                    Console.WriteLine();
                    

                }
                DialogResult = DialogResult.OK;
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

        private void authbox_Enter(object sender, EventArgs e)
        {

        }
    }
}
