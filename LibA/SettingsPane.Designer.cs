namespace LibA
{
    partial class SettingsPane
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.servName = new System.Windows.Forms.TextBox();
            this.saveState = new System.Windows.Forms.Button();
            this.port = new System.Windows.Forms.MaskedTextBox();
            this.dataSource = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя сервера или IPv4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя сервера";
            this.label2.Visible = false;
            // 
            // servName
            // 
            this.servName.Location = new System.Drawing.Point(164, 41);
            this.servName.Name = "servName";
            this.servName.Size = new System.Drawing.Size(173, 22);
            this.servName.TabIndex = 3;
            this.servName.Visible = false;
            // 
            // saveState
            // 
            this.saveState.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.saveState.Location = new System.Drawing.Point(244, 126);
            this.saveState.Name = "saveState";
            this.saveState.Size = new System.Drawing.Size(93, 28);
            this.saveState.TabIndex = 4;
            this.saveState.Text = "Сохранить";
            this.saveState.UseVisualStyleBackColor = false;
            this.saveState.Click += new System.EventHandler(this.saveState_Click);
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(292, 13);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(45, 22);
            this.port.TabIndex = 6;
            // 
            // dataSource
            // 
            this.dataSource.Location = new System.Drawing.Point(164, 13);
            this.dataSource.Name = "dataSource";
            this.dataSource.Size = new System.Drawing.Size(122, 22);
            this.dataSource.TabIndex = 7;
            // 
            // SettingsPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(339, 155);
            this.Controls.Add(this.dataSource);
            this.Controls.Add(this.port);
            this.Controls.Add(this.saveState);
            this.Controls.Add(this.servName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsPane";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox servName;
        private System.Windows.Forms.Button saveState;
        private System.Windows.Forms.MaskedTextBox port;
        private System.Windows.Forms.TextBox dataSource;
    }
}