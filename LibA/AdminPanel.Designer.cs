namespace LibA
{
    partial class AdminPanel
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.деавторизоватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаИспользованияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьКнигиУЧитателяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.должникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перевестиГруппыНаСледующийГодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузкаПервокурсниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tables = new System.Windows.Forms.ListBox();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.lPane = new System.Windows.Forms.SplitContainer();
            this.buttonRollback = new System.Windows.Forms.Button();
            this.buttonTransact = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lPane)).BeginInit();
            this.lPane.Panel1.SuspendLayout();
            this.lPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.отчётыToolStripMenuItem,
            this.операцииToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem,
            this.деавторизоватьсяToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 26);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click_1);
            // 
            // деавторизоватьсяToolStripMenuItem
            // 
            this.деавторизоватьсяToolStripMenuItem.Name = "деавторизоватьсяToolStripMenuItem";
            this.деавторизоватьсяToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.деавторизоватьсяToolStripMenuItem.Text = "Деавторизоваться и выйти";
            this.деавторизоватьсяToolStripMenuItem.Click += new System.EventHandler(this.деавторизоватьсяToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.settingsToolStripMenuItem.Text = "Настройки";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // отчётыToolStripMenuItem
            // 
            this.отчётыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.статистикаИспользованияToolStripMenuItem,
            this.проверитьКнигиУЧитателяToolStripMenuItem,
            this.должникиToolStripMenuItem});
            this.отчётыToolStripMenuItem.Name = "отчётыToolStripMenuItem";
            this.отчётыToolStripMenuItem.Size = new System.Drawing.Size(73, 26);
            this.отчётыToolStripMenuItem.Text = "Отчёты";
            // 
            // статистикаИспользованияToolStripMenuItem
            // 
            this.статистикаИспользованияToolStripMenuItem.Name = "статистикаИспользованияToolStripMenuItem";
            this.статистикаИспользованияToolStripMenuItem.Size = new System.Drawing.Size(289, 26);
            this.статистикаИспользованияToolStripMenuItem.Text = "Статистика использования";
            this.статистикаИспользованияToolStripMenuItem.Click += new System.EventHandler(this.статистикаИспользованияToolStripMenuItem_Click);
            // 
            // проверитьКнигиУЧитателяToolStripMenuItem
            // 
            this.проверитьКнигиУЧитателяToolStripMenuItem.Name = "проверитьКнигиУЧитателяToolStripMenuItem";
            this.проверитьКнигиУЧитателяToolStripMenuItem.Size = new System.Drawing.Size(289, 26);
            this.проверитьКнигиУЧитателяToolStripMenuItem.Text = "Проверить книги у читателя";
            this.проверитьКнигиУЧитателяToolStripMenuItem.Click += new System.EventHandler(this.проверитьКнигиУЧитателяToolStripMenuItem_Click);
            // 
            // должникиToolStripMenuItem
            // 
            this.должникиToolStripMenuItem.Name = "должникиToolStripMenuItem";
            this.должникиToolStripMenuItem.Size = new System.Drawing.Size(289, 26);
            this.должникиToolStripMenuItem.Text = "Должники";
            this.должникиToolStripMenuItem.Click += new System.EventHandler(this.должникиToolStripMenuItem_Click);
            // 
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.перевестиГруппыНаСледующийГодToolStripMenuItem,
            this.загрузкаПервокурсниковToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(95, 26);
            this.операцииToolStripMenuItem.Text = "Операции";
            // 
            // перевестиГруппыНаСледующийГодToolStripMenuItem
            // 
            this.перевестиГруппыНаСледующийГодToolStripMenuItem.Name = "перевестиГруппыНаСледующийГодToolStripMenuItem";
            this.перевестиГруппыНаСледующийГодToolStripMenuItem.Size = new System.Drawing.Size(353, 26);
            this.перевестиГруппыНаСледующийГодToolStripMenuItem.Text = "Перевести группы на следующий год";
            this.перевестиГруппыНаСледующийГодToolStripMenuItem.Click += new System.EventHandler(this.перевестиГруппыНаСледующийГодToolStripMenuItem_Click);
            // 
            // загрузкаПервокурсниковToolStripMenuItem
            // 
            this.загрузкаПервокурсниковToolStripMenuItem.Name = "загрузкаПервокурсниковToolStripMenuItem";
            this.загрузкаПервокурсниковToolStripMenuItem.Size = new System.Drawing.Size(353, 26);
            this.загрузкаПервокурсниковToolStripMenuItem.Text = "Загрузка первокурсников";
            this.загрузкаПервокурсниковToolStripMenuItem.Click += new System.EventHandler(this.загрузкаПервокурсниковToolStripMenuItem_Click);
            // 
            // Tables
            // 
            this.Tables.Dock = System.Windows.Forms.DockStyle.Top;
            this.Tables.FormattingEnabled = true;
            this.Tables.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Tables.ItemHeight = 16;
            this.Tables.Location = new System.Drawing.Point(0, 0);
            this.Tables.Margin = new System.Windows.Forms.Padding(0);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(160, 500);
            this.Tables.TabIndex = 13;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToOrderColumns = true;
            this.dataGridViewMain.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMain.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMain.Location = new System.Drawing.Point(200, 28);
            this.dataGridViewMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.RowHeadersWidth = 51;
            this.dataGridViewMain.RowTemplate.Height = 24;
            this.dataGridViewMain.Size = new System.Drawing.Size(600, 573);
            this.dataGridViewMain.TabIndex = 14;
            this.dataGridViewMain.Visible = false;
            this.dataGridViewMain.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMain_CellEndEdit);
            this.dataGridViewMain.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMain_CellMouseDoubleClick);
            // 
            // lPane
            // 
            this.lPane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lPane.Dock = System.Windows.Forms.DockStyle.Left;
            this.lPane.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.lPane.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lPane.IsSplitterFixed = true;
            this.lPane.Location = new System.Drawing.Point(0, 28);
            this.lPane.Margin = new System.Windows.Forms.Padding(0);
            this.lPane.MaximumSize = new System.Drawing.Size(200, 0);
            this.lPane.Name = "lPane";
            // 
            // lPane.Panel1
            // 
            this.lPane.Panel1.Controls.Add(this.buttonRollback);
            this.lPane.Panel1.Controls.Add(this.buttonTransact);
            this.lPane.Panel1.Controls.Add(this.Tables);
            this.lPane.Panel1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.lPane.Panel1MinSize = 0;
            // 
            // lPane.Panel2
            // 
            this.lPane.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.lPane.Panel2.BackgroundImage = global::LibA.Properties.Resources.left;
            this.lPane.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lPane.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lPane.Panel2.Click += new System.EventHandler(this.lPane_Panel2_Click);
            this.lPane.Panel2MinSize = 20;
            this.lPane.Size = new System.Drawing.Size(200, 573);
            this.lPane.SplitterDistance = 162;
            this.lPane.TabIndex = 15;
            // 
            // buttonRollback
            // 
            this.buttonRollback.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRollback.BackColor = System.Drawing.Color.Gray;
            this.buttonRollback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRollback.Enabled = false;
            this.buttonRollback.ForeColor = System.Drawing.SystemColors.InfoText;
            this.buttonRollback.Location = new System.Drawing.Point(-1, 533);
            this.buttonRollback.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRollback.Name = "buttonRollback";
            this.buttonRollback.Size = new System.Drawing.Size(159, 37);
            this.buttonRollback.TabIndex = 17;
            this.buttonRollback.Text = "Отменить изменения";
            this.buttonRollback.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonRollback.UseVisualStyleBackColor = false;
            this.buttonRollback.Visible = false;
            this.buttonRollback.Click += new System.EventHandler(this.buttonRollback_Click);
            // 
            // buttonTransact
            // 
            this.buttonTransact.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonTransact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonTransact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTransact.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonTransact.Location = new System.Drawing.Point(-1, 496);
            this.buttonTransact.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTransact.Name = "buttonTransact";
            this.buttonTransact.Size = new System.Drawing.Size(159, 37);
            this.buttonTransact.TabIndex = 16;
            this.buttonTransact.Text = "Применить изменения";
            this.buttonTransact.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonTransact.UseVisualStyleBackColor = false;
            this.buttonTransact.Click += new System.EventHandler(this.buttonTransact_Click);
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.lPane);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminPanel";
            this.Text = "Admin Panel (authorized-user only)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.lPane.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lPane)).EndInit();
            this.lPane.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem деавторизоватьсяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ListBox Tables;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.SplitContainer lPane;
        private System.Windows.Forms.Button buttonTransact;
        private System.Windows.Forms.Button buttonRollback;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem должникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перевестиГруппыНаСледующийГодToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаИспользованияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проверитьКнигиУЧитателяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузкаПервокурсниковToolStripMenuItem;
    }
}