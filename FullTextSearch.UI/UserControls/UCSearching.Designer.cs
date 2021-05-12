
namespace FullTextSearch.UI
{
    partial class UCSearching
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudResults = new System.Windows.Forms.NumericUpDown();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.cmbSearchModel = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lbProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SearchingWorker = new System.ComponentModel.BackgroundWorker();
            this.occurLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResults)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.panel1.Controls.Add(this.nudResults);
            this.panel1.Controls.Add(this.tbSearchText);
            this.panel1.Controls.Add(this.cmbSearchModel);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.occurLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 57);
            this.panel1.TabIndex = 10;
            // 
            // nudResults
            // 
            this.nudResults.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.nudResults.Location = new System.Drawing.Point(437, 21);
            this.nudResults.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudResults.Name = "nudResults";
            this.nudResults.Size = new System.Drawing.Size(51, 20);
            this.nudResults.TabIndex = 12;
            this.nudResults.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudResults.ValueChanged += new System.EventHandler(this.nudResults_ValueChanged);
            // 
            // tbSearchText
            // 
            this.tbSearchText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbSearchText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbSearchText.Location = new System.Drawing.Point(14, 20);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(290, 20);
            this.tbSearchText.TabIndex = 1;
            // 
            // cmbSearchModel
            // 
            this.cmbSearchModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSearchModel.FormattingEnabled = true;
            this.cmbSearchModel.Items.AddRange(new object[] {
            "Vector space model",
            "Boolean model"});
            this.cmbSearchModel.Location = new System.Drawing.Point(310, 20);
            this.cmbSearchModel.Name = "cmbSearchModel";
            this.cmbSearchModel.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchModel.TabIndex = 0;
            this.cmbSearchModel.SelectedIndexChanged += new System.EventHandler(this.cmbSearchModel_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(504, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.pnlLoading);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 57);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1023, 458);
            this.panelContainer.TabIndex = 11;
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.pnlLoading.Controls.Add(this.lbProgress);
            this.pnlLoading.Controls.Add(this.progressBar1);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLoading.Location = new System.Drawing.Point(0, 425);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(1023, 33);
            this.pnlLoading.TabIndex = 2;
            this.pnlLoading.Visible = false;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbProgress.Location = new System.Drawing.Point(121, 9);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(61, 13);
            this.lbProgress.TabIndex = 1;
            this.lbProgress.Text = "Searching..";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // SearchingWorker
            // 
            this.SearchingWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SearchingWorker_DoWork);
            this.SearchingWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SearchingWorker_RunWorkerCompleted);
            // 
            // occurLabel
            // 
            this.occurLabel.AutoSize = true;
            this.occurLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.occurLabel.Location = new System.Drawing.Point(665, 24);
            this.occurLabel.Name = "occurLabel";
            this.occurLabel.Size = new System.Drawing.Size(0, 13);
            this.occurLabel.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(598, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Occurance:";
            // 
            // UCSearching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel1);
            this.Name = "UCSearching";
            this.Size = new System.Drawing.Size(1023, 515);
            this.Load += new System.EventHandler(this.SearchingPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResults)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.ComboBox cmbSearchModel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.NumericUpDown nudResults;
        private System.ComponentModel.BackgroundWorker SearchingWorker;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label occurLabel;
        private System.Windows.Forms.Label label2;
    }
}
