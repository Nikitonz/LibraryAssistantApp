using System;
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
               // DialogResult = DialogResult.OK;

                this.Close();//--only if connection establishing was succeed
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //DialogResult = DialogResult.Retry;

                textBox3.Text = "";
                textBox4.Text = "";
                //Thread.Sleep(1000);   --bad, can't manipulate at all
                //button2_Click(sender, e);    --bad, endless cycle, no ability to define new user's input
            }
        }
    }
}
