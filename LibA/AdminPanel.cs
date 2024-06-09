using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
                tables = await DBWorker.BdGetDataMSSQL("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' AND table_name != 'sysdiagrams' order by table_name ASC");

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

        private void usageStatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports dln = new Reports();
            dln.MakeReport("GetPercentageUsersByGroupAndFaculty @startdate, @enddate", ReportType.With2Calendars);
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            dln.Text = menuItem.Text;
            dln.ShowDialog();
        }

        private void debtorsCommonBetween2DatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports dln = new Reports();
            dln.MakeReport("GetDebtorsReport @startdate, @enddate", ReportType.With2Calendars);
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            dln.Text = menuItem.Text;
            dln.ShowDialog();
        }

        private void debtorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Reports dln = new()) {
                dln.MakeReport("GetReaderInfoExact", ReportType.None);
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                dln.Text = menuItem.Text;
                dln.ShowDialog();
            }
        }
        private void whereisBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Reports dln = new())
            {
                dln.MakeReport("FindBookLocation @sterm", ReportType.TextInput);
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                dln.Text = menuItem.Text;
                dln.ShowDialog();
            }   
                
        }

        //OPERATIONS

        private async void перевестиГруппыНаСледующийГодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DBWorker.ExecProcedure("RemoveOutdatedReaders");
            MessageBox.Show("Выполнение процедуры успешно");
        }




        private async void dataGridViewMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string headerText = dataGridViewMain.Columns[e.ColumnIndex].HeaderText.ToLower();

                if (headerText.StartsWith("код") || headerText.StartsWith("id"))
                {
                    string columnName = dataGridViewMain.Columns[e.ColumnIndex].Name;
                    string ctableName = ((DataTable)dataGridViewMain.DataSource)?.TableName;
                    List<string> tableNames = await DBWorker.GetLinkedTableNames(ctableName, columnName);

                    if (tableNames.Count > 0)
                    {
                        foreach (string tableName in tableNames)
                        {
                            DataTable dependentTable = await DBWorker.GetDataTable(tableName);

                            if (dependentTable != null)
                            {
                                string cellValueString = dataGridViewMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                                int val = string.IsNullOrEmpty(cellValueString) ? 0 : int.Parse(cellValueString);
                                DLinker dLinkerForm = new DLinker(dependentTable, val);

                                dLinkerForm.SelectionMade += (selectedValue) =>
                                {
                                    dataGridViewMain.EndEdit();
                                    dataGridViewMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selectedValue;
                                };
                                dLinkerForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show($"Связанная таблица '{tableName}' не найдена.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Связанная таблица не найдена.");
                    }
                }
            }
        }



        private void dataGridViewMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

       

        private async void создатьШаблонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await СоздатьШаблонAsync();
        }

        private async Task СоздатьШаблонAsync()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
       
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Читатель");

                  
                    string[] headers = { "Фамилия", "Имя", "Отчество", "Дата рождения", "Контактный номер",
                                         "Адрес проживания", "Данные паспорта", "Номер читательского билета",
                                         "Название группы", "Год поступления", "Год окончания", "Имя для входа" };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = headers[i];
                    }

                    DataTable groupData = await DBWorker.GetDataTable("Группа");
                    if (groupData != null && groupData.Rows.Count > 0)
                    {
                        ExcelWorksheet groupWorksheet = excelPackage.Workbook.Worksheets.Add("Группы");


                        int columnCount = 1;
                        for (int i = 0; i < groupData.Columns.Count; i++)
                        {
                            if (!groupData.Columns[i].ColumnName.StartsWith("код", StringComparison.OrdinalIgnoreCase) &&
                                !groupData.Columns[i].ColumnName.StartsWith("id", StringComparison.OrdinalIgnoreCase))
                            {
                                groupWorksheet.Cells[1, columnCount].Value = groupData.Columns[i].ColumnName;
                                columnCount++;
                            }
                        }


                        for (int row = 0; row < groupData.Rows.Count; row++)
                        {
                            columnCount = 1;
                            for (int col = 0; col < groupData.Columns.Count; col++)
                            {
                                if (!groupData.Columns[col].ColumnName.StartsWith("код", StringComparison.OrdinalIgnoreCase) &&
                                    !groupData.Columns[col].ColumnName.StartsWith("id", StringComparison.OrdinalIgnoreCase))
                                {
                                    groupWorksheet.Cells[row + 2, columnCount].Value = groupData.Rows[row][col];
                                    columnCount++;
                                }
                            }
                        }
                    }

                    


                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveFileDialog.FileName = "Шаблон_Читатель.xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);


                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Шаблон успешно создан.");
                        }
                        else { 
                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании шаблона: {ex.Message}");
            }
        }




        private async void взятьДанныеИзxslsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = fetchDataFromExcelFile();
            DataTable dt = result.Item1;
            string filePath = result.Item2;

            if (dt != null && !string.IsNullOrEmpty(filePath))
            {
                DataTable extendedTable = generateAPassword(dt, filePath);
                await mergeReadersToDatabase(extendedTable);
            }
        }

        private Tuple<DataTable, string> fetchDataFromExcelFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    DataTable excelData = new DataTable();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            // Add columns
                            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                            {
                                excelData.Columns.Add(worksheet.Cells[1, col].Value?.ToString());
                            }

                            // Add rows
                            for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                            {
                                DataRow row = excelData.Rows.Add();
                                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                                {
                                    row[col - 1] = worksheet.Cells[rowNum, col].Value?.ToString();
                                }
                            }

                            return new Tuple<DataTable, string>(excelData, filePath);
                        }
                        else
                        {
                            throw new Exception("Лист Excel не найден.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обработке данных из файла Excel: {ex.Message}");
                }
            }

            return new Tuple<DataTable, string>(null, null);
        }

        private DataTable generateAPassword(DataTable dataTable, string filePath)
        {
            // Adding new columns for login, password, salt, and hashed password
            
            dataTable.Columns.Add("Пароль", typeof(string));
            dataTable.Columns.Add("Соль", typeof(string));
            dataTable.Columns.Add("Хэш пароля", typeof(string));

            foreach (DataRow row in dataTable.Rows)
            {
                string surname = row["Фамилия"].ToString();
                string name = row["Имя"].ToString();
                string patronymic = row["Отчество"].ToString();

                string loginName = GenerateLogin(surname, name, patronymic);
                string password = GenerateRandomPassword();
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(password, salt);

                row["Имя для входа"] = loginName;
                row["Пароль"] = password;
                row["Соль"] = salt;
                row["Хэш пароля"] = hashedPassword;
            }

            // Save updated data to a new worksheet in the same Excel file
            SaveUpdatedDataToExcel(dataTable, filePath);

            return dataTable;
        }

        private void SaveUpdatedDataToExcel(DataTable dataTable, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Пароли");

                // Add headers
                worksheet.Cells[1, 1].Value = "Фамилия";
                worksheet.Cells[1, 2].Value = "Имя";
                worksheet.Cells[1, 3].Value = "Отчество";
                worksheet.Cells[1, 4].Value = "Название группы";
                worksheet.Cells[1, 5].Value = "Имя для входа";
                worksheet.Cells[1, 6].Value = "Пароль";

                // Add rows
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    worksheet.Cells[row + 2, 1].Value = dataTable.Rows[row]["Фамилия"].ToString();
                    worksheet.Cells[row + 2, 2].Value = dataTable.Rows[row]["Имя"].ToString();
                    worksheet.Cells[row + 2, 3].Value = dataTable.Rows[row]["Отчество"].ToString();
                    worksheet.Cells[row + 2, 4].Value = dataTable.Rows[row]["Название группы"].ToString();
                    worksheet.Cells[row + 2, 5].Value = dataTable.Rows[row]["Имя для входа"].ToString();
                    worksheet.Cells[row + 2, 6].Value = dataTable.Rows[row]["Пароль"].ToString();
                }

                excelPackage.Save();
            }
        }

        public async Task mergeReadersToDatabase(DataTable dataTable)
        {
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string groupName = row["Название группы"].ToString();
                    int yearOfEnrollment = int.Parse(row["Год поступления"].ToString());
                    int yearOfGraduation = int.Parse(row["Год окончания"].ToString());

                    int groupCode = await GetGroupCodeOrCreateNew(groupName, yearOfEnrollment, yearOfGraduation);
                    if (groupCode > 0)
                    {
                        await InsertReaders(dataTable, groupCode);
                    }
                }
            }
        }

        private async Task<int> GetGroupCodeOrCreateNew(string groupName, int yearOfEnrollment, int yearOfGraduation)
        {
            int groupCode = await GetGroupCodeIfExists(groupName, yearOfEnrollment, yearOfGraduation);
            if (groupCode == 0)
            {
                groupCode = await AddNewGroup(groupName, yearOfEnrollment, yearOfGraduation);
            }
            return groupCode;
        }

        private async Task<int> GetGroupCodeIfExists(string groupName, int yearOfEnrollment, int yearOfGraduation)
        {
            int groupCode = 0;
            using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
            {
                string query = "SELECT Код FROM Группа WHERE Название = @GroupName AND [Год поступления] = @YearOfEnrollment AND [Год окончания] = @YearOfGraduation";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GroupName", groupName);
                command.Parameters.AddWithValue("@YearOfEnrollment", yearOfEnrollment);
                command.Parameters.AddWithValue("@YearOfGraduation", yearOfGraduation);

                object result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    groupCode = Convert.ToInt32(result);
                }
            }
            return groupCode;
        }

        private async Task<int> AddNewGroup(string groupName, int yearOfEnrollment, int yearOfGraduation)
        {
            int groupCode = 0;
            using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
            {
                string query = "INSERT INTO Группа (Название, [Год поступления], [Год окончания]) VALUES (@GroupName, @YearOfEnrollment, @YearOfGraduation); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GroupName", groupName);
                command.Parameters.AddWithValue("@YearOfEnrollment", yearOfEnrollment);
                command.Parameters.AddWithValue("@YearOfGraduation", yearOfGraduation);

                object result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    groupCode = Convert.ToInt32(result);
                }
            }
            return groupCode;
        }

        private async Task InsertReaders(DataTable dataTable, int groupCode)
        {
            using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string surname = row["Фамилия"].ToString();
                    string name = row["Имя"].ToString();
                    string patronymic = row["Отчество"].ToString();
                    DateTime dateOfBirth = DateTime.Parse(row["Дата рождения"].ToString());
                    string contactNumber = row["Контактный номер"].ToString();
                    string address = row["Адрес проживания"].ToString();
                    string passportData = row["Данные паспорта"].ToString();
                    string readerCardNumber = row["Номер читательского билета"].ToString();
                    string loginName = row["Имя для входа"].ToString();
                    string salt = row["Соль"].ToString();
                    string hashedPassword = row["Хэш пароля"].ToString();

                    string query = "INSERT INTO Пользователь ([Фамилия], [Имя], [Отчество], [Дата рождения], [Контактный номер], [Адрес проживания], [Данные паспорта], [Номер читательского билета], [Код группы], [Имя для входа], [Соль], [Хэш пароля]) " +
                                   "VALUES (@Surname, @Name, @Patronymic, @DateOfBirth, @ContactNumber, @Address, @PassportData, @ReaderCardNumber, @GroupCode, @LoginName, @Salt, @HashedPassword)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Patronymic", patronymic);
                    command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@PassportData", passportData);
                    command.Parameters.AddWithValue("@ReaderCardNumber", readerCardNumber);
                    command.Parameters.AddWithValue("@GroupCode", groupCode);
                    command.Parameters.AddWithValue("@LoginName", loginName);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@HashedPassword", hashedPassword);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private string GenerateLogin(string lastName, string firstName, string patronymic)
        {
            // Generate login in the format "nmpatronymic"
            string login = $"{firstName[0].ToString().ToLower()}{patronymic[0].ToString().ToLower()}{lastName.ToLower()}";
            return login;
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateSalt()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
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




    }






}


