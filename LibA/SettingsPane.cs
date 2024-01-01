using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace LibA
{
    public partial class SettingsPane : Form
    {

       

        public SettingsPane()
        {
           
            InitializeComponent();
            dataSource.Text = Properties.Settings.Default.dbConnSourceAddr.ToString();
        }

        private void saveState_Click(object sender, EventArgs e)
        {
            string connectionString = $"Data Source={Properties.Settings.Default.dbConnSourceAddr}; Initial Catalog={Properties.Settings.Default.dbConnSourceName}; Database ={ Properties.Settings.Default.ICatalog};;User Id=librarian; Password='123321'";
            try
            {
          
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
     
                }
                Properties.Settings.Default.dbConnSourceAddr = dataSource.Text;
                Properties.Settings.Default.dbConnSourceName = servName.Text;
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                dataSource.Text = Properties.Settings.Default.dbConnSourceAddr.ToString();
            }
           
     
        }

        
    }
}
