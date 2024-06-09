using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace LibA
{
    public partial class GiveBook : Form
    {
     

        public GiveBook()
        {
            InitializeComponent();

            // Инициализация формы
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Загрузка данных о факультетах
            LoadFaculties();

            // Установка связей между элементами управления
            listBoxFaculties.SelectedIndexChanged += ListBoxFaculties_SelectedIndexChanged;
            listBoxGroups.SelectedIndexChanged += ListBoxGroups_SelectedIndexChanged;
           

           
        }

        private async void LoadFaculties()
        {
            DataTable facultyTable = await DBWorker.GetDataTable("Факультет");

            if (facultyTable != null)
            {
                listBoxFaculties.DataSource = facultyTable;
                listBoxFaculties.DisplayMember = "Название";
            }
        }
        private void ListBoxFaculties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFaculties.SelectedIndex != -1)
            {
                DataRowView selectedFaculty = (DataRowView)listBoxFaculties.SelectedItem;


                LoadGroups(selectedFaculty["Название"].ToString());
            }
        }
        private async void LoadGroups(string v)
        {
            string sqlCommand = $"SELECT * FROM Группа WHERE [Код факультета] = (select Код from Факультет where [Название] = '{v}')";

            DataTable groupTable = await DBWorker.GetDataTable(sqlCommand);

            if (groupTable != null)
            {
                listBoxGroups.DataSource = groupTable;
                listBoxGroups.DisplayMember = "Название";
            }
        }
        private void ListBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedIndex != -1)
            {
                DataRowView selectedGroup = (DataRowView)listBoxGroups.SelectedItem;


                LoadReaders(selectedGroup["Название"].ToString());
            }
        }
        private async void LoadReaders(string v)
        {

            DataTable readerTable = await DBWorker.GetDataTable($"select Пользователь.Код, Фамилия, Имя, Отчество, [Дата рождения], [Контактный номер], [Адрес проживания], [Данные паспорта], [Номер читательского билета], Группа.Код, Группа.Название as [Название группы], [Год поступления], [Год окончания]  from Пользователь inner join Группа on Группа.Код = Пользователь.[Код группы] where [Права доступа]>1 and Группа.Название='{v}'");

           
            if (readerTable != null)
            {
                dataGridView1.DataSource = readerTable;
            }
            

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name.ToLower().StartsWith("код") || column.Name.ToLower().StartsWith("id"))
                {
                    column.Visible = false;
                }
            }
        }

      

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int readerCode = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код"].Value);
                LoadIssueReturn(readerCode);
                LoadStudentDetails(readerCode);
            }
        }

        private async void LoadIssueReturn(int readerCode)
        {
            string sqlCommand = @"
    SELECT 
        k.Код as [Код книги],
        p.Код as [Код читателя],
        k.[Название] AS [Название книги], 
        vr.[Дата выдачи], 
        vr.[Дата возврата],
        vr.[Книга утеряна]
    FROM dbo.[Выдача и возврат] vr
    JOIN dbo.[Книга] k ON vr.[Код книги] = k.[Код]
    JOIN dbo.Пользователь p ON vr.[Код читателя] = p.Код
    WHERE vr.[Код читателя] = @ReaderCode";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@ReaderCode", SqlDbType.Int) { Value = readerCode }
            };

            DataTable issueReturnTable = await DBWorker.GetDataTable(sqlCommand, parameters);

            if (issueReturnTable != null)
            {
                issueReturnTable.TableName = "Выдача и возврат";

                

                button1.Visible = true;
                //button2.Visible = true;
                dataGridView2.Visible = true;
                dataGridView2.Dock = DockStyle.Fill;
                dataGridView2.DataSource = issueReturnTable;

                // Скрытие колонок с кодами
                foreach (DataGridViewColumn c in dataGridView2.Columns)
                {
                    if (c.HeaderText.ToLower().StartsWith("код"))
                    {
                        c.Visible = false;
                    }
                }

                // Добавление колонки с кнопками
                if (!dataGridView2.Columns.Contains("ActionButton"))
                {
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "ActionButton",
                        HeaderText = "Действие",
                        Text = "Выдать/Сдать",
                        UseColumnTextForButtonValue = false
                    };
                    dataGridView2.Columns.Add(buttonColumn);
                }

                // Установка кнопок и ссылок
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.IsNewRow || row.Cells["Дата выдачи"].Value == DBNull.Value)
                    {
                        row.Cells["ActionButton"].Value = "Выдать";
                        DataGridViewLinkCell linkCell = new DataGridViewLinkCell
                        {
                            Value = "Выдать новую книгу"
                        };
                        row.Cells["Название книги"] = linkCell;
                    }
                    else
                    {
                        row.Cells["ActionButton"].Value = "Сдать";
                    }
                }

                // Установка порядка столбцов
                dataGridView2.Columns["Название книги"].DisplayIndex = 0;
                dataGridView2.Columns["Дата выдачи"].DisplayIndex = 1;
                dataGridView2.Columns["Дата возврата"].DisplayIndex = 2;
                dataGridView2.Columns["ActionButton"].DisplayIndex = 3;
                dataGridView2.Columns["Книга утеряна"].DisplayIndex = 4;

                splitContainer1.Panel1Collapsed = false;
            }
        }

        // Обработчик нажатия на кнопку и ссылку
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["ActionButton"].Index && e.RowIndex >= 0)
            {
                var actionCell = dataGridView2.Rows[e.RowIndex].Cells["ActionButton"];
                if (actionCell.Value.ToString() == "Сдать")
                {
                    // Обработка сдачи книги
                    var returnDateCell = dataGridView2.Rows[e.RowIndex].Cells["Дата возврата"];
                    if (returnDateCell.Value == DBNull.Value)
                    {
                        returnDateCell.Value = DateTime.Now.Date;
                        // Обновление данных в базе данных
                        UpdateReturnDateInDatabase(e.RowIndex);
                    }
                }
                else if (actionCell.Value.ToString() == "Выдать")
                {
                    // Обработка выдачи книги
                    var issueDateCell = dataGridView2.Rows[e.RowIndex].Cells["Дата выдачи"];
                    if (issueDateCell.Value == DBNull.Value)
                    {
                        issueDateCell.Value = DateTime.Now.Date;
                        // Обновление данных в базе данных
                        IssueNewBookToDatabase(e.RowIndex);
                    }
                }
            }
            else if (e.ColumnIndex == dataGridView2.Columns["Название книги"].Index && e.RowIndex >= 0 && dataGridView2.Rows[e.RowIndex].IsNewRow)
            {
                DataTable dt = (DataTable)dataGridView2.DataSource;
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);

                // Устанавливаем ссылку и кнопку для новой строки
                dataGridView2.Rows[e.RowIndex].Cells["ActionButton"].Value = "Выдать";
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell
                {
                    Value = "Выдать новую книгу"
                };
                dataGridView2.Rows[e.RowIndex].Cells["Название книги"] = linkCell;
            }
        }

        // Метод для обновления даты возврата в базе данных
        private void UpdateReturnDateInDatabase(int rowIndex)
        {
            int bookCode = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["Код книги"].Value);
            int readerCode = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["Код читателя"].Value);
            DateTime returnDate = DateTime.Now.Date;

            string updateSql = @"
    UPDATE dbo.[Выдача и возврат]
    SET [Дата возврата] = @ReturnDate
    WHERE [Код книги] = @BookCode AND [Код читателя] = @ReaderCode";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@ReturnDate", SqlDbType.Date) { Value = returnDate },
        new SqlParameter("@BookCode", SqlDbType.Int) { Value = bookCode },
        new SqlParameter("@ReaderCode", SqlDbType.Int) { Value = readerCode }
            };

            //DBWorker.ExecuteNonQuery(updateSql, parameters);
        }

        // Метод для выдачи новой книги и обновления данных в базе данных
        private void IssueNewBookToDatabase(int rowIndex)
        {
            object cellValue = dataGridView2.Rows[rowIndex].Cells["Код книги"].Value;
            int bookCode = cellValue != DBNull.Value ? Convert.ToInt32(cellValue) : 0;
            int readerCode = dataGridView2.Rows[rowIndex].Cells["Код читателя"].Value is DBNull ? Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["Код читателя"].Value) : 0;
            DateTime issueDate = DateTime.Now.Date;

            string insertSql = @"
    INSERT INTO dbo.[Выдача и возврат] ([Код книги], [Код читателя], [Дата выдачи])
    VALUES (@BookCode, @ReaderCode, @IssueDate)";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@BookCode", SqlDbType.Int) { Value = bookCode },
        new SqlParameter("@ReaderCode", SqlDbType.Int) { Value = readerCode },
        new SqlParameter("@IssueDate", SqlDbType.Date) { Value = issueDate }
            };

            //DBWorker.ExecuteNonQuery(insertSql, parameters);
        }

        // Обработчик нажатия на кнопку
       





        private async void LoadStudentDetails(int readerCode)
        {
            string sqlCommand = @"
        SELECT 
            u.[Фамилия], 
            u.[Имя], 
            u.[Отчество], 
            g.[Название] AS [Группа], 
            f.[Название] AS [Факультет]
        FROM Пользователь u
        JOIN Группа g ON u.[Код группы] = g.[Код]
        JOIN Факультет f ON g.[Код факультета] = f.[Код]
        WHERE u.[Код] = @ReaderCode";

            DataTable studentDetailsTable = await DBWorker.GetDataTable(sqlCommand, new SqlParameter("@ReaderCode", readerCode));

            if (studentDetailsTable != null && studentDetailsTable.Rows.Count > 0)
            {
                DataRow studentDetails = studentDetailsTable.Rows[0];
                groupBox1.Visible = true;
                groupBox1.Dock = DockStyle.Top;
                textBox1.Text = $"{studentDetails["Фамилия"]} {studentDetails["Имя"]} {studentDetails["Отчество"]}";
                textBox2.Text = studentDetails["Группа"].ToString();
                textBox3.Text = studentDetails["Факультет"].ToString();
            }
        }









        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            
            splitContainer1.Panel1Collapsed = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable changes = ((DataTable)dataGridView2.DataSource).GetChanges();

                if (changes != null)
                {
                    await DBWorker.BeginTransaction(changes);

                    ((DataTable)dataGridView2.DataSource).AcceptChanges();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Проверьте правильность заполнения и попробуйте ещё раз", "Ошибка при заполнении данных");
                Console.WriteLine(ex);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}