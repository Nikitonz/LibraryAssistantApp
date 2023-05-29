using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        private SqlConnection connection = null;
        private string[] usersAcessLevels = null;

        private static string[] dataSourse = new string[] { "NIKNOTEBOOK", "NIKITPC", "127.0.0.1" };
        private DataTable dataTable;
        public static string GetDataSources
        {
            get
            {
                int usedSorce = 1;

                return dataSourse[usedSorce];
            }
        }
        private enum AcсessLevels
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
        public Form1()
        {

            dataTable = new DataTable();
            this.Size = new Size(200, 200);
            this.Refresh();
            this.AutoSize = true;
            this.PerformAutoScale();



            InitializeComponent();
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;


            вызовТаблицыToolStripMenuItem.Visible = false;
            dataGridView1.Visible = false;
            CheckAcessibility(usersAcessLevels);
            dataGridView1.Location = new Point(241, 34);
            dataGridView1.Size = new Size(585, 286);

        }

        private void SetInvisible()
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox groupBox)
                {
                    groupBox.Visible = false;
                }
            }
        }
        private void SetInvisible(GroupBox groupBoxToKeepVisible)
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox groupBox)
                {
                    if (groupBox != groupBoxToKeepVisible)
                    {
                        groupBox.Visible = false;
                    }
                }
            }
        }
        private void CheckAcessibility(string[] roles)
        {
            AcсessLevels maxRole = AcсessLevels.none;
            if (!(roles is null))
            {

                foreach (string role in roles)
                {
                    if (Enum.TryParse(role, out AcсessLevels parsedRole) && parsedRole < maxRole)
                    {
                        maxRole = parsedRole;
                    }
                }
            }
            void FunctionalityDisable()
            {
                add_author.Enabled = false;
                
                pushAuthor.Enabled = false;
                addIzd.Enabled = false;
                pushIzd.Enabled = false;
                addGenre.Enabled = false;
                genreTran.Enabled = false;
                addBook.Enabled = false;
                pushBook.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView1.Visible = false;



                вызовТаблицыToolStripMenuItem.Visible = false;
                this.MinimumSize = new System.Drawing.Size(260, 48);
            }
            void FunctionalityEnable()
            {
                add_author.Enabled = true;
                search_author.Enabled = true;
                lookForCountry.Enabled = true;
                pushAuthor.Enabled = true;
                addIzd.Enabled = true;
                pushIzd.Enabled = true;
                addGenre.Enabled = true;
                genreTran.Enabled = true;
                addBook.Enabled = true;
                pushBook.Enabled = true;
                //вызовТаблицыToolStripMenuItem.Visible = true;
               // this.MinimumSize = new System.Drawing.Size(380, 46);
            }

            //MessageBox.Show($"{maxRole}", "yes", MessageBoxButtons.OK);
            switch (maxRole)
            {
                case AcсessLevels.db_owner:
                case AcсessLevels.db_datareader:
                case AcсessLevels.db_datawriter:
                    FunctionalityEnable();
                    break;
                default:
                    FunctionalityDisable();
                    break;
            }
            if (maxRole.ToString() != "none")
            {
                statText.Text = $"Авторизирован. Уровень доступа: {maxRole}";
            }
        }


        public void ExecuteQuery(string query)
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
                dataGridView1.Columns[0].ReadOnly = true;



            }
            catch (Exception er) { MessageBox.Show("Ошибка отображения! " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }


        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Developer: Никита Обухов\nemail: nikitoniy2468@gmail.com\nAll rights reserved.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void авторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox1.Visible = true;
            groupBox1.Location = new Point(19, 26);
            groupBox1.Size = new Size(220, 360);
        }
        private void издательстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox2.Visible = true;
            groupBox2.Location = new Point(19, 26);
            groupBox2.Size = new Size(220, 360);
        }

        private void жанрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox3.Visible = true;
            groupBox3.Location = new Point(19, 26);
            groupBox3.Size = new Size(220, 360);
        }

        private void книгаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox4.Visible = true;
            groupBox4.Location = new Point(19, 26);
            groupBox4.Size = new Size(220, 360);
        }
        private void книгиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox4.Visible = true;
            groupBox4.Location = new Point(19, 26);
            groupBox4.Size = new Size(220, 360);
        }

        private void авторыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox1.Visible = true;
            groupBox1.Location = new Point(19, 26);
            groupBox1.Size = new Size(220, 360);
        }

        private void жанрыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox3.Visible = true;
            groupBox3.Location = new Point(19, 26);
            groupBox3.Size = new Size(220, 360);
        }

        private void издательстваToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox2.Visible = true;
            groupBox2.Location = new Point(19, 26);
            groupBox2.Size = new Size(220, 360);
        }
        private void авторизироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthForm authreg = new AuthForm();
            try
            {

                authreg.WhichWindow("authorize");
                authreg.ShowDialog();
                connection = authreg.GetConnection;
                usersAcessLevels = authreg.GetAcessLevels;
                CheckAcessibility(usersAcessLevels);

            }
            catch { };
        }

        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthForm authForm = new AuthForm();
            authForm.WhichWindow("register");
            authForm.ShowDialog();


        }
        private void изПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void изУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Close();
            connection = null;
         
            CheckAcessibility(null);
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






 
        //авторы===========
        private void button1_Click(object sender, EventArgs e)
        {
            string que = "INSERT INTO Авторы (Фамилия, Имя, Отчество, [Страна автора]) VALUES (@Фамилия, @Имя, @Отчество, @Страна)";
            using (SqlCommand command = new SqlCommand(que, connection))
            {
                command.Parameters.AddWithValue("@Фамилия", textBox1.Text);
                command.Parameters.AddWithValue("@Имя", textBox2.Text);
                command.Parameters.AddWithValue("@Отчество", textBox3.Text);
                command.Parameters.AddWithValue("@Страна", textBox4.Text);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    disp_Авторы_Click(sender, e);
                    statText.Text = "Запись успешно добавлена. Используйте \"Создать транзакцию\", чтобы зафиксировать изменения";
                    if (dataGridView1.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView1.Rows.Count - 1;
                        dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView1.Rows[lastIndex].Selected = true;
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить запись.");
                }
            }
        }


        private void search_author_Click(object sender, EventArgs e)
        {
            doSearchRoutine("Авторы", "Фамилия", textBox1.Text);
        }
        
        private void lookForCountry_Click(object sender, EventArgs e)
        {
            doSearchRoutine("Авторы", "Страна автора", textBox4.Text);
        }

        private void pushAuthor_Click(object sender, EventArgs e)
        {
            DoTransactRoutine("Авторы");
        }


        private void disp_Авторы_Click(object sender, EventArgs e)
        {

            DoDisplayRoutine("Авторы");

        }


        //Издательства ========================

        private void addIzd_Click(object sender, EventArgs e)
        {
            string que = "INSERT INTO Издательство VALUES (@Название, @Город, @Адрес)";
            using (SqlCommand command = new SqlCommand(que, connection))
            {
                command.Parameters.AddWithValue("@Название", textBox5.Text);
                command.Parameters.AddWithValue("@Город", textBox6.Text);
                command.Parameters.AddWithValue("@Адрес", textBox7.Text);


                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    dispIzd_Click(sender, e);
                    statText.Text = "Запись успешно добавлена. Используйте \"Создать транзакцию\", чтобы зафиксировать изменения";
                    if (dataGridView1.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView1.Rows.Count - 1;
                        dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView1.Rows[lastIndex].Selected = true;
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить запись.");
                }
            }
        }

        private void pushIzd_Click(object sender, EventArgs e)
        {
            DoTransactRoutine("Издательство");
        }

        private void dispIzd_Click(object sender, EventArgs e)
        {
            DoDisplayRoutine("Издательство");
        }
        //Жанры================================
        private void addGenre_Click(object sender, EventArgs e)
        {
            string que = "INSERT INTO Жанр VALUES (@GENRE)";
            using (SqlCommand command = new SqlCommand(que, connection))
            {
                command.Parameters.AddWithValue("@GENRE", textBox8.Text);


                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    dispGenre_Click(sender, e);
                    statText.Text = "Запись успешно добавлена. Используйте \"Создать транзакцию\", чтобы зафиксировать изменения";
                    if (dataGridView1.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView1.Rows.Count - 1;
                        dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView1.Rows[lastIndex].Selected = true;
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить запись.");
                }
            }
        }
        private void findGenre_Click(object sender, EventArgs e)
        {
            doSearchRoutine("Жанр", "Название жанра", textBox8.Text);
        }
        private void genreTran_Click(object sender, EventArgs e)
        {
            DoTransactRoutine("Жанр");
        }

        private void dispGenre_Click(object sender, EventArgs e)
        {
            DoDisplayRoutine("Жанр");
        }
        //КНИГА=============================

        private void addBook_Click(object sender, EventArgs e)
        {
            string que = "INSERT INTO Книга([Название],[Год выпуска],[Число страниц],[Язык книги],[Цена]) VALUES (@name,@yearIs,@pagesCol,@lang,@moneh)";
            using (SqlCommand command = new SqlCommand(que, connection))
            {
                command.Parameters.AddWithValue("@name", textBox9.Text);
                command.Parameters.AddWithValue("@yearIs", textBox10.Text);
                command.Parameters.AddWithValue("@pagesCol", textBox11.Text);
                command.Parameters.AddWithValue("@lang", textBox12.Text);
                command.Parameters.AddWithValue("@moneh", textBox13.Text);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        dispBook_Click(sender, e);
                        statText.Text = "Запись успешно добавлена. Используйте \"Создать транзакцию\", чтобы зафиксировать изменения";
                        if (dataGridView1.Rows.Count > 0)
                        {
                            int lastIndex = dataGridView1.Rows.Count - 1;
                            dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
                            dataGridView1.Rows[lastIndex].Selected = true;
                        }
                    }
                }catch 
                {
                    MessageBox.Show("Не удалось добавить запись. Проверьте корректность введенных данных");
                }
            }
        }

        private void findBook_Click(object sender, EventArgs e)
        {
            doSearchRoutine("Книга", "Название", textBox9.Text);
        }
        private void pushBook_Click(object sender, EventArgs e)
        {
            DoTransactRoutine("Книга");
        }


        private void dispBook_Click(object sender, EventArgs e)
        {
            DoDisplayRoutine("Книга");
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }

    }
}
