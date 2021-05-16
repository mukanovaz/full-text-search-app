
namespace FullTextSearch.UI.UserControls
{
    partial class UCSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.cbLogs = new System.Windows.Forms.CheckBox();
            this.cbStemmer = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.panelContainer.Controls.Add(this.groupBox1);
            this.panelContainer.Controls.Add(this.cbLogs);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1036, 455);
            this.panelContainer.TabIndex = 22;
            // 
            // cbLogs
            // 
            this.cbLogs.AutoSize = true;
            this.cbLogs.Checked = true;
            this.cbLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLogs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbLogs.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLogs.Location = new System.Drawing.Point(24, 25);
            this.cbLogs.Name = "cbLogs";
            this.cbLogs.Size = new System.Drawing.Size(86, 18);
            this.cbLogs.TabIndex = 0;
            this.cbLogs.Text = "Show logs";
            this.cbLogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbLogs.UseVisualStyleBackColor = true;
            this.cbLogs.CheckedChanged += new System.EventHandler(this.cbLogs_CheckedChanged);
            // 
            // cbStemmer
            // 
            this.cbStemmer.AutoSize = true;
            this.cbStemmer.Checked = true;
            this.cbStemmer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStemmer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbStemmer.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStemmer.Location = new System.Drawing.Point(6, 19);
            this.cbStemmer.Name = "cbStemmer";
            this.cbStemmer.Size = new System.Drawing.Size(98, 18);
            this.cbStemmer.TabIndex = 1;
            this.cbStemmer.Text = "Use stemmer";
            this.cbStemmer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbStemmer.UseVisualStyleBackColor = true;
            this.cbStemmer.CheckedChanged += new System.EventHandler(this.cbStemmer_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbStemmer);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(24, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preprocessing";
            // 
            // UCSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer);
            this.Name = "UCSettings";
            this.Size = new System.Drawing.Size(1036, 455);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.CheckBox cbLogs;
        private System.Windows.Forms.CheckBox cbStemmer;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
