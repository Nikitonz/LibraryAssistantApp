namespace LibA
{
    partial class UserPanel
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPanel));
            this.DBStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.регистрацияИАвторизацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зарегистрироватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.авторизироватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.изУчетнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изПриложенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.администрированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разработчикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statText = new System.Windows.Forms.ToolStripStatusLabel();
            this.IsDBAliveTimer = new System.Windows.Forms.Timer(this.components);
            this.authFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DBStat
            // 
            this.DBStat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.DBStat.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.DBStat.Image = global::LibA.Properties.Resources.ok;
            this.DBStat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DBStat.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.DBStat.Name = "DBStat";
            this.DBStat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DBStat.Size = new System.Drawing.Size(148, 24);
            this.DBStat.Text = "База данных: OK";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.администрированиеToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(815, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.регистрацияИАвторизацияToolStripMenuItem,
            this.выходToolStripMenuItem1});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 26);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // регистрацияИАвторизацияToolStripMenuItem
            // 
            this.регистрацияИАвторизацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.зарегистрироватьсяToolStripMenuItem,
            this.авторизироватьсяToolStripMenuItem});
            this.регистрацияИАвторизацияToolStripMenuItem.Name = "регистрацияИАвторизацияToolStripMenuItem";
            this.регистрацияИАвторизацияToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.регистрацияИАвторизацияToolStripMenuItem.Text = "Регистрация и авторизация";
            // 
            // зарегистрироватьсяToolStripMenuItem
            // 
            this.зарегистрироватьсяToolStripMenuItem.Name = "зарегистрироватьсяToolStripMenuItem";
            this.зарегистрироватьсяToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.зарегистрироватьсяToolStripMenuItem.Text = "Зарегистрироваться";
            this.зарегистрироватьсяToolStripMenuItem.Click += new System.EventHandler(this.зарегистрироватьсяToolStripMenuItem_Click);
            // 
            // авторизироватьсяToolStripMenuItem
            // 
            this.авторизироватьсяToolStripMenuItem.Name = "авторизироватьсяToolStripMenuItem";
            this.авторизироватьсяToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.авторизироватьсяToolStripMenuItem.Text = "Авторизироваться";
            this.авторизироватьсяToolStripMenuItem.Click += new System.EventHandler(this.авторизироватьсяToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem1
            // 
            this.выходToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изУчетнойЗаписиToolStripMenuItem,
            this.изПриложенияToolStripMenuItem});
            this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
            this.выходToolStripMenuItem1.Size = new System.Drawing.Size(286, 26);
            this.выходToolStripMenuItem1.Text = "Выход";
            this.выходToolStripMenuItem1.Click += new System.EventHandler(this.выходToolStripMenuItem1_Click);
            // 
            // изУчетнойЗаписиToolStripMenuItem
            // 
            this.изУчетнойЗаписиToolStripMenuItem.Name = "изУчетнойЗаписиToolStripMenuItem";
            this.изУчетнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.изУчетнойЗаписиToolStripMenuItem.Text = "Из учетной записи";
            this.изУчетнойЗаписиToolStripMenuItem.Click += new System.EventHandler(this.изУчетнойЗаписиToolStripMenuItem_Click);
            // 
            // изПриложенияToolStripMenuItem
            // 
            this.изПриложенияToolStripMenuItem.Name = "изПриложенияToolStripMenuItem";
            this.изПриложенияToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.изПриложенияToolStripMenuItem.Text = "Из приложения";
            this.изПриложенияToolStripMenuItem.Click += new System.EventHandler(this.изПриложенияToolStripMenuItem_Click);
            // 
            // администрированиеToolStripMenuItem
            // 
            this.администрированиеToolStripMenuItem.Name = "администрированиеToolStripMenuItem";
            this.администрированиеToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.администрированиеToolStripMenuItem.Text = "Администрирование";
            this.администрированиеToolStripMenuItem.Click += new System.EventHandler(this.администрированиеToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.settingsToolStripMenuItem.Text = "Настройки";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem,
            this.разработчикToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // разработчикToolStripMenuItem
            // 
            this.разработчикToolStripMenuItem.Name = "разработчикToolStripMenuItem";
            this.разработчикToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.разработчикToolStripMenuItem.Text = "Разработчик";
            this.разработчикToolStripMenuItem.Click += new System.EventHandler(this.разработчикToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DBStat,
            this.statText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(815, 30);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statText
            // 
            this.statText.Name = "statText";
            this.statText.Size = new System.Drawing.Size(12, 24);
            this.statText.Text = "·";
            // 
            // IsDBAliveTimer
            // 
            this.IsDBAliveTimer.Enabled = true;
            this.IsDBAliveTimer.Interval = 6000;
            this.IsDBAliveTimer.Tick += new System.EventHandler(this.IsDBAliveTimer_Tick);
            // 
            // authFormBindingSource
            // 
            this.authFormBindingSource.DataSource = typeof(LibA.AuthForm);
            // 
            // UserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 368);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(349, 47);
            this.Name = "UserPanel";
            this.Text = "Меню библиотеки";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem регистрацияИАвторизацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зарегистрироватьсяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem авторизироватьсяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem разработчикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem изУчетнойЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изПриложенияToolStripMenuItem;
        private System.Windows.Forms.BindingSource authFormBindingSource;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel statText;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem администрированиеToolStripMenuItem;
        private System.Windows.Forms.Timer IsDBAliveTimer;
        private System.Windows.Forms.ToolStripStatusLabel DBStat;
    }
}

