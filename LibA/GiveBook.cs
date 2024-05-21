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

            DataTable readerTable = await DBWorker.GetDataTable($"select * from Читатель where [Код группы] in (select Код from Группа where [Название] = '{v}')");

           
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
            }
        }

        private async void LoadIssueReturn(int readerCode)
        {
            string sqlCommand = "SELECT TOP 0 * FROM [Выдача и возврат]";

            DataTable issueReturnTable = await DBWorker.GetDataTable(sqlCommand);

            if (issueReturnTable != null)
            {
                issueReturnTable.Columns.Remove("Код");
                issueReturnTable.Columns["Код читателя"].DefaultValue = readerCode;
                issueReturnTable.TableName = "Выдача и возврат";
                button1.Visible = true;
                button2.Visible = true;
                dataGridView2.Visible = true;
                dataGridView2.Dock = DockStyle.Fill;
                dataGridView2.DataSource = issueReturnTable;

                DataRow newRow = issueReturnTable.NewRow();
                //newRow["Код читателя"] = readerCode;
                issueReturnTable.Rows.Add(newRow);
            }

        }







        private async void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string headerText = dataGridView2.Columns[e.ColumnIndex].HeaderText.ToLower();

                if (headerText.StartsWith("код") || headerText.StartsWith("id"))
                {
                    string columnName = dataGridView2.Columns[e.ColumnIndex].Name;
                    string ctableName = "Выдача и возврат";
                    Console.WriteLine($"{columnName}, {ctableName}");
                    List<string> tableNames = await DBWorker.GetLinkedTableNames(ctableName, columnName);

                    if (tableNames.Count > 0)
                    {
                        foreach (string tableName in tableNames)
                        {
                            DataTable dependentTable = await DBWorker.GetDataTable(tableName);

                            if (dependentTable != null)
                            {
                                string cellValueString = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                                int val = string.IsNullOrEmpty(cellValueString) ? 0 : int.Parse(cellValueString);
                                DLinker dLinkerForm = new DLinker(dependentTable, val);

                                dLinkerForm.SelectionMade += (selectedValue) =>
                                {
                                    dataGridView2.EndEdit();
                                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selectedValue;
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

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
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
            catch {
                MessageBox.Show("Проверьте правильность заполнения и попробуйте ещё раз", "Ошибка при заполнении данных");
            }
        }

        
    }
}