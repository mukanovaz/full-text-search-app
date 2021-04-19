using CrawlerIR2.Interface;
using CrawlerIR2.Models;
using FullTextSearch.Model;
using FullTextSearch.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FullTextSearch.View
{
    public partial class UCSearching : UserControl
    {
        static UCDocumentsView _ucDocumentsView;

        public Panel PanelContainer
        {
            get { return panelContainer; }
            set { panelContainer = value; }
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

        public UCSearching()
        {
            InitializeComponent();
        }

        private void SearchingPanel_Load(object sender, EventArgs e)
        {
            PanelContainer.Controls.Add(DocumentsViewUC);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Article> articles = new List<Article>();
            List<IResult> results = Controller.Instance.Index.Search(tbSearchText.Text);

            for (int i = 0; i < results.Count; i++)
            {
                if (articles.Count == nudResults.Value) break;
                Result res = (Result) results[i];
                string id = res.GetDocumentID();
                using (var context = new Context(Controller.Instance.TableName))
                {
                    Article article = context.Articles
                                       .Where(s => s.ArticleId.ToString() == id)
                                       .FirstOrDefault<Article>();

                    Article obj = articles.FirstOrDefault(x => x.ArticleId == article.ArticleId);
                    if (obj == null)
                    {
                        article.Text = DataReader.Instance.AddHighlightToText(article.Text, res.StartPosition, res.EndPositionPosition);
                        articles.Add(article);
                    } 
                }
            }
            DocumentsViewUC.FillTable(articles);
        }
    }
}
