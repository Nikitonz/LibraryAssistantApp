using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibA
{
   
    public partial class DynamicLinkerAndReports : Form
    {

        public int selectedRowCode;
        private string sqlcommand;


        public DynamicLinkerAndReports()
        {
            InitializeComponent();

        }
        public void MakeData(string sqlcommand)
        {
            uPane.Panel1.Show();
            groupBox1.Visible= true;
            groupBox1.Dock = DockStyle.Fill;
            this.sqlcommand = sqlcommand;
        }
        public void MakeData(DataTable dt)
        {

            dataGridViewMain.DataSource = dt;
            dataGridViewMain.Visible = true;
            dataGridViewMain.RowHeadersVisible = false;
            dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            foreach (DataGridViewRow row in dataGridViewMain.Rows)
            {
                int rowIndex = row.Index;
                row.Height = row.GetPreferredHeight(rowIndex, DataGridViewAutoSizeRowMode.AllCells, true);
            }
            //dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders;


        }

        private void dataGridViewMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                object codeValue = dataGridViewMain.Rows[e.RowIndex].Cells["Код"].Value;
                if (codeValue != null && int.TryParse(codeValue.ToString(), out int code))
                {
                    selectedRowCode = code;
                    this.Close();
                }
            }
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            

            if (date1 <= date2)
            {
                string formattedDate1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string formattedDate2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                DataTable dt = new();
                using (SqlCommand comm = new SqlCommand(this.sqlcommand, await ConnectionManager.Instance.OpenConnection()))
                {
                    comm.Parameters.AddWithValue("@startdate", formattedDate1);
                    comm.Parameters.AddWithValue("@enddate", formattedDate2);
                    dt = DBWorker.GetDataTable(comm);
                    MakeData(dt);
                }
            }
        }
    }

}

