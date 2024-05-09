using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace LibA
{
    public partial class DLinker : Form
    {
        public delegate void SelectionDelegate(string selectedValue);
        public event SelectionDelegate SelectionMade;
        public DLinker(DataTable dt)
        {
            
            InitializeComponent();
            dataGridViewMain.DataSource = dt;
            dataGridViewMain.Columns[0].Visible = false;
            this.Text = $"Режим выбора: {dt.TableName}";
        }

        private async void dataGridViewMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >=0)
            {

                string headerText = dataGridViewMain.Columns[e.ColumnIndex].HeaderText.ToLower();


                if ((headerText.StartsWith("код") || headerText.StartsWith("id"))&& !(headerText.Equals("код", StringComparison.OrdinalIgnoreCase) || headerText.Equals("id", StringComparison.OrdinalIgnoreCase)) )
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
                                DLinker dLinkerForm = new DLinker(dependentTable);
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
            else
            {

                string selectedValue = dataGridViewMain.Rows[e.RowIndex].Cells[0].Value.ToString();
                SelectionMade?.Invoke(selectedValue);
                this.Close();
            }
        }
    }

}
