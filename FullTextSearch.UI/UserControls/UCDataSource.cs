using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Core;
using FullTextSearch.UI.UserControls;
using FullTextSearch.Utils;

namespace FullTextSearch.UI
{
    public partial class UCDataSource : UserControl
    {
        private string _dbName;
        static UCcrudDocuments _ucDocumentsView;
        private Form1 _form;

        public List<Article> Articles { get; private set; }

        public UCDataSource(Form1 form1)
        {
            InitializeComponent();
            _form = form1;
        }

        public UCcrudDocuments DocumentsViewUC
        {
            get
            {
                if (_ucDocumentsView == null)
                {
                    _ucDocumentsView = new UCcrudDocuments();
                    _ucDocumentsView.Dock = DockStyle.Fill;
                }
                return _ucDocumentsView;
            }
        }

        public Panel PanelContainer
        {
            get { return panelContainer; }
            set { panelContainer = value; }
        }

        public BackgroundWorker BWIndexDocuments
        {
            get { return bwIndexDocuments; }
            set { bwIndexDocuments = value; }
        }

        public ProgressBar ProgressBar
        {
            get { return progressBar1; }
            set { progressBar1 = value; }
        }

        private void UCDataSource_Load(object sender, EventArgs e)
        {
            PanelContainer.Controls.Add(DocumentsViewUC);
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            // Generate db name 
            _dbName = DataReader.Instance.GetValidDBName(cbCrawler.Text);
            // Change controls
            pnlLoading.Visible = true;
            _form.LockButtonsBeforeDataSource(false);
            pnlDataSource.Enabled = false;
            _ucDocumentsView.Enabled = false;
            // Run task
            bwIndexDocuments.RunWorkerAsync();
        }

        private void ShowUserControl(UserControl userControl)
        {
            for (int i = 0; i < PanelContainer.Controls.Count; i++)
            {
                if (PanelContainer.Controls[i].Name == userControl.GetType().Name)
                {
                    PanelContainer.Controls[userControl.GetType().Name].Visible = true;
                    PanelContainer.Controls[userControl.GetType().Name].BringToFront();
                    break;
                }
            }

        }

        private void cbCrawler_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowUserControl(DocumentsViewUC);
        }

        private void bwIndexDocuments_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            Articles = MainController.Instance.RunCrawler(true, _dbName, db_name: _dbName, backgroundWorker: backgroundWorker);
        }

        private void bwIndexDocuments_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pnlLoading.Visible = false;
            DocumentsViewUC.FillTable(Articles);
            _form.LockButtonsBeforeDataSource(true);
            pnlDataSource.Enabled = true;
            _ucDocumentsView.Enabled = true;
        }

        private void bwIndexDocuments_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                lbProgress.Text = "Reading data..";
            } else
            {
                lbProgress.Text = "Indexing data..";
            }
        }

    }
}
