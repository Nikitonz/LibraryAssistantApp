using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using OfficeOpenXml;
using System.IO;

namespace LibA
{
    public enum ReportType{
      With2Calendars,
      TextInput,
      None

    }
    public partial class Reports : Form
    {
        string sqlCommand;
        string oName;


        public Reports()
        {
            InitializeComponent();
            button1.Visible = false;
        }
        private void Reports_SizeChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Right > b_with2Calendars.Left)
            {
                this.Width = dateTimePicker2.Right + b_with2Calendars.Width + 20;
            }
            
        }
        public async void MakeReport(string sqlcommand, ReportType rtype)
        {
            
            this.sqlCommand = sqlcommand;
            if (rtype is ReportType.With2Calendars)
            {
                gbox_with2Calendars.Visible = true;
                gbox_with2Calendars.Dock = DockStyle.Top;
            }
            else if (rtype is ReportType.TextInput) {

                gbox_WithTextInput.Visible = true;
                gbox_WithTextInput.Dock = DockStyle.Top;
                i_sterm.Width = b_sterm.Left; 
            }
            else if (rtype is ReportType.None)
            {
               
                using (SqlCommand comm = new SqlCommand(this.sqlCommand, await ConnectionManager.Instance.OpenConnection()))
                {

                    DataTable dt = DBWorker.GetDataTable(comm);
                    dataGridViewMain.DataSource = dt;

                }
                button1.Visible = true;

            }
            dataGridViewMain.Visible = true;
            dataGridViewMain.Dock = DockStyle.Fill;
            



        }
       
       

        private async void b_with2Calendars_Click(object sender, System.EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            

            if (date1 <= date2)
            {
                string startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                DataTable dt = new();


                
                dt = await DBWorker.GetDataTable(this.sqlCommand,startDate,endDate);
                dataGridViewMain.DataSource = dt;

               

            }
            button1.Visible = true;
        }
        private async void b_sterm_Click(object sender, EventArgs e)
        {
            DataTable dt = new();
            dt = await DBWorker.GetDataTable(this.sqlCommand, i_sterm.Text);
            dataGridViewMain.DataSource = dt;
            button1.Visible = true;
        }





        private void printButton_Click(object sender, EventArgs e)
        {
            // Получаем название отчета из объекта sender
           

            // Создаем DataTable и копируем данные из DataGridView
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn column in dataGridViewMain.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType);
            }
            foreach (DataGridViewRow row in dataGridViewMain.Rows)
            {
                DataRow newRow = dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    newRow[cell.ColumnIndex] = cell.Value;
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.FileName = $"{this.Text}.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(saveFileDialog.FileName);
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    try
                    {
                        // Пытаемся удалить лист, если он существует
                        ExcelWorksheet existingWorksheet = package.Workbook.Worksheets[$"{this.Text}"];
                        if (existingWorksheet != null)
                        {
                            package.Workbook.Worksheets.Delete(existingWorksheet);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(this.Text); // Используем название отчета

                    // Сохраняем заголовки столбцов
                    for (int col = 1; col <= dt.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col].Value = dt.Columns[col - 1].ColumnName;
                    }

                    // Сохраняем данные из DataTable
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            if (dt.Columns[col].DataType == typeof(DateTime))
                            {
                                worksheet.Cells[row + 2, col + 1].Value = (DateTime)dt.Rows[row][col];
                                worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "yyyy-MM-dd"; // Установка формата даты
                            }
                            else
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dt.Rows[row][col];
                            }
                        }
                    }

                    package.Save();
                }

                MessageBox.Show("Готово.", "1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




    }

}

