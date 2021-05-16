
namespace FullTextSearch.UI
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
            this.cbCrawler = new System.Windows.Forms.ComboBox();
            this.pnlDataSource = new System.Windows.Forms.Panel();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lbProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bwIndexDocuments = new System.ComponentModel.BackgroundWorker();
            this.bwIndexCzechDocuments = new System.ComponentModel.BackgroundWorker();
            this.pnlDataSource.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            this.btnGo.FlatAppearance.BorderSize = 0;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGo.Location = new System.Drawing.Point(139, 15);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(96, 23);
            this.btnGo.TabIndex = 16;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // cbCrawler
            // 
            this.cbCrawler.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCrawler.FormattingEnabled = true;
            this.cbCrawler.Items.AddRange(new object[] {
            "Motorkáři.cz",
            "Czech data"});
            this.cbCrawler.Location = new System.Drawing.Point(12, 16);
            this.cbCrawler.Name = "cbCrawler";
            this.cbCrawler.Size = new System.Drawing.Size(121, 21);
            this.cbCrawler.TabIndex = 18;
            this.cbCrawler.SelectedIndexChanged += new System.EventHandler(this.cbCrawler_SelectedIndexChanged);
            // 
            // pnlDataSource
            // 
            this.pnlDataSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.pnlDataSource.Controls.Add(this.btnGo);
            this.pnlDataSource.Controls.Add(this.cbCrawler);
            this.pnlDataSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataSource.Location = new System.Drawing.Point(0, 0);
            this.pnlDataSource.Name = "pnlDataSource";
            this.pnlDataSource.Size = new System.Drawing.Size(1036, 51);
            this.pnlDataSource.TabIndex = 20;
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.pnlLoading);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 51);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1036, 404);
            this.panelContainer.TabIndex = 21;
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.pnlLoading.Controls.Add(this.lbProgress);
            this.pnlLoading.Controls.Add(this.progressBar1);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLoading.Location = new System.Drawing.Point(0, 371);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(1036, 33);
            this.pnlLoading.TabIndex = 1;
            this.pnlLoading.Visible = false;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbProgress.Location = new System.Drawing.Point(121, 9);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(91, 13);
            this.lbProgress.TabIndex = 1;
            this.lbProgress.Text = "Reading data..";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // bwIndexDocuments
            // 
            this.bwIndexDocuments.WorkerReportsProgress = true;
            this.bwIndexDocuments.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwIndexDocuments_DoWork);
            this.bwIndexDocuments.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwIndexDocuments_ProgressChanged);
            this.bwIndexDocuments.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwIndexDocuments_RunWorkerCompleted);
            // 
            // bwIndexCzechDocuments
            // 
            this.bwIndexCzechDocuments.WorkerReportsProgress = true;
            // 
            // UCDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.pnlDataSource);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UCDataSource";
            this.Size = new System.Drawing.Size(1036, 455);
            this.Load += new System.EventHandler(this.UCDataSource_Load);
            this.pnlDataSource.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cbCrawler;
        private System.Windows.Forms.Panel pnlDataSource;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bwIndexDocuments;
        private System.Windows.Forms.Label lbProgress;
        private System.ComponentModel.BackgroundWorker bwIndexCzechDocuments;
    }
}
