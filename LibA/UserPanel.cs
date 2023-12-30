using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace LibA
{

    public partial class UserPanel : Form { 
       
        private AuthForm authForm;
        public UserPanel()
        {

            ConnectionManager _ = new();
            this.Size = new Size(200, 200);
            this.Refresh();
            this.AutoSize = false;
         
            InitializeComponent();
            Screen screen = Screen.FromControl(this);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            //администрированиеToolStripMenuItem_Click();
            

        }


        private void ChangeStatus(object sender, EventArgs e)
        {
            
            statText.Text = $"Пользователь вошёл в систему. Роли базы данных:";
        }





        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Developer: Никита Обухов\nemail: nikitoniy2468@gmail.com\nAll rights reserved.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        
        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            authForm = new AuthForm(this);
            authForm.RegAuthSuccess += ChangeStatus;
            authForm.WhichWindow(WindowType.REGISTER);
            authForm.ShowDialog();
            
        }
        private void авторизироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                authForm = new AuthForm(this);
                authForm.RegAuthSuccess += ChangeStatus;
                authForm.WhichWindow(WindowType.AUTHORIZE);
                authForm.ShowDialog();
                
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message,"an error occured. At regauth form" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            
                
           
        }

        private void изПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void изУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (AuthForm.Connection == null) return;
            AuthForm.Connection.Close();
            AuthForm.Disconnect();
            this.statText.Text = "";
         */
            
        }


        

        public void newTSSLabel(string ctext) {
            
            this.statText.Text = ctext;
        }

        

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsPane spane = new SettingsPane();
            spane.Show();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void администрированиеToolStripMenuItem_Click(object sender=null, EventArgs e = null)
        {
            if (AdminPanel.Instance == null || AdminPanel.Instance.IsDisposed)
            {
                AdminPanel admpane = new AdminPanel();
                admpane.Show();
            }
            else
            {
              
                AdminPanel.Instance.Focus();
            }
        }
    }
}
