﻿namespace JwtSecret.Generator
{
    partial class Form1
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
            this.generateButton = new System.Windows.Forms.Button();
            this.keySizeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.keyValueTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(64, 38);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(100, 23);
            this.generateButton.TabIndex = 0;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // keySizeTextBox
            // 
            this.keySizeTextBox.Location = new System.Drawing.Point(64, 12);
            this.keySizeTextBox.Name = "keySizeTextBox";
            this.keySizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.keySizeTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Key size";
            // 
            // keyValueTextBox
            // 
            this.keyValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyValueTextBox.BackColor = System.Drawing.Color.White;
            this.keyValueTextBox.Location = new System.Drawing.Point(12, 67);
            this.keyValueTextBox.Name = "keyValueTextBox";
            this.keyValueTextBox.ReadOnly = true;
            this.keyValueTextBox.Size = new System.Drawing.Size(471, 304);
            this.keyValueTextBox.TabIndex = 3;
            this.keyValueTextBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 383);
            this.Controls.Add(this.keyValueTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keySizeTextBox);
            this.Controls.Add(this.generateButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.TextBox keySizeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox keyValueTextBox;
    }
}
