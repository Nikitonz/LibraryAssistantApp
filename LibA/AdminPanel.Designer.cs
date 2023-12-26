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
            this.деавторизоватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tables = new System.Windows.Forms.ListBox();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.lPane = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.выходToolStripMenuItem});
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
            this.деавторизоватьсяToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // деавторизоватьсяToolStripMenuItem
            // 
            this.деавторизоватьсяToolStripMenuItem.Name = "деавторизоватьсяToolStripMenuItem";
            this.деавторизоватьсяToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.деавторизоватьсяToolStripMenuItem.Text = "Деавторизоваться";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.settingsToolStripMenuItem.Text = "Настройки";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // Tables
            // 
            this.Tables.Dock = System.Windows.Forms.DockStyle.Top;
            this.Tables.FormattingEnabled = true;
            this.Tables.ItemHeight = 16;
            this.Tables.Location = new System.Drawing.Point(0, 0);
            this.Tables.Margin = new System.Windows.Forms.Padding(0);
            this.Tables.MultiColumn = true;
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(158, 484);
            this.Tables.TabIndex = 13;
            this.Tables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Location = new System.Drawing.Point(243, 28);
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.RowHeadersWidth = 51;
            this.dataGridViewMain.RowTemplate.Height = 24;
            this.dataGridViewMain.Size = new System.Drawing.Size(128, 77);
            this.dataGridViewMain.TabIndex = 14;
            this.dataGridViewMain.Visible = false;
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
            this.lPane.Panel1.Controls.Add(this.button2);
            this.lPane.Panel1.Controls.Add(this.button1);
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
            this.lPane.Size = new System.Drawing.Size(200, 572);
            this.lPane.SplitterDistance = 160;
            this.lPane.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button2.Location = new System.Drawing.Point(-1, 531);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 37);
            this.button2.TabIndex = 17;
            this.button2.Text = "Отменить изменения";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.UseWaitCursor = true;
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(-1, 494);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 37);
            this.button1.TabIndex = 16;
            this.button1.Text = "Применить изменения";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.UseWaitCursor = true;
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
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
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.SplitContainer lPane;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}