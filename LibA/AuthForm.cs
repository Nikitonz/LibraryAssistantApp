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
using System.Security.Cryptography;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LibA
{
    public enum WindowType { 
        REGISTER,
        AUTHORIZE
    }
    public partial class AuthForm : Form
    {
        public event EventHandler RegAuthSuccess;
        public delegate void RightsType(int type);
        public event RightsType HasRights;
        

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
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string transfer = textBox1.Text;
                transfer = transfer.Replace(' ', '_').TrimEnd('_');
                
                string response = await ConnectionManager.Instance.SendRegData(transfer, textBox2.Text, textBox5.Text);
                if (response is null)
                    throw new Exception("Похоже, сервис регистрации недоступен. Попробуйте позже");
                if (response == "200")
                {
                    bool ifvalid = ConnectionManager.Instance.SetupConnectionString();
                    if (ifvalid) { RegAuthSuccess?.Invoke(this, EventArgs.Empty); }
                    
                }
                else {
                   
                    throw new Exception($"{response.Split('|')[1]}");
                }
            }
            catch (SqlException er)
            {
                MessageBox.Show($"Невозможно добавить пользователя\n" + er.Message, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception er)
            {
                MessageBox.Show($"Невозможно добавить пользователя\n" + er.Message, "RegErr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WhichWindow(WindowType.AUTHORIZE);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WhichWindow(WindowType.REGISTER);
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionManager.Instance.SetupConnectionString();
                string saltFromServer = await GetSaltFromServer(textBox3.Text);

               
                if (saltFromServer == string.Empty)
                    throw new Exception("Пользователь с таким логином не найден");
                string hashedPassword = HashPassword(textBox4.Text, saltFromServer);
       
                int userRights = await DBWorker.ExecuteFunction("CheckUserRights", textBox3.Text, hashedPassword);
                if (userRights >= 0)
                {
                    switch (userRights) {
                        case 0:
                            ConnectionManager.Instance.SetupConnectionString("libadmin", "33223311");
                            break;
                        case 1:
                            ConnectionManager.Instance.SetupConnectionString("librarian", "123321");
                            break;
                        default:
                            ConnectionManager.Instance.SetupConnectionString();
                            break;

                    }
                    HasRights?.Invoke(userRights);
                    RegAuthSuccess?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    switch (userRights)
                    {
                        case -1:
                            throw new Exception("Пользователь с таким логином не найден");
                        case -2:
                            throw new Exception("Пароль неверный");
                        case -5:
                            MessageBox.Show("Требуется сменить пароль. Введите новый пароль.");
                            string newPassword = PromptForNewPassword();
                            if (string.IsNullOrEmpty(newPassword))
                                throw new Exception("Новый пароль не может быть пустым.");

                            if (newPassword == textBox4.Text)
                                throw new Exception("Новый пароль не должен совпадать с текущим.");

                            string newSalt = GenerateSalt();
                            string newHashedPassword = HashPassword(newPassword, newSalt);

                            await DBWorker.ExecProcedure(
                                "SetNewPass",
                                new SqlParameter("@username", textBox3.Text),
                                new SqlParameter("@hashedpass", newHashedPassword),
                                new SqlParameter("@newsalt", newSalt)
);
                            MessageBox.Show("Пароль успешно изменен. Пожалуйста, войдите с новым паролем.");
                            textBox4.Text = "";
                            textBox3.Focus();
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}");
                textBox4.Text = "";
                textBox3.Focus();
            }
        }


        private string PromptForNewPassword()
        {
            // Открываем диалоговое окно для ввода нового пароля
            using (var form = new Form())
            {
                form.AutoSize = true;
                form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                form.MinimumSize = new Size(200, 100);

                var label = new Label
                {
                    Text = "Введите новый пароль",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(10),
                    AutoSize = true
                };

                var textBox = new TextBox
                {
                    UseSystemPasswordChar = true,
                    Dock = DockStyle.Top,
                    Margin = new Padding(10)
                };

                var buttonOk = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Dock = DockStyle.Bottom,
                    Margin = new Padding(10)
                };

                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };

                panel.Controls.Add(textBox);
                panel.Controls.Add(label);

                form.Controls.Add(panel);
                form.Controls.Add(buttonOk);

                form.AcceptButton = buttonOk;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    return textBox.Text;
                }
            }
            return null;
        }

        private string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hashBytes);
            }
        }
        private async Task<string> GetSaltFromServer(string username)
        {
            string salt = "";
            try
            {
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                {
                    

                    using (SqlCommand command = new SqlCommand("GetSaltForUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);

                        // Выполняем запрос и получаем соль
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            salt = reader["Salt"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при получении соли
                Console.WriteLine(ex.Message);
            }
            
            return salt;
        }

    }
}
