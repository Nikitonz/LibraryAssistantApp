namespace LibA
{
    partial class Reports
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
            this.gbox_with2Calendars = new System.Windows.Forms.GroupBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.b_with2Calendars = new System.Windows.Forms.Button();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.gbox_WithTextInput = new System.Windows.Forms.GroupBox();
            this.i_sterm = new System.Windows.Forms.TextBox();
            this.b_sterm = new System.Windows.Forms.Button();
            this.gbox_with2Calendars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.gbox_WithTextInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbox_with2Calendars
            // 
            this.gbox_with2Calendars.Controls.Add(this.dateTimePicker2);
            this.gbox_with2Calendars.Controls.Add(this.label1);
            this.gbox_with2Calendars.Controls.Add(this.label2);
            this.gbox_with2Calendars.Controls.Add(this.dateTimePicker1);
            this.gbox_with2Calendars.Controls.Add(this.b_with2Calendars);
            this.gbox_with2Calendars.Location = new System.Drawing.Point(29, 50);
            this.gbox_with2Calendars.Margin = new System.Windows.Forms.Padding(2);
            this.gbox_with2Calendars.Name = "gbox_with2Calendars";
            this.gbox_with2Calendars.Padding = new System.Windows.Forms.Padding(2);
            this.gbox_with2Calendars.Size = new System.Drawing.Size(536, 36);
            this.gbox_with2Calendars.TabIndex = 6;
            this.gbox_with2Calendars.TabStop = false;
            this.gbox_with2Calendars.Visible = false;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker2.Location = new System.Drawing.Point(228, 9);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(151, 26);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "От";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(194, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "До";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Location = new System.Drawing.Point(36, 9);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 26);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // b_with2Calendars
            // 
            this.b_with2Calendars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_with2Calendars.AutoSize = true;
            this.b_with2Calendars.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.b_with2Calendars.Location = new System.Drawing.Point(479, 13);
            this.b_with2Calendars.Margin = new System.Windows.Forms.Padding(2);
            this.b_with2Calendars.Name = "b_with2Calendars";
            this.b_with2Calendars.Size = new System.Drawing.Size(57, 23);
            this.b_with2Calendars.TabIndex = 4;
            this.b_with2Calendars.Text = "Фильтр";
            this.b_with2Calendars.UseVisualStyleBackColor = true;
            this.b_with2Calendars.Click += new System.EventHandler(this.b_with2Calendars_Click);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.AllowUserToOrderColumns = true;
            this.dataGridViewMain.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMain.ColumnHeadersHeight = 29;
            this.dataGridViewMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewMain.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewMain.Location = new System.Drawing.Point(0, 450);
            this.dataGridViewMain.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewMain.MultiSelect = false;
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            this.dataGridViewMain.RowHeadersVisible = false;
            this.dataGridViewMain.RowHeadersWidth = 51;
            this.dataGridViewMain.RowTemplate.Height = 24;
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMain.ShowCellErrors = false;
            this.dataGridViewMain.ShowCellToolTips = false;
            this.dataGridViewMain.ShowEditingIcon = false;
            this.dataGridViewMain.ShowRowErrors = false;
            this.dataGridViewMain.Size = new System.Drawing.Size(1042, 187);
            this.dataGridViewMain.TabIndex = 5;
            this.dataGridViewMain.Visible = false;
            // 
            // gbox_WithTextInput
            // 
            this.gbox_WithTextInput.Controls.Add(this.i_sterm);
            this.gbox_WithTextInput.Controls.Add(this.b_sterm);
            this.gbox_WithTextInput.Location = new System.Drawing.Point(29, 91);
            this.gbox_WithTextInput.Name = "gbox_WithTextInput";
            this.gbox_WithTextInput.Padding = new System.Windows.Forms.Padding(0);
            this.gbox_WithTextInput.Size = new System.Drawing.Size(592, 42);
            this.gbox_WithTextInput.TabIndex = 7;
            this.gbox_WithTextInput.TabStop = false;
            this.gbox_WithTextInput.Visible = false;
            // 
            // i_sterm
            // 
            this.i_sterm.Dock = System.Windows.Forms.DockStyle.Left;
            this.i_sterm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.i_sterm.Location = new System.Drawing.Point(0, 13);
            this.i_sterm.Margin = new System.Windows.Forms.Padding(0);
            this.i_sterm.Name = "i_sterm";
            this.i_sterm.Size = new System.Drawing.Size(224, 29);
            this.i_sterm.TabIndex = 1;
            this.i_sterm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // b_sterm
            // 
            this.b_sterm.AutoSize = true;
            this.b_sterm.Dock = System.Windows.Forms.DockStyle.Right;
            this.b_sterm.Location = new System.Drawing.Point(517, 13);
            this.b_sterm.Margin = new System.Windows.Forms.Padding(0);
            this.b_sterm.Name = "b_sterm";
            this.b_sterm.Size = new System.Drawing.Size(75, 29);
            this.b_sterm.TabIndex = 0;
            this.b_sterm.Text = "Найти";
            this.b_sterm.UseVisualStyleBackColor = true;
            this.b_sterm.Click += new System.EventHandler(this.b_sterm_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 637);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.gbox_WithTextInput);
            this.Controls.Add(this.gbox_with2Calendars);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Reports";
            this.SizeChanged += new System.EventHandler(this.Reports_SizeChanged);
            this.gbox_with2Calendars.ResumeLayout(false);
            this.gbox_with2Calendars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.gbox_WithTextInput.ResumeLayout(false);
            this.gbox_WithTextInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_with2Calendars;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button b_with2Calendars;
        private System.Windows.Forms.GroupBox gbox_WithTextInput;
        private System.Windows.Forms.Button b_sterm;
        private System.Windows.Forms.TextBox i_sterm;
    }
}