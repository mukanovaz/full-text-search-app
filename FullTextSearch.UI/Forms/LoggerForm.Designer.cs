
namespace FullTextSearch.UI.Forms
{
    partial class LoggerForm
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
            this.loggerTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // loggerTextBox
            // 
            this.loggerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loggerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerTextBox.Enabled = false;
            this.loggerTextBox.Location = new System.Drawing.Point(0, 0);
            this.loggerTextBox.Name = "loggerTextBox";
            this.loggerTextBox.Size = new System.Drawing.Size(800, 450);
            this.loggerTextBox.TabIndex = 0;
            this.loggerTextBox.Text = "";
            this.loggerTextBox.TextChanged += new System.EventHandler(this.loggerTextBox_TextChanged);
            // 
            // LoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.loggerTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoggerForm";
            this.Text = "Logger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox loggerTextBox;
    }
}