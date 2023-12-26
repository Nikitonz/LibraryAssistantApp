using LibA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
            Tables.Items.Add(DBWorker.BdGetDataMSSQL);
        }

      

        private void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
