
namespace FullTextSearch
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
            this.cmbSearchModel = new System.Windows.Forms.ComboBox();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToExistingIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvHits = new System.Windows.Forms.DataGridView();
            this.ArticleTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHits)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSearchModel
            // 
            this.cmbSearchModel.FormattingEnabled = true;
            this.cmbSearchModel.Items.AddRange(new object[] {
            "Vector space model",
            "Boolean model"});
            this.cmbSearchModel.Location = new System.Drawing.Point(308, 37);
            this.cmbSearchModel.Name = "cmbSearchModel";
            this.cmbSearchModel.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchModel.TabIndex = 0;
            // 
            // tbSearchText
            // 
            this.tbSearchText.Location = new System.Drawing.Point(12, 37);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(290, 20);
            this.tbSearchText.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Location = new System.Drawing.Point(435, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Occurance:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "7";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(465, 99);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(498, 362);
            this.webBrowser1.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(973, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewIndexToolStripMenuItem,
            this.addToExistingIndexToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem1.Text = "Index";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // addNewIndexToolStripMenuItem
            // 
            this.addNewIndexToolStripMenuItem.Name = "addNewIndexToolStripMenuItem";
            this.addNewIndexToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addNewIndexToolStripMenuItem.Text = "Add new index";
            // 
            // addToExistingIndexToolStripMenuItem
            // 
            this.addToExistingIndexToolStripMenuItem.Name = "addToExistingIndexToolStripMenuItem";
            this.addToExistingIndexToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addToExistingIndexToolStripMenuItem.Text = "Add to existing index";
            // 
            // dgvHits
            // 
            this.dgvHits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArticleTitle});
            this.dgvHits.Location = new System.Drawing.Point(12, 99);
            this.dgvHits.Name = "dgvHits";
            this.dgvHits.Size = new System.Drawing.Size(447, 362);
            this.dgvHits.TabIndex = 8;
            // 
            // ArticleTitle
            // 
            this.ArticleTitle.HeaderText = "Article title";
            this.ArticleTitle.Name = "ArticleTitle";
            this.ArticleTitle.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 470);
            this.Controls.Add(this.dgvHits);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSearchText);
            this.Controls.Add(this.cmbSearchModel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSearchModel;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addNewIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToExistingIndexToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArticleTitle;
    }
}

