using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;


namespace LibA
{

    public partial class UserPanel : Form
    {

        private AuthForm authForm;
        public UserPanel()
        {

            this.Size = new Size(200, 200);
            this.Refresh();
            this.AutoSize = false;
            InitializeComponent();
            Screen screen = Screen.FromControl(this);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            ConnectionManager.Instance.Disconnection += (sender, e) =>
            {
                администрированиеToolStripMenuItem.Visible = false;
                книжныйФондToolStripMenuItem.Visible = false;
                поискToolStripMenuItem1.Visible = false;
                пользователиToolStripMenuItem.Visible = false;
                searchPanel.Visible = false;
                this.statText.Text = "Вы не вошли в систему";
                this.statText.Image = LibA.Properties.Resources.error;
            };
            авторизироватьсяToolStripMenuItem_Click(null, null);


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
            authForm.RegAuthSuccess -= ChangeStatus;

        }
        private void авторизироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {


            authForm = new AuthForm(this);
            authForm.RegAuthSuccess += ChangeStatus;
            authForm.HasRights += (id) =>
            {
                switch (id)
                {
                    case 0:
                        администрированиеToolStripMenuItem.Visible = true;
                        goto case 1;
                   
                    case 1:
                        пользователиToolStripMenuItem.Visible = true;
                        книжныйФондToolStripMenuItem.Visible = true;
                        break;
                }

            };
            authForm.WhichWindow(WindowType.AUTHORIZE);
            authForm.ShowDialog();
            authForm.RegAuthSuccess -= ChangeStatus;
        }

        private void изПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void изУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Disconnect();
           
            //ConnectionManager.Instance.SetupConnectionString("Guest1", "1");


        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }


        private void администрированиеToolStripMenuItem_Click(object sender = null, EventArgs e = null)
        {
            Program.MakeFocus(AdminPanel.Instance);

        }

        private async void IsDBAliveTimer_Tick(object sender, EventArgs e)
        {


            try
            {

                bool isDBAlive = await ConnectionManager.CheckDBConnectionAsync();


                if (isDBAlive)
                {
                    this.DBStat.Image = Properties.Resources.ok;
                    this.DBStat.Text = "База данных: ОК";
                    IsDBAliveTimer.Interval = 60 * 1000;
                }
                else
                {
                    this.DBStat.Image = Properties.Resources.error;
                    this.DBStat.Text = "База данных: ОШИБКА";
                    IsDBAliveTimer.Interval = 10 * 1000;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке базы данных: {ex.Message}");
            }

        }



        private void ChangeStatus(object sender, EventArgs e)
        {
            поискToolStripMenuItem1.Visible = true;
            this.statText.Text = "Успешный вход"; 
            this.statText.Image = LibA.Properties.Resources.ok;
        }

        private void UserPanel_SizeChanged(object sender = null, EventArgs e = null)
        {
            int leftMargin = (int)(Width * 0.1);
            int topMargin = (int)(Height * 0.05);
            int rightMargin = (int)(Width * 0.1);

            int topMarginWithMenuStrip = topMargin + menuStrip1.Height;

            searchPanel.Location = new Point(leftMargin, topMarginWithMenuStrip);
            searchPanel.Width = Width - leftMargin - rightMargin;

            UpdateSearchPanelLayout();
        }

        private void searchInput_Enter(object sender, EventArgs e)
        {
            if (searchInput.Text == "Начните вводить что-нибудь...")
            {
                searchInput.Text = "";
                searchInput.ForeColor = SystemColors.WindowText;
                UpdateSearchPanelLayout();
            }
        }

        private void searchInput_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchInput.Text))
            {
                searchInput.Text = "Начните вводить что-нибудь...";
                searchInput.ForeColor = SystemColors.GrayText;


                UpdateSearchPanelLayout();
            }
        }

        private void UpdateSearchPanelLayout()
        {
            int panelWidth = searchPanel.Width;
            int panelHeight = searchPanel.Height;

            int doSearchSize = panelHeight;
            doSearch.Size = new Size(doSearchSize, doSearchSize);
            doSearch.Location = new Point(panelWidth - doSearchSize, 0);

            int searchInputWidth = panelWidth - doSearchSize;
            int searchInputHeight = panelHeight;
            searchInput.Size = new Size(searchInputWidth, searchInputHeight);
            searchInput.Location = new Point(0, 0);

            float fontSize = searchInputHeight * 0.7f;
            if (fontSize > searchInputHeight)
                fontSize = searchInputHeight;

            Font font = new Font("Arial", fontSize);

            searchInput.Font = font;
            searchInput.TextAlign = HorizontalAlignment.Left;
            searchInput.Select(0, 0);


            int dataGridViewMargin = 10;
            int dataGridViewTopMargin = searchPanel.Bottom + dataGridViewMargin;

            dataGridViewMain.Location = new Point(searchPanel.Left, searchPanel.Bottom + 10);
            dataGridViewMain.Width = searchPanel.Width;
            dataGridViewMain.Height = this.ClientSize.Height - dataGridViewMain.Top - statusStrip1.Height;

        }

        private async void doSearch_Click(object sender, EventArgs e)
        {


            using (SqlCommand command = new SqlCommand("SELECT [Название],[Автор],[Жанр],[Издательство],[Год выпуска],[Число страниц],[Язык книги],[Доступность],[Как часто брали],[Краткое описание] FROM BooksPublicInfo(@SearchTerm)",  await ConnectionManager.Instance.OpenConnection()))
            {
                try
                {

                 
                    if (searchInput.Text == string.Empty || searchInput.Text.Equals("Начните вводить что-нибудь..."))
                        throw new Exception();
                    command.Parameters.AddWithValue("@SearchTerm", searchInput.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    //dataTable.Columns.Remove("Обложка");

                    string[] availability = new string[dataTable.Rows.Count];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        availability[i] = ((int)dataTable.Rows[i]["Доступность"] > 0) ? "В наличии" : "Нет в наличии";
                    }

                    int columnIndex = dataTable.Columns["Доступность"].Ordinal;
                    dataTable.Columns.Remove("Доступность");
                    dataTable.Columns.Add("Доступность", typeof(string));
                    dataTable.Columns["Доступность"].SetOrdinal(columnIndex);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        dataTable.Rows[i]["Доступность"] = availability[i];
                    }
                    dataGridViewMain.DataSource = dataTable;
                    dataGridViewMain.Visible = true;

                    dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;






                    foreach (DataGridViewRow row in dataGridViewMain.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null)
                            {
                                cell.Style.WrapMode = DataGridViewTriState.True;
                            }
                        }
                    }
                   



                }
                catch
                {
                    dataGridViewMain.Visible = false;
                }
            }
        }
        private void dataGridViewMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
  
           
        }
        private void dataGridViewMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewMain.Columns[e.ColumnIndex].Name == "Доступность") 
            {
         
                if (e.Value != null && e.Value.ToString() == "В наличии")
                {
                    e.CellStyle.BackColor = Color.LightGreen; 
                }
                else 
                    e.CellStyle.BackColor = Color.FromArgb(255, 255, 192, 192); 
               
            }
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MakeFocus(SettingsPane.Instance);
        }

        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            searchPanel.Visible = true;
        }

        private async void факультетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DataTable dt = await DBWorker.GetDataTable("select Код, Название from Факультет");
            ShowData sd = new(dt);

            sd.ShowDialog();
        }

        private async void группыToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            DataTable dt = await DBWorker.GetDataTable("select Группа.Код, Группа.Название as [Навзание группы], [Год поступления], [Год окончания], Факультет.Код, Факультет.Название as [Название факультета] from Группа inner join Факультет on [Код факультета]=Факультет.Код");
            ShowData sd = new(dt);
            sd.ShowDialog();
        }

        private async void читателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            DataTable dt = await DBWorker.GetDataTable("select Пользователь.Код, Фамилия, Имя, Отчество, [Дата рождения], [Контактный номер], [Адрес проживания], [Данные паспорта], [Номер читательского билета], Группа.Код, Группа.Название as [Название группы], [Год поступления], [Год окончания]  from Пользователь inner join Группа on Группа.Код = Пользователь.[Код группы] where [Права доступа]>1 ");
            ShowData sd = new(dt);
            sd.ShowDialog();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Reports dln = new())
            {
                dln.MakeReport("FindBookLocation @sterm", ReportType.TextInput);
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                dln.Text = menuItem.Text;
                dln.ShowDialog();
            }
        }

        private void выдатьСдатьКнигиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GiveBook b = new();
            b.Show();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBookManually();
        }
        private async void AddBookManually()
        {
            try
            {
                using (var form = new Form())
                {
                    form.Text = "Добавление книги";
                    form.AutoSize = true;

                    var tableLayout = new TableLayoutPanel
                    {
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        ColumnCount = 3,
                        Padding = new Padding(10),
                        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                    };
                    form.Controls.Add(tableLayout);

                    var labels = new[] { "Название", "Издательство", "Жанр", "Стеллаж", "Год выпуска", "Число страниц", "Язык книги", "Цена" };

                    var textBoxes = new TextBox[labels.Length];
                    for (int i = 0; i < labels.Length; i++)
                    {
                        var label = new Label
                        {
                            Text = labels[i] + ":",
                            TextAlign = ContentAlignment.MiddleLeft
                        };
                        tableLayout.Controls.Add(label, 0, i);

                        var textBox = new TextBox();
                        textBoxes[i] = textBox;
                        tableLayout.Controls.Add(textBox, 1, i);

                        if (labels[i] == "Язык книги")
                        {
                            var autoComplete = new AutoCompleteStringCollection();
                            autoComplete.AddRange(new string[] { "Русский", "Английский", "Французский", "Немецкий", "Испанский", "Китайский" });
                            textBox.AutoCompleteCustomSource = autoComplete;
                            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }
                        else if (labels[i] == "Издательство" || labels[i] == "Жанр" || labels[i] == "Стеллаж")
                        {

                            var autoComplete = new AutoCompleteStringCollection();
                            var sourceData = await GetAutoCompleteData(labels[i]);
                            autoComplete.AddRange(sourceData);
                            textBox.AutoCompleteCustomSource = autoComplete;
                            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }
                    }

                    async Task<string[]> GetAutoCompleteData(string fieldName)
                    {

                        string sterm = null;
                        if (fieldName == "Стеллаж")
                        {
                            sterm = "Номер стеллажа";
                            fieldName += "и";
                        }
                        else if (fieldName == "Жанр")
                            sterm = "Название жанра";
                        else
                            sterm = "Название";
                        string query = $"SELECT [{sterm}] FROM {fieldName}";
                        List<string> data = new List<string>();
                        try
                        {
                            using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                            {

                                SqlCommand command = new SqlCommand(query, connection);
                                SqlDataReader reader = await command.ExecuteReaderAsync();
                                while (reader.Read())
                                {
                                    data.Add(reader.GetString(0));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при получении данных для автодополнения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        foreach (var item in data)
                        {
                            Console.WriteLine(item);
                        }
                        return data.ToArray();
                    }

                    var pictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BorderStyle = BorderStyle.FixedSingle,
                        Width = 150,
                        Height = 200
                    };
                    tableLayout.Controls.Add(pictureBox, 2, 0);
                    tableLayout.SetRowSpan(pictureBox, labels.Length);
                    var bookCover = new Label
                    {
                        Text = "Выберите обложку:",
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    tableLayout.Controls.Add(bookCover, 0, labels.Length);
                    var buttonUploadCover = new Button
                    {
                        Text = "Загрузить обложку"
                    };
                    buttonUploadCover.Click += (sender, e) =>
                    {
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                pictureBox.ImageLocation = openFileDialog.FileName;
                            }
                        }
                    };
                    tableLayout.Controls.Add(buttonUploadCover, 1, labels.Length);

                    var descriptionLabel = new Label
                    {
                        Text = "Краткое описание:",
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    tableLayout.Controls.Add(descriptionLabel, 0, labels.Length + 1);

                    var descriptionTextBox = new RichTextBox
                    {
                        Dock = DockStyle.Fill,
                        Height = 100
                    };
                    tableLayout.Controls.Add(descriptionTextBox, 1, labels.Length + 1);
                    tableLayout.SetColumnSpan(descriptionTextBox, 2);

                    var buttonOk = new Button
                    {
                        Text = "Добавить книгу",
                        DialogResult = DialogResult.OK
                    };
                    form.Controls.Add(buttonOk);
                    buttonOk.Dock = DockStyle.Bottom;
                    buttonOk.Click += async (sender, e) =>
                    {
                        // Сначала проверяем, что все поля введены
                        foreach (var textBox in textBoxes)
                        {
                            if (string.IsNullOrWhiteSpace(textBox.Text))
                            {
                                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Подготовка данных для вставки
                        string title = textBoxes[0].Text;
                        string publisherQuery = "SELECT [Код] FROM Издательство WHERE [Название] = @PublisherName";
                        SqlCommand publisherCommand = new SqlCommand(publisherQuery, await ConnectionManager.Instance.OpenConnection());
                        publisherCommand.Parameters.AddWithValue("@PublisherName", textBoxes[1].Text);
                        int publisherId = (int)publisherCommand.ExecuteScalar();

                        // Получение ID жанра
                        string genreQuery = "SELECT [Код] FROM Жанр WHERE [Название жанра] = @GenreName";
                        SqlCommand genreCommand = new SqlCommand(genreQuery, await ConnectionManager.Instance.OpenConnection());
                        genreCommand.Parameters.AddWithValue("@GenreName", textBoxes[2].Text);
                        int genreId = (int)genreCommand.ExecuteScalar();

                        // Получение ID стеллажа
                        string shelfQuery = "SELECT [Код] FROM Стеллажи WHERE [Номер стеллажа] = @ShelfNumber";
                        SqlCommand shelfCommand = new SqlCommand(shelfQuery, await ConnectionManager.Instance.OpenConnection());
                        shelfCommand.Parameters.AddWithValue("@ShelfNumber", textBoxes[3].Text);
                        int shelfId = (int)shelfCommand.ExecuteScalar();
                        int year = int.Parse(textBoxes[4].Text);
                        int pageCount = int.Parse(textBoxes[5].Text);
                        string language = textBoxes[6].Text;
                        decimal price = decimal.Parse(textBoxes[7].Text);
                        string description = descriptionTextBox.Text;
                        string coverPath = pictureBox.ImageLocation;
                        byte[] coverImage = null;
                        if (coverPath != null)
                        {
                            coverImage = System.IO.File.ReadAllBytes(coverPath);
                        }
                        // Вставка данных в базу данных

                        using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                        {
                            string insertQuery = @"INSERT INTO Книга ([Название], [Код издательства], [Код жанра], [Код стеллажа], [Год выпуска], [Число страниц], [Язык книги], [Краткое описание], [Цена], [Обложка]) 
       VALUES (@Title, @PublisherId, @GenreId, @ShelfId, @Year, @PageCount, @Language, @Description, @Price, @CoverImage)";

                            SqlCommand command = new SqlCommand(insertQuery, connection);
                            command.Parameters.AddWithValue("@Title", title);
                            command.Parameters.AddWithValue("@PublisherId", publisherId);
                            command.Parameters.AddWithValue("@GenreId", genreId);
                            command.Parameters.AddWithValue("@ShelfId", shelfId);
                            command.Parameters.AddWithValue("@Year", year);
                            command.Parameters.AddWithValue("@PageCount", pageCount);
                            command.Parameters.AddWithValue("@Language", language);
                            command.Parameters.AddWithValue("@Description", description);
                            command.Parameters.AddWithValue("@Price", price);

                            command.Parameters.AddWithValue("@CoverImage", coverImage);
                            try
                            {

                                int rowsAffected = await command.ExecuteNonQueryAsync();
                                MessageBox.Show("Книга успешно добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при добавлении книги: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    };
                    form.ShowDialog();
                }
            }
            catch { }
        }

        private async void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = await DBWorker.GetDataTable("Книга");
            ShowData s = new(dt);
            s.ShowDialog();
        }
    }
}