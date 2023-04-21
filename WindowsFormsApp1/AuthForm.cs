using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Threading;
namespace WindowsFormsApp1
{
    public partial class AuthForm : Form
    {

        private SqlConnection connection;

        public SqlConnection GetConnection {
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

        private string PerformConnectionString(string name, string code) {
            string cd = $"Data Source=NIKNOTEBOOK;Initial Catalog=Библиотека;User ID={name};Password='{code}';" ;
            return cd;
        }

        public void WhichWindow(string op) {
            this.AutoSize = true;
            if (op == "register") {
                
                regbox.Visible = true;
                regbox.Location = new Point(5, 5);
                regbox.Size = new Size(213, 340);
                
               
            }
            
            else if (op=="authorize"){
                authbox.Visible = true;
                authbox.Location = new Point(5, 5);
                authbox.Size = new Size(213, 340);
                
                
            }
            
            
            this.Show();
        }

        public AuthForm()
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
            //connection = new SqlConnection()
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = PerformConnectionString(textBox3.Text, textBox4.Text);
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ee) {
            
            } 
            try
            {
                connection.Open();
                MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                string connectionString = "Data Source=NIKNOTEBOOK;Initial Catalog=Библиотека;Integrated Security=True";
                string query = $"CREATE LOGIN {textBox2.Text} WITH PASSWORD = '{textBox5.Text}'; CREATE USER {textBox1.Text+"_"+comboBox1.Text} FOR LOGIN {textBox2.Text}; GRANT SELECT ON Авторы TO {textBox2.Text};";//, Жанр, Издательство, Книга
                Console.WriteLine(query);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter qr = new SqlDataAdapter( query,connection);

                }
                MessageBox.Show($"Пользователь {textBox2.Text} успешно зарегистрирован", "RegGud", MessageBoxButtons.OK, MessageBoxIcon.Information);           
            }
            catch(Exception er) {
                MessageBox.Show($"Невозможно добавить такого пользователя\n"+er, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    }
}
