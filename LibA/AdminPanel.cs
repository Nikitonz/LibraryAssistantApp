using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{

    public partial class AdminPanel : Form
    {

        public static AdminPanel Instance { get; private set; }
        public AdminPanel()
        {
            InitializeComponent();
            LoadTablesAsync();
            Instance = this;
        }



        private async void LoadTablesAsync()
        {

            string[] tables = await DBWorker.BdGetDataMSSQL("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' order by table_name ASC");
            if (tables != null)
            {
                Tables.Items.AddRange(tables);

            }

            int maxHeight = 1;


            foreach (var item in Tables.Items)
            {
                string text = item.ToString();
                int itemHeight = (int)TextRenderer.MeasureText(text, Tables.Font).Height;

                if (itemHeight > maxHeight)
                {
                    maxHeight = itemHeight;
                }
            }
            Tables.ItemHeight = maxHeight;
            int totalHeight = Tables.ItemHeight * Tables.Items.Count;
            Tables.Height = totalHeight;
            buttonTransact.Location = new Point(
                Tables.Left, 
                Tables.Bottom
            );

            buttonRollback.Location = new Point(
                buttonTransact.Left,
                buttonTransact.Bottom
            );
            this.Height = buttonRollback.Bottom+65;

        }

        private async void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = Tables.Items[Tables.SelectedIndex > 0 ? Tables.SelectedIndex : 0].ToString();
            DataTable datatable = await DBWorker.GetDataTable(selectedTable);
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

            dataGridViewMain.Dock = DockStyle.Fill;
        }

        private void buttonTransact_Click(object sender, EventArgs e)
        {
            //DBWorker.BeginTransaction(dataGridViewMain);
            buttonRollback.Enabled = true;
            buttonRollback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))); ;
        }

        private void buttonRollback_Click(object sender = null, EventArgs e = null)
        {

            //DBWorker.RollbackTransaction();

            buttonRollback.Enabled =false;
            buttonRollback.BackColor = Color.Gray;

        }

        public static implicit operator AdminPanel(ConnectionManager v)
        {
            throw new NotImplementedException();
        }
    }
}
