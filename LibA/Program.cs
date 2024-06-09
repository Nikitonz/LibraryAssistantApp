using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    
    internal static class Program
    {
   
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //ConnectionManager.Instance.SetupConnectionString("Guest1", "1");
            Application.Run(new UserPanel());
           
        }
        public static void MakeFocus(Form o) {
            
            o.Show();
            o.WindowState = FormWindowState.Normal;
            o.Focus();
            Cursor.Position = new System.Drawing.Point(
            o.Left + o.Width / 2,
            o.Top + o.Height / 2
            );
        }    
    }
}
