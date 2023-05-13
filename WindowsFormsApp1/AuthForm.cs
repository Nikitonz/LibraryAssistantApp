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

        /*
         using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }
                connection.Close();
            }
        }
         
         
         */

        private string PerformConnectionString(string name, string code)
        {
            string cd = $"Data Source=NIKITPC;Initial Catalog=Библиотека;User ID={name};Password='{code}';";
            return cd;
        }

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
            //connection = new SqlConnection()
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=NIKITPC;Initial Catalog=Библиотека;Integrated Security=True";
                string transfer = textBox1.Text;
                transfer = transfer.Replace(" ", "_");
                transfer += "_" + comboBox1.Text;
                string query = $"CREATE LOGIN {textBox2.Text} WITH PASSWORD = '{textBox5.Text}'; " +
                    $"CREATE USER {transfer} FOR LOGIN {textBox2.Text}; " +
                    $"GRANT SELECT ON Авторы TO {textBox2.Text};";//, Жанр, Издательство, Книга

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

            // Добавить элементы в ComboBox

        }

       

        private async void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string connectionString = PerformConnectionString(textBox3.Text, textBox4.Text);
                connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                using (SqlCommand command = new SqlCommand($"SELECT DP1.name AS DatabaseRoleName FROM sys.database_role_members AS DRM RIGHT OUTER JOIN sys.database_principals AS DP1  ON DRM.role_principal_id = DP1.principal_id LEFT OUTER JOIN sys.database_principals AS DP2 ON DRM.member_principal_id = DP2.principal_id WHERE DP2.name = '{textBox3.Text}' -- Имя пользователя AND DP1.type = 'R' ORDER BY DP1.name;", connection))
                {
                    int i;
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
                        else
                            MessageBox.Show($"0 rows", "test", MessageBoxButtons.OK,MessageBoxIcon.Hand);
                    }
                    Console.WriteLine();
                    MessageBox.Show($"text {acessLevels.ToString()}", "test", MessageBoxButtons.OK);

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
              
            }
        }

        private void authbox_Enter(object sender, EventArgs e)
        {

        }
    }
}
