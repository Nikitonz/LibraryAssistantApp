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
            this.dataSource = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.saveState = new System.Windows.Forms.Button();
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
            this.label1.Size = new System.Drawing.Size(159, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "База данных(источник)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = ">?";
            // 
            // dataSource
            // 
            this.dataSource.Location = new System.Drawing.Point(178, 13);
            this.dataSource.Name = "dataSource";
            this.dataSource.Size = new System.Drawing.Size(231, 22);
            this.dataSource.TabIndex = 2;
         
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(178, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(231, 22);
            this.textBox2.TabIndex = 3;
            // 
            // saveState
            // 
            this.saveState.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.saveState.Location = new System.Drawing.Point(326, 124);
            this.saveState.Name = "saveState";
            this.saveState.Size = new System.Drawing.Size(93, 28);
            this.saveState.TabIndex = 4;
            this.saveState.Text = "Сохранить";
            this.saveState.UseVisualStyleBackColor = false;
            this.saveState.Click += new System.EventHandler(this.saveState_Click);
            // 
            // SettingsPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(420, 155);
            this.Controls.Add(this.saveState);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.dataSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsPane";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dataSource;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button saveState;
    }
}