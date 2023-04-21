namespace WindowsFormsApp1
{
    partial class AuthForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
            this.regbox = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.authbox = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.authFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.regbox.SuspendLayout();
            this.authbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // regbox
            // 
            this.regbox.BackColor = System.Drawing.SystemColors.Control;
            this.regbox.Controls.Add(this.comboBox1);
            this.regbox.Controls.Add(this.linkLabel1);
            this.regbox.Controls.Add(this.label7);
            this.regbox.Controls.Add(this.textBox5);
            this.regbox.Controls.Add(this.label6);
            this.regbox.Controls.Add(this.label5);
            this.regbox.Controls.Add(this.textBox2);
            this.regbox.Controls.Add(this.button1);
            this.regbox.Controls.Add(this.label2);
            this.regbox.Controls.Add(this.label1);
            this.regbox.Controls.Add(this.textBox1);
            this.regbox.Location = new System.Drawing.Point(13, 25);
            this.regbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.regbox.Name = "regbox";
            this.regbox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.regbox.Size = new System.Drawing.Size(240, 402);
            this.regbox.TabIndex = 0;
            this.regbox.TabStop = false;
            this.regbox.Text = "Регистрация";
            this.regbox.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(57, 386);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(127, 16);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Авторизируйтесь!";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 367);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(219, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Уже есть аккаунт?";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(9, 265);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(224, 22);
            this.textBox5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 245);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Пароль";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 175);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Имя для входа";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(9, 198);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(223, 22);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(32, 310);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "Зарегистрироваться!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Должность";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "ФИО";

            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 58);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 22);
            this.textBox1.TabIndex = 0;
            // 
            // authbox
            // 
            this.authbox.BackColor = System.Drawing.SystemColors.Control;
            this.authbox.Controls.Add(this.textBox4);
            this.authbox.Controls.Add(this.button2);
            this.authbox.Controls.Add(this.textBox3);
            this.authbox.Controls.Add(this.label4);
            this.authbox.Controls.Add(this.label3);
            this.authbox.Location = new System.Drawing.Point(259, 25);
            this.authbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.authbox.Name = "authbox";
            this.authbox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.authbox.Size = new System.Drawing.Size(277, 146);
            this.authbox.TabIndex = 1;
            this.authbox.TabStop = false;
            this.authbox.Text = "Авторизация";
            this.authbox.Visible = false;

            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(112, 76);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.MaxLength = 20;
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '*';
            this.textBox4.Size = new System.Drawing.Size(159, 22);
            this.textBox4.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(9, 121);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(261, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Авторизироваться";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(112, 39);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(159, 22);
            this.textBox3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Имя для входа";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Гость",
            "Читатель",
            "Библиотекарь",
            "Директор"});
            this.comboBox1.Location = new System.Drawing.Point(7, 121);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(225, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // authFormBindingSource
            // 
            this.authFormBindingSource.DataSource = typeof(WindowsFormsApp1.AuthForm);
            // 
            // AuthForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(637, 572);
            this.Controls.Add(this.authbox);
            this.Controls.Add(this.regbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AuthForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "RegAuth";
            this.Text = "Rigister/Login window";
            this.Load += new System.EventHandler(this.AuthForm_Load);
            this.regbox.ResumeLayout(false);
            this.regbox.PerformLayout();
            this.authbox.ResumeLayout(false);
            this.authbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authFormBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox regbox;
        private System.Windows.Forms.GroupBox authbox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource authFormBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}