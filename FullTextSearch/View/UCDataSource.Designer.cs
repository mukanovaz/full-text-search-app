﻿
namespace FullTextSearch.View
{
    partial class UCDataSource
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
            this.btnGo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCrawler = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            this.btnGo.FlatAppearance.BorderSize = 0;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGo.Location = new System.Drawing.Point(272, 18);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(96, 23);
            this.btnGo.TabIndex = 16;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 17;
            this.label6.Text = "Select crawler:";
            // 
            // cbCrawler
            // 
            this.cbCrawler.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCrawler.FormattingEnabled = true;
            this.cbCrawler.Location = new System.Drawing.Point(145, 19);
            this.cbCrawler.Name = "cbCrawler";
            this.cbCrawler.Size = new System.Drawing.Size(121, 21);
            this.cbCrawler.TabIndex = 18;
            this.cbCrawler.SelectedIndexChanged += new System.EventHandler(this.cbCrawler_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Controls.Add(this.cbCrawler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 51);
            this.panel1.TabIndex = 20;
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 51);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1036, 404);
            this.panelContainer.TabIndex = 21;
            // 
            // UCDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UCDataSource";
            this.Size = new System.Drawing.Size(1036, 455);
            this.Load += new System.EventHandler(this.UCDataSource_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCrawler;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelContainer;
    }
}
