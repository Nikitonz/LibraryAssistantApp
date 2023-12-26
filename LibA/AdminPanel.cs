using System;
using System.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace LibA
{
    public partial class AdminPanel : Form
    {
        
        public AdminPanel()
        {
            InitializeComponent();
            LoadTablesAsync();
            
        }

     

        private async void LoadTablesAsync()
        {
            Console.WriteLine("Task load STARTED");
            string[] tables = await DBWorker.BdGetDataMSSQL("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' order by table_name ASC");
            if (tables != null)
            {
                Tables.Items.AddRange(tables);
                Console.WriteLine("Task load end");
            }
        }

        private async void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = Tables.Items[Tables.SelectedIndex>0?Tables.SelectedIndex:0].ToString();
            DataTable datatable = await DBWorker.GetDataTable($"SELECT * FROM [{selectedTable}]");
            if (datatable != null)
            {
                dataGridViewMain.DataSource = datatable;
                dataGridViewMain.Enabled = true;
                dataGridViewMain.Visible = true;
                dataGridViewMain.Columns[0].Visible = false;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            this.Close();
            this.Dispose();
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

            // Перемещение DataGridViewMain внутри Panel2
            dataGridViewMain.Dock = DockStyle.Fill;
        }


    }
}
