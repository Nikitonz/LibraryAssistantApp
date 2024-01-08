using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
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

        private async void статистикаИспользованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynamicLinkerAndReports dln = new DynamicLinkerAndReports();
            DataTable data = await DBWorker.GetDataTable("SELECT * FROM dbo.GetPercentageUsersByGroupAndFaculty('2020-01-01', '2024-12-31')");
            dln.MakeData(data);
            dln.ShowDialog();
        }

        private async void проверитьКнигиУЧитателяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynamicLinkerAndReports dln = new DynamicLinkerAndReports();
            DataTable data = await DBWorker.GetDataTable("SELECT * FROM dbo.GetDebtorsReport('2020-01-01', '2024-12-31');");
            dln.MakeData(data);
            dln.ShowDialog();
        }

        private async void должникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynamicLinkerAndReports dln = new DynamicLinkerAndReports();
            DataTable data = await DBWorker.GetDataTable("SELECT * from dbo.GetBookStatusAndDueDate(3,2)");
            dln.MakeData(data);
            dln.ShowDialog();
        }

        private async void перевестиГруппыНаСледующийГодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DBWorker.ExecProcedure("УвеличитьКурсГрупп");
        }


       

        private async void dataGridViewMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
              
                string columnName = dataGridViewMain.Columns[e.ColumnIndex].Name;

             
                if (columnName.ToLower().StartsWith("код") || columnName.ToLower().StartsWith("id"))
                {

                    int selectedRow = 0;
                    DataTable dataTable = await GetDataForTable(columnName);
                    using (DynamicLinkerAndReports linkerAndReports = new DynamicLinkerAndReports()) {
                        
                        linkerAndReports.MakeData(dataTable);
                        linkerAndReports.ShowDialog();
                        selectedRow = linkerAndReports.selectedRowCode;
                    }
                    dataGridViewMain.EndEdit();
                    dataGridViewMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selectedRow;
                    dataGridViewMain.InvalidateCell(e.ColumnIndex, e.RowIndex);

                }
            }
        }

        


        private async Task<DataTable> GetDataForTable(string columnName)
        {
            DataTable dataTable = new DataTable();

          
            string lowerColumnName = columnName.ToLower();

            string[] tablesSM = await DBWorker.BdGetDataMSSQL("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' order by table_name ASC");
            List<string> tables = new List<string>(tablesSM);

            

            



            string matchingTableName = DBWorker.FindMatchingTableName(lowerColumnName, tables);

            if (!string.IsNullOrEmpty(matchingTableName))
            {
                dataTable = await DBWorker.GetDataTable(matchingTableName);
            }

            return dataTable;
        }

        

        
    }
}
