using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Interface;
using FullTextSearch.Model;
using FullTextSearch.Utils;

namespace FullTextSearch.View
{
    public partial class DataSourcePanel : UserControl
    {
        private const string SettingFile = @"settings.txt";
        public List<Article> Articles;

        public DataSourcePanel()
        {
            InitializeComponent();
            FillDataSource();
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
                BasicCrawler basic = new BasicCrawler()
                {
                    Base_url = tbBaseURL.Text,
                    Page_url = tbPageURL.Text,
                    First_page = Int32.Parse(tbFrom.Text),
                    Last_page = Int32.Parse(tbTo.Text),
                    XPathToElements = tbXpToElements.Text,
                    XPathToArticleUrl = tbXpToUrl.Text,
                    XPathToTitle = tbXpToTitle.Text,
                    XPathToText = tbXpToText.Text,
                    IsComments = cbComments.Checked,
                    XPathToCommentsUrl = tbXpToCommentsUrl.Text,
                    XPathToComment = tbXpToComment.Text,
                    Type = cbType.Text,
                    TableName = tbName.Text
                };
                Articles = basic.GetArticles();
                if (Articles != null)
                {
                    AddToFile(tbName.Text);
                }
            } else if (cbCrawler.Text == "Motorkáři.cz")
            {
                List<Article> list = new List<Article>();
                DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html.xml"), ref list);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html64.xml"), ref list);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html128.xml"), ref list);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, @"Data\", @"CrawlerIR_Html192.xml"), ref list);

                Index index = new Index();
                (index as IIndexer).Index(list);
            }
        }

        private void cbComments_CheckedChanged(object sender, EventArgs e)
        {
            gbComments.Visible = cbComments.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlNewCrawler.Visible = cbCrawler.Text == "New";
        }
    }
}
