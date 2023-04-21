using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        public Form1()
        {

            /* 
             groupBox7.Visible = false;
             groupBox8.Visible = false;
             groupBox9.Visible = false;*/
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            вызовТаблицыToolStripMenuItem.Visible = false;
            dataGridView1.Visible = false;
            DBO dbo = new DBO();

        }

        public void SetInvisible()
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox groupBox)
                {
                    groupBox.Visible = false;
                }
            }
        }
        public void SetInvisible(GroupBox groupBoxToKeepVisible)
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox groupBox)
                {
                    if (groupBox != groupBoxToKeepVisible)
                    {
                        groupBox.Visible = false;
                    }
                }
            }
        }

        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {


            DialogResult = MessageBox.Show("Developer: Никита Обухов\nemail: nikitoniy2468@gmail.com\nAll rights reserved.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        private void авторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox1.Visible = true;
            groupBox1.Location = new Point(19, 26);
            groupBox1.Size = new Size(213, 340);


        }
        private void издательстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox2.Visible = true;
            groupBox2.Location = new Point(19, 26);
            groupBox2.Size = new Size(213, 340);
        }

        private void жанрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox3.Visible = true;
            groupBox3.Location = new Point(19, 26);
            groupBox3.Size = new Size(213, 340);
        }

        private void книгаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox6.Visible = true;
            groupBox6.Location = new Point(19, 26);
            groupBox6.Size = new Size(213, 340);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string que = $"INSERT INTO Авторы (Фамилия, Имя, Отчество, [Страна автора]) VALUES ({textBox1.Text}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text})";
            try
            {
                DBO.ExecuteQuery(que);
                MessageBox.Show("Успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка добавления!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /* string que = $"update Авторы (Фамилия, Имя, Отчество, [Страна автора]) set ({textBox1.Text}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text}) WHERE SEARCHRESULT";
             try
             {
                 DBO.ExecuteQuery(que);
                 MessageBox.Show("Успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             catch (Exception ex) { MessageBox.Show("Ошибка обновления!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*string que = $"delete from Авторы WHERE SEARCHRESULT ";
            DialogResult res = MessageBox.Show("Точно удалить ?", "Уточнение", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (res == DialogResult.OK)
            {
                try
                {
                    DBO.ExecuteQuery(que);
                    MessageBox.Show("Успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("Ошибка добавления!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void disp_Авторы_Click(object sender, EventArgs e)
        {
            dataGridView1.Location = new Point(241, 34);
            dataGridView1.Size = new Size(585, 286);
            string sqlQuery = "SELECT * FROM Авторы";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                table.Columns.RemoveAt(0);
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = table;
                dataGridView1.Visible = true;
            } catch (Exception er) { MessageBox.Show("Ошибка отображения! " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void проверитьПодключениеКБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection is null)
                    throw new Exception("Вы не авторизированы!");
                connection?.Open();
                MessageBox.Show("Успешно подключено", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Невозможно подключиться\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }




        async private void авторизироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthForm authreg = new AuthForm();
            try
            {
                
                authreg.WhichWindow("authorize");
                
                connection = authreg.GetConnection;
                Thread.Sleep(1000);
                if (connection != null)
                    MessageBox.Show("settled connection");
                else
                    MessageBox.Show("lee kuda", "hell no");
            }
            catch (Exception er) {
            
                
                Console.WriteLine(er);
            }
            //connection.Open();


        }

        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthForm authForm= new AuthForm();
            authForm.WhichWindow("register");

         
        }

        private void вызовТаблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void книгиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox6.Visible = true;
            groupBox6.Location = new Point(19, 26);
            groupBox6.Size = new Size(213, 340);
        }

        private void авторыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox1.Visible = true;
            groupBox1.Location = new Point(19, 26);
            groupBox1.Size = new Size(213, 340);
        }

        private void жанрыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox3.Visible = true;
            groupBox3.Location = new Point(19, 26);
            groupBox3.Size = new Size(213, 340);
        }

        private void издательстваToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInvisible();
            groupBox2.Visible = true;
            groupBox2.Location = new Point(19, 26);
            groupBox2.Size = new Size(213, 340);
        }
    }
}
