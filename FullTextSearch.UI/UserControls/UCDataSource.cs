using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Core;
using FullTextSearch.Utils;

namespace FullTextSearch.UI
{
    public partial class UCDataSource : UserControl
    {
        private string _dbName;
        static UCCrawlerSettings _ucCrawlerSettings;
        static UCDocumentsView _ucDocumentsView;

        public List<Article> Articles;

        public UCDataSource()
        {
            InitializeComponent();
            FillDataSource();
        }

        public UCCrawlerSettings CrawlerSettingsUC
        {
            get
            {
                if (_ucCrawlerSettings == null)
                {
                    _ucCrawlerSettings = new UCCrawlerSettings();
                    _ucCrawlerSettings.Dock = DockStyle.Fill;
                }
                return _ucCrawlerSettings;
            }
        }

        public UCDocumentsView DocumentsViewUC
        {
            get
            {
                if (_ucDocumentsView == null)
                {
                    _ucDocumentsView = new UCDocumentsView();
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
            PanelContainer.Controls.Add(CrawlerSettingsUC);
        }

        private void FillDataSource()
        {
            string[] lines = DataReader.Instance.GetDataSources();
            foreach (string line in lines)
            {
                cbCrawler.Items.Add(line);
            }
            cbCrawler.Items.Add("New");
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            if (cbCrawler.Text == "New")
            {
                Articles = MainController.Instance.RunCrawler(false, crawler: new BasicCrawler()
                {
                    Base_url = CrawlerSettingsUC.BaseUrl,
                    Page_url = CrawlerSettingsUC.PageURL,
                    First_page = Int32.Parse(CrawlerSettingsUC.FromPage),
                    Last_page = Int32.Parse(CrawlerSettingsUC.ToPage),
                    XPathToElements = CrawlerSettingsUC.XpToElements,
                    XPathToArticleUrl = CrawlerSettingsUC.XpToUrl,
                    XPathToTitle = CrawlerSettingsUC.XpToTitle,
                    XPathToText = CrawlerSettingsUC.XpToText,
                    IsComments = CrawlerSettingsUC.Comments,
                    XPathToCommentsUrl = CrawlerSettingsUC.XpToCommentsUrl,
                    XPathToComment = CrawlerSettingsUC.XpToComment,
                    Type = CrawlerSettingsUC.Type,
                    TableName = DataReader.Instance.GetValidDBName(CrawlerSettingsUC.CrawlerName)
                });

                if (Articles != null)
                {
                    DataReader.Instance.AddDataSourceToFile(CrawlerSettingsUC.CrawlerName);
                }
            } else
            {
                _dbName = DataReader.Instance.GetValidDBName(cbCrawler.Text);
                pnlLoading.Visible = true;
                bwIndexDocuments.RunWorkerAsync();
            }
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
            if (cbCrawler.Text == "New")
            {
                ShowUserControl(CrawlerSettingsUC);
            }
            else 
            {
                ShowUserControl(DocumentsViewUC);
            }
        }

        private void bwIndexDocuments_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            Articles = MainController.Instance.RunCrawler(true, db_name: _dbName, backgroundWorker: backgroundWorker);
        }

        private void bwIndexDocuments_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pnlLoading.Visible = false;
            DocumentsViewUC.FillTable(Articles);
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
