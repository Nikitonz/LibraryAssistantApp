using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    public partial class SettingsPane : Form
    {
        public SettingsPane()
        {
           
            InitializeComponent();
            dataSource.Text = Properties.Settings.Default.dbConnStrMain.ToString();
        }

        private void saveState_Click(object sender, EventArgs e)
        {
            //checks goes here???
            Properties.Settings.Default.dbConnStrMain = dataSource.Text;
            MessageBox.Show("Изменения сохранены");
        }

        
    }
}
