using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


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
            };
            


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


            authForm = new AuthForm(this);
            authForm.RegAuthSuccess += ChangeStatus;
            authForm.HasRights += (sender, e) =>
            {
                администрированиеToolStripMenuItem.Visible = true;
            };
            authForm.WhichWindow(WindowType.AUTHORIZE);
            authForm.ShowDialog();
        }

        private void изПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void изУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Disconnect();
            this.statText.Text = "";
            ConnectionManager.Instance.SetupConnectionString("Guest1", "1");


        }




        public void newTSSLabel(string ctext)
        {

            this.statText.Text = ctext;
        }



        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MakeFocus(SettingsPane.Instance);


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



        private async void ChangeStatus(object sender, EventArgs e)
        {
            using (SqlConnection c = await ConnectionManager.Instance.OpenConnection())
            {
                var b = new SqlConnectionStringBuilder(c?.ConnectionString);
                statText.Text = $"Добро пожаловать, {b?.UserID}";
            }
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
         
                if (e.Value != null && e.Value == "В наличии")
                {
                    e.CellStyle.BackColor = Color.LightGreen; 
                }
                else 
                    e.CellStyle.BackColor = Color.FromArgb(255, 255, 192, 192); 
               
            }
        }

    }
}