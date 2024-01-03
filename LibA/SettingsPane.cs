using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LibA
{
    public partial class SettingsPane : Form
    {
        private static SettingsPane instance;
        private static bool isClosed = true;

        public static SettingsPane Instance
        {
            get
            {
                if (instance == null || isClosed)
                {
                    instance = new SettingsPane();
                    instance.FormClosed += (sender, e) =>
                    {
                        isClosed = true;
                    };
                    isClosed = false;
                }
                return instance;
            }
        }


        public SettingsPane(){
            InitializeComponent();

            dataSource.Text = Properties.Settings.Default.dbConnSourceAddr.ToString();
            port.Text = Properties.Settings.Default.DBPort;
            AcceptButton = saveState;
            Focus();
            Text = "Настройки";
            
        }
      
















        private void saveState_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.dbConnSourceAddr = dataSource.Text.Replace(',','.').Replace(" ","");

            Properties.Settings.Default.DBPort = port.Text;
            //Properties.Settings.Default.dbConnSourceInstName = servName.Text;


            
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.Connect(Properties.Settings.Default.dbConnSourceAddr, Int32.Parse(port.Text));
                    tcpClient.Close();
                }


                Properties.Settings.Default.dbConnSourceAddr = dataSource.Text;
                //Properties.Settings.Default.dbConnSourceInstName = servName.Text;
                Properties.Settings.Default.Save();
                System.Windows.Forms.MessageBox.Show("Данные обновлены");
                this.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Ошибка!");
                dataSource.Text = Properties.Settings.Default.dbConnSourceAddr.ToString();
                //servName.Text = Properties.Settings.Default.dbConnSourceInstName.ToString();
            }





        }


    }
}
