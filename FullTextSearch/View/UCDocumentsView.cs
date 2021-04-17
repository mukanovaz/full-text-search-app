using CrawlerIR2.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FullTextSearch.View
{
    public partial class UCDocumentsView : UserControl
    {
        public List<Article> Articles { get; set; }

        public UCDocumentsView()
        {
            InitializeComponent();
            Articles = new List<Article>();
        }

        public void FillTable(List<Article> articles)
        {
            dgvDocuments.DataSource = articles;
        }
    }
}
