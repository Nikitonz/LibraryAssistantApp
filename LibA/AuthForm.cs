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
        public event EventHandler HasRights;
        //private ProgressBar progressBar;

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
                string response = await ConnectionManager.Instance.SendRegData(transfer, textBox2.Text, textBox5.Text);
                if (response == "200")
                {
                    ConnectionManager.Instance.SetupConnectionString(textBox2.Text, textBox5.Text);


                    await ConnectionManager.Instance.OpenConnection();
                    RegAuthSuccess?.Invoke(this, EventArgs.Empty);
                }
                else {
                   
                    throw new Exception($"{response.Split('|')[1]}");
                }
            }
            catch (SqlException er)
            {
                MessageBox.Show($"Невозможно добавить такого пользователя\n" + er.Message, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception er)
            {
                MessageBox.Show($"Невозможно добавить такого пользователя\n" + er.Message, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (connection == null || connection.State != ConnectionState.Open)
                        throw new Exception("Ошибка подключения");
                    MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (await DBWorker.CheckUserRights(await ConnectionManager.Instance.OpenConnection()))
                    {
                        HasRights?.Invoke(this, EventArgs.Empty);
                    }
                    RegAuthSuccess?.Invoke(this, EventArgs.Empty);


                }


                this.Close();


            }
            catch (SqlException sqlex) {
                this.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка подключения: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //textBox3.Text = "";
                textBox4.Text = "";
                textBox3.Focus();
            }
        }

       
    }
    

    
    

       


    
}
