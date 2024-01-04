using System;
using System.Data;
using System.Drawing;
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


            Tables.Height = Tables.ItemHeight * (Tables.Items.Count+1);
        
            

            buttonTransact.Location = new Point(
                Tables.Left,
                Tables.Bottom
            );

            buttonRollback.Location = new Point(
                buttonTransact.Left,
                buttonTransact.Bottom
            );

            this.Height = buttonRollback.Bottom + 65;
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

        private async void buttonTransact_Click(object sender, EventArgs e)
        {
            try
            {
                await DBWorker.BeginTransaction(dataGridViewMain);

                buttonRollback.Enabled = true;
                buttonRollback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                MessageBox.Show("Изменения сохранены в базе данных.");
            }
            catch {

            }
        }

        private void buttonRollback_Click(object sender = null, EventArgs e = null)
        {
            dataGridViewMain.DataSource = DBWorker.RollbackTransaction();
            buttonRollback.Enabled = false;
            buttonRollback.BackColor = Color.Gray;
            MessageBox.Show("Откат выполнен успешно");
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
    }
}
