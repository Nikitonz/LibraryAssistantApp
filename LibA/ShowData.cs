using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibA
{
    public partial class ShowData : Form
    {
        private GroupBox groupBox;
        public ShowData(DataTable dt)
        {
            InitializeComponent();
            try
            {
                SetData(dt);
                ShowData_Load();
                this.Resize += new EventHandler(Form1_Resize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            groupBox.Left = this.ClientSize.Width - groupBox.Width - 20; // Привязка к правому краю
            groupBox.Top = this.ClientSize.Height - groupBox.Height - 20; // Привязка к нижнему краю
        }
        private void SetData(DataTable dt)
        {
            
            try
            {
                dataGridView1.DataSource = dt;
                dataGridView1.ReadOnly = true;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = false;
                    }
                }
                foreach (DataGridViewColumn c in dataGridView1.Columns)
                {
                    if (c.HeaderText.ToLower().StartsWith("код"))
                    {
                        c.Visible = false;
                    }
                }

                // Добавляем столбец "Изменить"
                DataGridViewLinkColumn editColumn = new DataGridViewLinkColumn
                {
                    HeaderText = "Изменить строку",
                    Text = "Изменить",
                    UseColumnTextForLinkValue = true
                };
                dataGridView1.Columns.Add(editColumn);

                // Добавляем столбец "Удалить"
                DataGridViewLinkColumn deleteColumn = new DataGridViewLinkColumn
                {
                    HeaderText = "Удалить строку",
                    Text = "Удалить",
                    UseColumnTextForLinkValue = true
                };
                dataGridView1.Columns.Add(deleteColumn);

                dataGridView1.CellContentClick += DataGridView1_CellContentClick;
                dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
                dataGridView1.RowLeave += DataGridView1_RowLeave;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при установке данных: " + ex.Message);
            }
        }

        private void ShowData_Load()
        {
            groupBox = new GroupBox
            {
                Text = "Действия",
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
                Left = this.ClientSize.Width - 200,
                Top = this.ClientSize.Height - 80
            };

            this.Controls.Add(groupBox);

            // Кнопка "Добавить"
            Button buttonAdd = new Button
            {
                Text = "Добавить",
                Width = 80,
                Left = 10,
                Top = 20
            };
            buttonAdd.Click += ButtonAdd_Click;
            groupBox.Controls.Add(buttonAdd);

            // Кнопка "Применить"
            Button buttonApply = new Button
            {
                Text = "Применить",
                Width = 80,
                Left = 100,
                Top = 20
            };
            buttonApply.Click += ButtonApply_Click;
            groupBox.Controls.Add(buttonApply);

            groupBox.BringToFront();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                // Логика для сохранения данных
                MessageBox.Show("Данные сохранены успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow()
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении строки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Изменить строку")
                    {
                        dataGridView1.ReadOnly = true;
                        foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                        {
                            cell.ReadOnly = false;
                            cell.Style.BackColor = Color.LightYellow; // Опционально: изменяем цвет ячеек для визуального обозначения редактирования
                        }
                        
                        // Переключаем текущую ячейку на первую ячейку в строке для начала редактирования
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                        dataGridView1.BeginEdit(true);
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Удалить строку")
                    {
                        // Удаляем текущую строку
                        DeleteRow(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обработке действия: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                    {
                        cell.ReadOnly = true;
                        cell.Style.BackColor = Color.White; // Опционально: возвращаем цвет ячеек к исходному
                    }
                    dataGridView1.EndEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при отключении редактирования: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // После завершения редактирования, отключаем редактирование для строки
            foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
            {
                cell.ReadOnly = true;
                cell.Style.BackColor = Color.White;
            }
        }

        private void DeleteRow(int rowIndex)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.Rows.RemoveAt(rowIndex);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении строки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}