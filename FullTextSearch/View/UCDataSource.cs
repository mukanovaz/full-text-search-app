using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Interface;
using FullTextSearch.Model;
using FullTextSearch.Utils;

namespace FullTextSearch.View
{
    public partial class UCDataSource : UserControl
    {
        static UCCrawlerSettings _ucCrawlerSettings;
        static UCDocumentsView _ucDocumentsView;

        private string SettingFile = @"D:\Study\ZCU\2.semestr\IR\SemestralWork\kiv-ir-net\FullTextSearch\crawlers.txt";
        public List<Article> Articles;

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

        public UCDataSource()
        {
            InitializeComponent();
            FillDataSource();
        }

        private void UCDataSource_Load(object sender, EventArgs e)
        {
            PanelContainer.Controls.Add(DocumentsViewUC);
            PanelContainer.Controls.Add(CrawlerSettingsUC);
        }

        // TODO: move to model
        private void FillDataSource()
        {
            string[] lines = File.ReadAllLines(SettingFile);
            foreach (string line in lines)
            {
                cbCrawler.Items.Add(line);
            }
            cbCrawler.Items.Add("New");
        }

        private void AddToFile(string table_name)
        {
            File.AppendAllLines(SettingFile, new string[] { table_name });
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            if (cbCrawler.Text == "New")
            {
                Articles = Controller.Instance.RunCrawler(new BasicCrawler()
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
                    TableName = Controller.Instance.GetValidDBName(CrawlerSettingsUC.CrawlerName)
                });

                if (Articles != null)
                {
                    AddToFile(CrawlerSettingsUC.CrawlerName);
                }
            } else
            {
                Articles = Controller.Instance.GetDataFromFilesAndIndex(Controller.Instance.GetValidDBName(cbCrawler.Text));
                DocumentsViewUC.FillTable(Articles);
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
    }
}
