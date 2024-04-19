using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{

    public partial class AdminPanel : Form
    {
        private static AdminPanel instance;
        private static bool isClosed = true;

        public static AdminPanel Instance
        {
            get
            {
                if (instance == null || isClosed)
                {
                    instance = new AdminPanel();
                    instance.FormClosed += (sender, e) =>
                    {
                        isClosed = true;
                    };
                    isClosed = false;
                }

                return instance;
            }
        }

        private AdminPanel()
        {
            InitializeComponent();
            this.Shown += LoadTablesAsync;
        }



        private async void LoadTablesAsync(object sender, EventArgs e)
        {
            string[] tables = null;
            try
            {
                tables = await DBWorker.BdGetDataMSSQL("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' order by table_name ASC");

            }
            catch
            {
                this.Close();
                return;
            }

            if (tables != null)
            {
                Tables.Items.AddRange(tables);
            }


            Tables.Height = Tables.ItemHeight * (Tables.Items.Count + 1);



            buttonTransact.Location = new Point(
                Tables.Left,
                Tables.Bottom
            );

            buttonRollback.Location = new Point(
                buttonTransact.Left,
                buttonTransact.Bottom
            );

            this.Height = dataGridViewMain.Bottom;
            this.Width = dataGridViewMain.Right + (this.Width - this.ClientSize.Width);
        }

        private async void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DBWorker.OldTable != null && !AreTablesEqual(dataGridViewMain.DataSource as DataTable, DBWorker.OldTable))
            {
                DialogResult dgv = MessageBox.Show("У вас есть несохранённые изменения. Вы хотите сохранить их?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (dgv == DialogResult.Yes)
                {
                    buttonTransact_Click(sender, e);
                    return;
                }
            }

            string selectedTable = Tables.Items[Tables.SelectedIndex > 0 ? Tables.SelectedIndex : 0].ToString();
            DataTable datatable = await DBWorker.GetDataTable(selectedTable);
            if (datatable != null)
            {
                buttonRollback.Enabled = false;
                buttonRollback.BackColor = Color.Gray;
                DBWorker.OldTable = datatable.Copy();
                dataGridViewMain.DataSource = datatable;
                dataGridViewMain.Enabled = true;
                dataGridViewMain.Visible = true;
                dataGridViewMain.Columns[0].Visible = false;
                dataGridViewMain.AutoGenerateColumns = true;

            }
        }



        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
            this.Dispose();
        }

        private bool AreTablesEqual(DataTable table1, DataTable table2)
        {
            try
            {
                if (table1 == null || table2 == null)
                {
                    return table1 == table2;
                }

                if (table1.Rows.Count != table2.Rows.Count || table1.Columns.Count != table2.Columns.Count)
                {
                    return false;
                }

                for (int i = 0; i < table1.Rows.Count; i++)
                {
                    for (int j = 0; j < table1.Columns.Count; j++)
                    {
                        if (!object.Equals(table1.Rows[i][j], table2.Rows[i][j]))
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }



        private async void buttonTransact_Click(object sender, EventArgs e)
        {
            try
            {
                await DBWorker.BeginTransaction(dataGridViewMain);
                buttonRollback.Enabled = true;
                buttonRollback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                MessageBox.Show("Изменения сохранены в базе данных.");
            }
            catch (Exception er)
            {
                MessageBox.Show("Непредвиденная ошибка при обновлении данных:\n" + er.Message);
            }
        }

        private async void buttonRollback_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewMain.DataSource = DBWorker.OldTable;

                await DBWorker.BeginTransaction(DBWorker.OldTable);
                buttonRollback.Enabled = false;
                buttonRollback.BackColor = Color.Gray;
                MessageBox.Show("Откат выполнен успешно");
            }
            catch
            {
                MessageBox.Show("Ошибка отката");
            }
        }





        private void dataGridViewMain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewMain.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn column &&
                    column.DefaultCellStyle.Format == "dd.MM.yyyy")
                {
                    if (DateTime.TryParseExact(
                        dataGridViewMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date))
                    {
                        dataGridViewMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date.ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат даты!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании: {ex.Message}");
            }
        }


        private async void lPane_Panel2_Click(object sender, EventArgs e)
        {
            int targetWidth;

            if (lPane.Panel1Collapsed)
            {
                targetWidth = 200;
                lPane.Panel1Collapsed = !lPane.Panel1Collapsed;
                await AnimateSplitter(targetWidth);
            }
            else
            {
                targetWidth = 40;
                await AnimateSplitter(targetWidth);
                lPane.Panel1Collapsed = !lPane.Panel1Collapsed;

            }


            lPane.Panel2.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            lPane.Refresh();


        }

        private async Task AnimateSplitter(int targetWidth)
        {
            const int AnimationDuration = 100;
            const int AnimationSteps = 10;

            int currentWidth = lPane.Width;
            int step = (targetWidth - currentWidth) / AnimationSteps;

            for (int i = 0; i < AnimationSteps; i++)
            {
                currentWidth += step;
                lPane.Width = currentWidth;
                await Task.Delay(AnimationDuration / AnimationSteps);
            }

            lPane.Width = targetWidth;

            dataGridViewMain.Dock = DockStyle.Fill;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MakeFocus(SettingsPane.Instance);


        }



        private void деавторизоватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Disconnect();
            this.Close();
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void статистикаИспользованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynamicLinkerAndReports dln = new DynamicLinkerAndReports();
            dln.MakeData("SELECT * FROM dbo.GetPercentageUsersByGroupAndFaculty(@startdate, @enddate)");
            dln.ShowDialog();
        }

        private void проверитьКнигиУЧитателяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynamicLinkerAndReports dln = new DynamicLinkerAndReports();

            dln.MakeData("SELECT * FROM dbo.GetDebtorsReport(@startdate, @enddate)");
            dln.ShowDialog();
        }

        private async void должникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = 0;
                using (DynamicLinkerAndReports dln = new DynamicLinkerAndReports())
                {

                    DataTable data = await DBWorker.GetDataTable("SELECT * from Читатель");
                    dln.MakeData(data);
                    dln.ShowDialog();
                    selectedRow = dln.selectedRowCode;
                }
                if (selectedRow == 0)
                    return;
                using (DynamicLinkerAndReports dln = new DynamicLinkerAndReports())
                {
                    DataTable data = await DBWorker.GetDataTable($"select * from dbo.GetBookStatusAndDueDate({selectedRow})");
                    dln.MakeData(data);
                    dln.ShowDialog();
                }
            }
            catch { }
        }

        private async void перевестиГруппыНаСледующийГодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DBWorker.ExecProcedure("RemoveOutdatedReaders");
        }




        private async void dataGridViewMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string tableName = null;
            if (dataGridViewMain.DataSource is DataTable dataTable1)
            {
                tableName = dataTable1.TableName;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                string columnName = dataGridViewMain.Columns[e.ColumnIndex].Name;


                if (columnName.ToLower().StartsWith("код") || columnName.ToLower().StartsWith("id"))
                {

                    int selectedRow = 0;

                    DataTable dataTable = await GetDataForTable(columnName, tableName);
                    using (DynamicLinkerAndReports linkerAndReports = new DynamicLinkerAndReports())
                    {

                        linkerAndReports.MakeData(dataTable);
                        linkerAndReports.ShowDialog();

                        selectedRow = linkerAndReports.selectedRowCode;

                    }
                    if (selectedRow != 0)
                    {
                        dataGridViewMain.EndEdit();
                        dataGridViewMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selectedRow;
                        dataGridViewMain.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    }
                }
            }
        }




        private async Task<DataTable> GetDataForTable(string columnName, string selectedTable)
        {
            DataTable dataTable = new DataTable();
            string lowerColumnName = columnName.ToLower();

            string[] tablesSM = await DBWorker.BdGetDataMSSQL("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' order by table_name ASC");
            List<string> tables = new List<string>(tablesSM);


            string matchingTableName = DBWorker.FindMatchingTableName(lowerColumnName, tables, selectedTable);

            if (!string.IsNullOrEmpty(matchingTableName))
            {
                dataTable = await DBWorker.GetDataTable(matchingTableName);
            }

            return dataTable;
        }

        private async void загрузкаПервокурсниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "desktop";
            openFileDialog.Filter = "CSV|*.csv|txt|*.txt|CSV или TXT|*.*";
            openFileDialog.Title = "Открытие файла...";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                string selectedFileName = openFileDialog.FileName;

                using (StreamReader reader = new StreamReader(selectedFileName))
                {
                    bool firstLine = true;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Split(';');
                        if (firstLine)
                        {

                            firstLine = false;
                            continue;
                        }

                        var f = line[1];
                        var i = line[2];
                        var o = line[3];
                        var date = DateTime.ParseExact(line[4], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        var number = line[5];
                        var adress = line[6];
                        var passport = line[7];
                        var readerId = line[8];
                        var gName = line[9];
                        var login = line[10];

                        string checkReaderQuery = $"Select 1 Where Exists (Select 1 From Читатель Where Фамилия = '{f}' and Имя = '{i}' and Отчество = '{o}')";
                        string insertReaderQuery = $"INSERT INTO Читатель VALUES (@Фамилия, @Имя, @Отчество, @Дата_рождения, @Контактный_номер, @Адрес_проживания, @Данные_паспорта, @Номер_читательского_билета, @Код_группы, @Имя_для_входа)";
                        string updateReaderQuery = $"UPDATE Читатель SET [Фамилия] = @Фамилия, [Имя] = @Имя, [Отчество] = @Отчество, [Дата рождения]=@Дата_рождения, [Контактный номер]=@Контактный_номер, [Адрес проживания]=@Адрес_проживания,[Данные паспорта]=@Данные_паспорта,[Номер читательского билета]=@Номер_читательского_билета,[Код группы]=@Код_группы,[Имя для входа] = @Имя_для_входа where [Фамилия] = @Фамилия AND [Имя] = @Имя AND [Отчество] = @Отчество";


                        using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                        {


                            using (SqlCommand checkReaderCommand = new SqlCommand(checkReaderQuery, connection))
                            {



                                
                                object result = checkReaderCommand.ExecuteScalar();
                                int readerCount = result == null ? 0 : (int)result;
                                int groupId = GetOrCreateGroup(connection, gName);
                                if ((int)readerCount == 0)
                                {


                                    using (SqlCommand insertCommand = new SqlCommand(insertReaderQuery, connection))
                                    {
                                        insertCommand.Parameters.AddWithValue("@Фамилия", f);
                                        insertCommand.Parameters.AddWithValue("@Имя", i);
                                        insertCommand.Parameters.AddWithValue("@Отчество", o);
                                        insertCommand.Parameters.AddWithValue("@Дата_рождения", date);
                                        insertCommand.Parameters.AddWithValue("@Контактный_номер", number);
                                        insertCommand.Parameters.AddWithValue("@Адрес_проживания", adress);
                                        insertCommand.Parameters.AddWithValue("@Данные_паспорта", passport);
                                        insertCommand.Parameters.AddWithValue("@Номер_читательского_билета", readerId);
                                        insertCommand.Parameters.AddWithValue("@Код_группы", groupId);
                                        insertCommand.Parameters.AddWithValue("@Имя_для_входа", login);
                                        insertCommand.ExecuteNonQuery();
                                    }

                                }
                                else
                                {

                                    DialogResult updateResult = MessageBox.Show($"Читатель {f} {i} {o} уже существует. Обновить информацию?\n\nВнимание, это удалит сущетсвующего пользователя!", "Обновление читателя", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (updateResult == DialogResult.Yes)
                                    {
                                        using (SqlCommand updateCommand = new SqlCommand(updateReaderQuery, connection))
                                        {
                                            updateCommand.Parameters.AddWithValue("@Фамилия", f);
                                            updateCommand.Parameters.AddWithValue("@Имя", i);
                                            updateCommand.Parameters.AddWithValue("@Отчество", o);
                                            updateCommand.Parameters.AddWithValue("@Дата_рождения", date);
                                            updateCommand.Parameters.AddWithValue("@Контактный_номер", number);
                                            updateCommand.Parameters.AddWithValue("@Адрес_проживания", adress);
                                            updateCommand.Parameters.AddWithValue("@Данные_паспорта", passport);
                                            updateCommand.Parameters.AddWithValue("@Номер_читательского_билета", readerId);
                                            updateCommand.Parameters.AddWithValue("@Код_группы", groupId);
                                            updateCommand.Parameters.AddWithValue("@Имя_для_входа", login);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
            }
            MessageBox.Show("Данные успешно обработаны и добавлены в базу данных.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int GetOrCreateGroup(SqlConnection connection, string groupName)
        {

            string selectGroupQuery = "SELECT Код FROM Группа WHERE Название = @GroupName AND Курс = 1";

            using (SqlCommand selectGroupCommand = new SqlCommand(selectGroupQuery, connection))
            {
                selectGroupCommand.Parameters.AddWithValue("@GroupName", groupName);

                object result = selectGroupCommand.ExecuteScalar();
                if (result != null)
                {

                    return (int)result;
                }
                else
                {
                    string insertGroupQuery = "INSERT INTO Группа (Название, Курс, [Последний курс], [Код факультета]) VALUES (@GroupName, 1, 2, NULL); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand insertGroupCommand = new SqlCommand(insertGroupQuery, connection))
                    {
                        insertGroupCommand.Parameters.AddWithValue("@GroupName", groupName);


                        return Convert.ToInt32(insertGroupCommand.ExecuteScalar());
                    }
                }
            }
        }
    }



}


