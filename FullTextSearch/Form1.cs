using Nest;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FullTextSearch.Model;
using FullTextSearch.Utils;
using System.IO;

namespace FullTextSearch
{
    public partial class Form1 : Form
    {
        private ElasticSearchInstance ElasticSearchInstance;

        public Form1()
        {
            InitializeComponent();
            IndexInit();
        }

        private void IndexInit()
        {
            ElasticSearchInstance = new ElasticSearchInstance("articles");
            ElasticSearchInstance.FillData();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //webBrowser1.DocumentText = "<b>This text is bold</b>";
            List<Article> articles = ElasticSearchInstance.Search(tbSearchText.Text);
            foreach (Article article in articles)
            {
                dgvHits.Rows.Add(article.Title);
            }
           
        }
    }
}
