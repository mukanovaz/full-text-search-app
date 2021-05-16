
namespace FullTextSearch.UI.UserControls
{
    partial class UCcrudDocuments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvDocuments = new System.Windows.Forms.DataGridView();
            this.ArticleTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbViews = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbCategory = new System.Windows.Forms.TextBox();
            this.tbArticles = new System.Windows.Forms.RichTextBox();
            this.bwUpdateArticle = new System.ComponentModel.BackgroundWorker();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lbProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bwUpdateTable = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(170)))), ((int)(((byte)(168)))));
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 45);
            this.panel1.TabIndex = 21;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDelete.Location = new System.Drawing.Point(178, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNew.Location = new System.Drawing.Point(97, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Location = new System.Drawing.Point(16, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvDocuments
            // 
            this.dgvDocuments.AllowUserToResizeColumns = false;
            this.dgvDocuments.AllowUserToResizeRows = false;
            this.dgvDocuments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.dgvDocuments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDocuments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDocuments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArticleTitle});
            this.dgvDocuments.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvDocuments.DataMember = "Article";
            this.dgvDocuments.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvDocuments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.dgvDocuments.Location = new System.Drawing.Point(0, 45);
            this.dgvDocuments.MultiSelect = false;
            this.dgvDocuments.Name = "dgvDocuments";
            this.dgvDocuments.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvDocuments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDocuments.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvDocuments.RowHeadersVisible = false;
            this.dgvDocuments.Size = new System.Drawing.Size(400, 377);
            this.dgvDocuments.TabIndex = 22;
            this.dgvDocuments.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocuments_CellEnter);
            // 
            // ArticleTitle
            // 
            this.ArticleTitle.DataPropertyName = "Title";
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            this.ArticleTitle.DefaultCellStyle = dataGridViewCellStyle11;
            this.ArticleTitle.HeaderText = "Article title";
            this.ArticleTitle.Name = "ArticleTitle";
            this.ArticleTitle.ReadOnly = true;
            this.ArticleTitle.Width = 400;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbViews);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbAuthor);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbTitle);
            this.panel2.Controls.Add(this.tbCategory);
            this.panel2.Controls.Add(this.tbArticles);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(400, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(636, 377);
            this.panel2.TabIndex = 23;
            // 
            // tbViews
            // 
            this.tbViews.Location = new System.Drawing.Point(84, 94);
            this.tbViews.Name = "tbViews";
            this.tbViews.Size = new System.Drawing.Size(522, 20);
            this.tbViews.TabIndex = 10;
            this.tbViews.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbViews_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Views:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd.MM.yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(84, 122);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDate.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Date:";
            // 
            // tbAuthor
            // 
            this.tbAuthor.Location = new System.Drawing.Point(84, 68);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(522, 20);
            this.tbAuthor.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Author:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Category:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title:";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(84, 10);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(522, 20);
            this.tbTitle.TabIndex = 2;
            // 
            // tbCategory
            // 
            this.tbCategory.Location = new System.Drawing.Point(84, 39);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(522, 20);
            this.tbCategory.TabIndex = 1;
            // 
            // tbArticles
            // 
            this.tbArticles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbArticles.Location = new System.Drawing.Point(20, 150);
            this.tbArticles.Name = "tbArticles";
            this.tbArticles.Size = new System.Drawing.Size(586, 249);
            this.tbArticles.TabIndex = 0;
            this.tbArticles.Text = "";
            // 
            // bwUpdateArticle
            // 
            this.bwUpdateArticle.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpdateArticle_DoWork);
            this.bwUpdateArticle.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUpdateArticle_RunWorkerCompleted);
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.pnlLoading.Controls.Add(this.lbProgress);
            this.pnlLoading.Controls.Add(this.progressBar1);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLoading.Location = new System.Drawing.Point(0, 422);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(1036, 33);
            this.pnlLoading.TabIndex = 24;
            this.pnlLoading.Visible = false;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbProgress.Location = new System.Drawing.Point(121, 9);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(46, 13);
            this.lbProgress.TabIndex = 1;
            this.lbProgress.Text = "Saving..";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // UCcrudDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvDocuments);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLoading);
            this.Name = "UCcrudDocuments";
            this.Size = new System.Drawing.Size(1036, 455);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDocuments;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArticleTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox tbArticles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbViews;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker bwUpdateArticle;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.ComponentModel.BackgroundWorker bwUpdateTable;
    }
}
