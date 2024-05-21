using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibA
{
    public enum ReportType{
      With2Calendars,
      TextInput,
      None

    }
    public partial class Reports : Form
    {
        string sqlCommand;


        public Reports()
        {
            InitializeComponent();

        }
        private void Reports_SizeChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Right > b_with2Calendars.Left)
            {
                this.Width = dateTimePicker2.Right + b_with2Calendars.Width + 20;
            }
        }
        public async void MakeReport(string sqlcommand, ReportType rtype)
        {
            
            this.sqlCommand = sqlcommand;
            if (rtype is ReportType.With2Calendars)
            {
                gbox_with2Calendars.Visible = true;
                gbox_with2Calendars.Dock = DockStyle.Top;
            }
            else if (rtype is ReportType.TextInput) {

                gbox_WithTextInput.Visible = true;
                gbox_WithTextInput.Dock = DockStyle.Top;
                i_sterm.Width = b_sterm.Left; 
            }
            else if (rtype is ReportType.None)
            {
                DataTable dt = new();
                using (SqlCommand comm = new SqlCommand(this.sqlCommand, await ConnectionManager.Instance.OpenConnection()))
                {

                    dt = DBWorker.GetDataTable(comm);
                    dataGridViewMain.DataSource = dt;

                }


            }
            dataGridViewMain.Visible = true;
            dataGridViewMain.Dock = DockStyle.Fill;
            



        }
       
       

        private async void b_with2Calendars_Click(object sender, System.EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            

            if (date1 <= date2)
            {
                string startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                DataTable dt = new();


                
                dt = await DBWorker.GetDataTable(this.sqlCommand,startDate,endDate);
                dataGridViewMain.DataSource = dt;

               

            }
        }
        private async void b_sterm_Click(object sender, EventArgs e)
        {
            DataTable dt = new();
            dt = await DBWorker.GetDataTable(this.sqlCommand, i_sterm.Text);
            dataGridViewMain.DataSource = dt;

        }

        
    }

}

