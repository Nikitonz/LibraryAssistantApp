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
            this.DBStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.регистрацияИАвторизацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зарегистрироватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.авторизироватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.изУчетнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изПриложенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разработчикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользователиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.факультетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.читателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.книжныйФондToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выдатьСдатьКнигиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.администрированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statText = new System.Windows.Forms.ToolStripStatusLabel();
            this.IsDBAliveTimer = new System.Windows.Forms.Timer(this.components);
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchInput = new System.Windows.Forms.TextBox();
            this.doSearch = new System.Windows.Forms.Button();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.authFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DBStat
            // 
            this.DBStat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.DBStat.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.DBStat.Image = global::LibA.Properties.Resources.error;
            this.DBStat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DBStat.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.DBStat.Name = "DBStat";
            this.DBStat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DBStat.Size = new System.Drawing.Size(156, 24);
            this.DBStat.Text = "База данных: ОШИБКА";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.оПрограммеToolStripMenuItem,
            this.пользователиToolStripMenuItem,
            this.книжныйФондToolStripMenuItem,
            this.поискToolStripMenuItem1,
            this.администрированиеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(732, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.регистрацияИАвторизацияToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.выходToolStripMenuItem1});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // регистрацияИАвторизацияToolStripMenuItem
            // 
            this.регистрацияИАвторизацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.зарегистрироватьсяToolStripMenuItem,
            this.авторизироватьсяToolStripMenuItem});
            this.регистрацияИАвторизацияToolStripMenuItem.Name = "регистрацияИАвторизацияToolStripMenuItem";
            this.регистрацияИАвторизацияToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.регистрацияИАвторизацияToolStripMenuItem.Text = "Регистрация и авторизация";
            // 
            // зарегистрироватьсяToolStripMenuItem
            // 
            this.зарегистрироватьсяToolStripMenuItem.Name = "зарегистрироватьсяToolStripMenuItem";
            this.зарегистрироватьсяToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.зарегистрироватьсяToolStripMenuItem.Text = "Зарегистрироваться";
            this.зарегистрироватьсяToolStripMenuItem.Click += new System.EventHandler(this.зарегистрироватьсяToolStripMenuItem_Click);
            // 
            // авторизироватьсяToolStripMenuItem
            // 
            this.авторизироватьсяToolStripMenuItem.Name = "авторизироватьсяToolStripMenuItem";
            this.авторизироватьсяToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.авторизироватьсяToolStripMenuItem.Text = "Авторизироваться";
            this.авторизироватьсяToolStripMenuItem.Click += new System.EventHandler(this.авторизироватьсяToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem1
            // 
            this.выходToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изУчетнойЗаписиToolStripMenuItem,
            this.изПриложенияToolStripMenuItem});
            this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
            this.выходToolStripMenuItem1.Size = new System.Drawing.Size(225, 22);
            this.выходToolStripMenuItem1.Text = "Выход";
            this.выходToolStripMenuItem1.Click += new System.EventHandler(this.выходToolStripMenuItem1_Click);
            // 
            // изУчетнойЗаписиToolStripMenuItem
            // 
            this.изУчетнойЗаписиToolStripMenuItem.Name = "изУчетнойЗаписиToolStripMenuItem";
            this.изУчетнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.изУчетнойЗаписиToolStripMenuItem.Text = "Из учетной записи";
            this.изУчетнойЗаписиToolStripMenuItem.Click += new System.EventHandler(this.изУчетнойЗаписиToolStripMenuItem_Click);
            // 
            // изПриложенияToolStripMenuItem
            // 
            this.изПриложенияToolStripMenuItem.Name = "изПриложенияToolStripMenuItem";
            this.изПриложенияToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.изПриложенияToolStripMenuItem.Text = "Из приложения";
            this.изПриложенияToolStripMenuItem.Click += new System.EventHandler(this.изПриложенияToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.разработчикToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // разработчикToolStripMenuItem
            // 
            this.разработчикToolStripMenuItem.Name = "разработчикToolStripMenuItem";
            this.разработчикToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.разработчикToolStripMenuItem.Text = "Разработчик";
            this.разработчикToolStripMenuItem.Click += new System.EventHandler(this.разработчикToolStripMenuItem_Click);
            // 
            // пользователиToolStripMenuItem
            // 
            this.пользователиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.факультетыToolStripMenuItem,
            this.группыToolStripMenuItem,
            this.читателиToolStripMenuItem});
            this.пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
            this.пользователиToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.пользователиToolStripMenuItem.Text = "Пользователи";
            this.пользователиToolStripMenuItem.Visible = false;
            // 
            // факультетыToolStripMenuItem
            // 
            this.факультетыToolStripMenuItem.Name = "факультетыToolStripMenuItem";
            this.факультетыToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.факультетыToolStripMenuItem.Text = "Факультеты";
            this.факультетыToolStripMenuItem.Click += new System.EventHandler(this.факультетыToolStripMenuItem_Click);
            // 
            // группыToolStripMenuItem
            // 
            this.группыToolStripMenuItem.Name = "группыToolStripMenuItem";
            this.группыToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.группыToolStripMenuItem.Text = "Группы";
            this.группыToolStripMenuItem.Click += new System.EventHandler(this.группыToolStripMenuItem_Click);
            // 
            // читателиToolStripMenuItem
            // 
            this.читателиToolStripMenuItem.Name = "читателиToolStripMenuItem";
            this.читателиToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.читателиToolStripMenuItem.Text = "Читатели";
            this.читателиToolStripMenuItem.Click += new System.EventHandler(this.читателиToolStripMenuItem_Click);
            // 
            // книжныйФондToolStripMenuItem
            // 
            this.книжныйФондToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.просмотрToolStripMenuItem,
            this.поискToolStripMenuItem,
            this.выдатьСдатьКнигиToolStripMenuItem});
            this.книжныйФондToolStripMenuItem.Name = "книжныйФондToolStripMenuItem";
            this.книжныйФондToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.книжныйФондToolStripMenuItem.Text = "Книжный фонд";
            this.книжныйФондToolStripMenuItem.Visible = false;
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // просмотрToolStripMenuItem
            // 
            this.просмотрToolStripMenuItem.Name = "просмотрToolStripMenuItem";
            this.просмотрToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.просмотрToolStripMenuItem.Text = "Просмотр/Изменение";
            this.просмотрToolStripMenuItem.Click += new System.EventHandler(this.просмотрToolStripMenuItem_Click);
            // 
            // поискToolStripMenuItem
            // 
            this.поискToolStripMenuItem.Name = "поискToolStripMenuItem";
            this.поискToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.поискToolStripMenuItem.Text = "Поиск";
            this.поискToolStripMenuItem.Click += new System.EventHandler(this.поискToolStripMenuItem_Click);
            // 
            // выдатьСдатьКнигиToolStripMenuItem
            // 
            this.выдатьСдатьКнигиToolStripMenuItem.Name = "выдатьСдатьКнигиToolStripMenuItem";
            this.выдатьСдатьКнигиToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.выдатьСдатьКнигиToolStripMenuItem.Text = "Выдать/Сдать книги";
            this.выдатьСдатьКнигиToolStripMenuItem.Click += new System.EventHandler(this.выдатьСдатьКнигиToolStripMenuItem_Click);
            // 
            // поискToolStripMenuItem1
            // 
            this.поискToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.поискToolStripMenuItem1.Name = "поискToolStripMenuItem1";
            this.поискToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.поискToolStripMenuItem1.Text = "Поиск";
            this.поискToolStripMenuItem1.Visible = false;
            this.поискToolStripMenuItem1.Click += new System.EventHandler(this.поискToolStripMenuItem1_Click);
            // 
            // администрированиеToolStripMenuItem
            // 
            this.администрированиеToolStripMenuItem.Name = "администрированиеToolStripMenuItem";
            this.администрированиеToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.администрированиеToolStripMenuItem.Text = "Администрирование";
            this.администрированиеToolStripMenuItem.Visible = false;
            this.администрированиеToolStripMenuItem.Click += new System.EventHandler(this.администрированиеToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DBStat,
            this.statText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 374);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(732, 29);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statText
            // 
            this.statText.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statText.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statText.Image = global::LibA.Properties.Resources.error;
            this.statText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statText.Name = "statText";
            this.statText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statText.Size = new System.Drawing.Size(161, 24);
            this.statText.Text = "Вы не вошли в систему";
            // 
            // IsDBAliveTimer
            // 
            this.IsDBAliveTimer.Enabled = true;
            this.IsDBAliveTimer.Interval = 6000;
            this.IsDBAliveTimer.Tick += new System.EventHandler(this.IsDBAliveTimer_Tick);
            // 
            // searchPanel
            // 
            this.searchPanel.AllowDrop = true;
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchPanel.Controls.Add(this.searchInput);
            this.searchPanel.Controls.Add(this.doSearch);
            this.searchPanel.Location = new System.Drawing.Point(9, 27);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(2);
            this.searchPanel.MaximumSize = new System.Drawing.Size(1500, 82);
            this.searchPanel.MinimumSize = new System.Drawing.Size(226, 41);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(226, 41);
            this.searchPanel.TabIndex = 0;
            this.searchPanel.Visible = false;
            // 
            // searchInput
            // 
            this.searchInput.AllowDrop = true;
            this.searchInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.searchInput.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchInput.ForeColor = System.Drawing.SystemColors.GrayText;
            this.searchInput.Location = new System.Drawing.Point(0, 0);
            this.searchInput.Margin = new System.Windows.Forms.Padding(2);
            this.searchInput.MaximumSize = new System.Drawing.Size(1500, 150);
            this.searchInput.MaxLength = 100;
            this.searchInput.MinimumSize = new System.Drawing.Size(76, 20);
            this.searchInput.Name = "searchInput";
            this.searchInput.Size = new System.Drawing.Size(195, 19);
            this.searchInput.TabIndex = 3;
            this.searchInput.Text = "Начните вводить что-нибудь...";
            this.searchInput.Enter += new System.EventHandler(this.searchInput_Enter);
            this.searchInput.Leave += new System.EventHandler(this.searchInput_Leave);
            // 
            // doSearch
            // 
            this.doSearch.BackColor = System.Drawing.Color.Blue;
            this.doSearch.BackgroundImage = global::LibA.Properties.Resources.SearchIcon;
            this.doSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.doSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doSearch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.doSearch.Location = new System.Drawing.Point(199, 2);
            this.doSearch.Margin = new System.Windows.Forms.Padding(2);
            this.doSearch.MaximumSize = new System.Drawing.Size(0, 122);
            this.doSearch.MinimumSize = new System.Drawing.Size(15, 16);
            this.doSearch.Name = "doSearch";
            this.doSearch.Size = new System.Drawing.Size(15, 21);
            this.doSearch.TabIndex = 2;
            this.doSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.doSearch.UseVisualStyleBackColor = false;
            this.doSearch.Click += new System.EventHandler(this.doSearch_Click);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.AllowUserToOrderColumns = true;
            this.dataGridViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.dataGridViewMain.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridViewMain.ColumnHeadersHeight = 29;
            this.dataGridViewMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewMain.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewMain.Location = new System.Drawing.Point(11, 72);
            this.dataGridViewMain.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewMain.MultiSelect = false;
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            this.dataGridViewMain.RowHeadersWidth = 51;
            this.dataGridViewMain.RowTemplate.Height = 24;
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMain.ShowCellErrors = false;
            this.dataGridViewMain.ShowCellToolTips = false;
            this.dataGridViewMain.ShowEditingIcon = false;
            this.dataGridViewMain.ShowRowErrors = false;
            this.dataGridViewMain.Size = new System.Drawing.Size(131, 69);
            this.dataGridViewMain.TabIndex = 9;
            this.dataGridViewMain.Visible = false;
            this.dataGridViewMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMain_CellFormatting);
            this.dataGridViewMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewMain_DataError);
            // 
            // authFormBindingSource
            // 
            this.authFormBindingSource.DataSource = typeof(LibA.AuthForm);
            // 
            // UserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 403);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.searchPanel);
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(266, 45);
            this.Name = "UserPanel";
            this.Text = "Меню библиотеки";
            this.Load += new System.EventHandler(this.UserPanel_SizeChanged);
            this.SizeChanged += new System.EventHandler(this.UserPanel_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem разработчикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem изУчетнойЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изПриложенияToolStripMenuItem;
        private System.Windows.Forms.BindingSource authFormBindingSource;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel statText;
        private System.Windows.Forms.ToolStripMenuItem администрированиеToolStripMenuItem;
        private System.Windows.Forms.Timer IsDBAliveTimer;
        private System.Windows.Forms.ToolStripStatusLabel DBStat;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button doSearch;
        private System.Windows.Forms.TextBox searchInput;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem книжныйФондToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem факультетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem группыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem читателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выдатьСдатьКнигиToolStripMenuItem;
    }
}

