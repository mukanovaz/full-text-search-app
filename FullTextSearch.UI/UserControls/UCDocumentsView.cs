using CrawlerIR2.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FullTextSearch.UI
{
    public partial class UCDocumentsView : UserControl
    {
        public UCDocumentsView()
        {
            InitializeComponent();
            webBrowser.ScriptErrorsSuppressed = true;
        }

        public void FillTable(List<Article> articles)
        {
            dgvDocuments.DataSource = articles;
        }

        private void dgvDocuments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDocuments.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvDocuments.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvDocuments.Rows[selectedrowindex];
                string text = Convert.ToString(selectedRow.Cells["Text"].Value);
                if (text == null) return;
                webBrowser.DocumentText = text;

                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlElement element = webBrowser.Document.GetElementById("search");
            if (element != null) element.Focus();
        }
    }
}
