using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace LibA
{

    public partial class UserPanel : Form { 

        public string statTextedit {
            set { this.statText.Text = value; }
        }
    
            
        private SqlConnection connection = null;
        

        private DataTable dataTable;
        
        
        public UserPanel()
        {

            this.dataTable = new DataTable();
            this.Size = new Size(200, 200);
            this.Refresh();
            this.AutoSize = false;
            //this.PerformAutoScale();
            InitializeComponent();
            Screen screen = Screen.FromControl(this);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            администрированиеToolStripMenuItem_Click();
        }

    
        


        /*public void ExecuteQuery(string query)
        {

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }


        }
        private void doSearchRoutine(string tablename, string field, string qText) {
            string que = $"select * from [{tablename}] where [{field}] like '%{qText}%'";

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(que, connection))
                {
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                }
                dataGridView1.Enabled = true;
                dataGridView1.Visible = true;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка поиска!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


        }
        private void DoTransactRoutine(string tablename)
        {

            if (dataTable != null)
            {


                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = new SqlCommand($"SELECT * FROM {tablename}", connection);
                        adapter.SelectCommand.Transaction = transaction;

                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        adapter.Update(dataTable);
                        transaction.Commit();
                        statText.Text = "Изменения сохранены в базе данных.";

                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Произошла ошибка при сохранении изменений: " + ex.Message);
                }

            }
        }

        private void DoDisplayRoutine(string tablename)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection($"Data Source={GetDataSources};Initial Catalog=Библиотека;Integrated Security=True"))
                {
                    string query = $"SELECT * FROM {tablename}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
                dataGridView1.Enabled = true;
                dataGridView1.Visible = true;
                dataGridView1.Columns[0].Visible = false;



            }
            catch (Exception er) { MessageBox.Show("Ошибка отображения! " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        */


        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Developer: Никита Обухов\nemail: nikitoniy2468@gmail.com\nAll rights reserved.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        
        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthForm authForm = new AuthForm(this);
            authForm.WhichWindow("register");
            authForm.ShowDialog();
            connection = authForm.connection;
        }
        private void авторизироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthForm authForm = new AuthForm(this);
            try
            {
                authForm.WhichWindow("authorize");
                authForm.ShowDialog();
                connection = authForm.connection;
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message,"an error occured. At regautx form" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            
                
           
        }

        private void изПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void изУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection == null) return;
            connection.Close();
            connection = null;
            this.statText.Text = "Status: online";
         
            
        }


        private void проверитьПодключениеКБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    string sql = "SELECT SUSER_SNAME()";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        string userName = command.ExecuteScalar().ToString();

                        MessageBox.Show($"Успешно подключено.\nПользователь: {Environment.UserName};\nКомпьютер: {Environment.MachineName};\nИмя сервера: {connection.DataSource};\nИмя базы данных: {connection.Database};\nВы авторизированы под именем: {userName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else throw new Exception("Вы не авторизированы!");


            }
            catch (Exception ex) { MessageBox.Show("Невозможно подключиться\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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
            AdminPanel admpane = new AdminPanel();
            admpane.Show();
            
        }
    }
}
