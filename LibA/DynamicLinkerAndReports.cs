using System;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    public partial class DynamicLinkerAndReports : Form
    {

        public int selectedRowCode;


        public DynamicLinkerAndReports()
        {
            InitializeComponent();
          
        }

        public void MakeData(System.Data.DataTable dt) { 
            dataGridViewMain.DataSource = dt;
            dataGridViewMain.Visible = true;
            dataGridViewMain.RowHeadersVisible = false;
            dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            foreach (DataGridViewRow row in dataGridViewMain.Rows)
            {
                int rowIndex = row.Index;
                row.Height = row.GetPreferredHeight(rowIndex, DataGridViewAutoSizeRowMode.AllCells, true);
            }

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
    }

}

