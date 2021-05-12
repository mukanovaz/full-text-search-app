using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;

namespace FullTextSearch.UI.UserControls
{
    public partial class UCcrudDocuments : UserControl
    {
        private string _lastArticleId;
        private BindingSource _bindingSource = new BindingSource();

        public UCcrudDocuments()
        {
            InitializeComponent();
        }

        internal void FillTable(List<Article> articles)
        {
            if (articles == null)
            {
                return;
            }
            _bindingSource.DataSource = articles;
            dgvDocuments.DataSource = _bindingSource;
        }

        private void dgvDocuments_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _lastArticleId = dgvDocuments.Rows[e.RowIndex].Cells[0].Value.ToString();
                tbTitle.Text = dgvDocuments.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbCategory.Text = dgvDocuments.Rows[e.RowIndex].Cells[4].Value.ToString();
                tbAuthor.Text = dgvDocuments.Rows[e.RowIndex].Cells[5].Value.ToString();
                try
                {

                    string date = dgvDocuments.Rows[e.RowIndex].Cells[2].Value.ToString();
                    dtpDate.Value = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                catch { }
                tbArticles.Text = ExtractText(dgvDocuments.Rows[e.RowIndex].Cells[7].Value.ToString());
                tbViews.Text = dgvDocuments.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch 
            { }
            
        }

        private string ExtractText(string text)
        {
            int index = text.IndexOf("<blockquote>", 0, text.Length);
            return index == -1 ? text : text.Remove(0, index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newText = new CrawlerMotorkari().BuildText(tbTitle.Text, tbCategory.Text, tbAuthor.Text, dtpDate.Value.Date.ToString("dd.MM.yyyy"), tbViews.Text, tbArticles.Text);

            // Update article in database
            MainController.Instance.RunDatabaseUtility(
                    2,
                    Int32.Parse(_lastArticleId),
                    tbTitle.Text,
                    tbCategory.Text,
                    tbAuthor.Text,
                    dtpDate.Value.Date,
                    newText,
                    tbViews.Text
                );
            // Update index
            dgvDocuments.Enabled = false;
            pnlLoading.Visible = true;
            lbProgress.Text = "Saving..";
            bwUpdateArticle.RunWorkerAsync();
        }

        private void tbViews_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void bwUpdateArticle_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Update index
            MainController.Instance.RunIndexer(2, Int32.Parse(_lastArticleId));
        }

        private void bwUpdateArticle_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pnlLoading.Visible = false;
            dgvDocuments.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Update in database
            MainController.Instance.RunDatabaseUtility(3, Int32.Parse(_lastArticleId));

            // Update in index
            MainController.Instance.RunIndexer(3, Int32.Parse(_lastArticleId));

            // Remove from binding source
            _bindingSource.RemoveCurrent();
        }

        private bool _isNew = false;
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!_isNew)
            {
                tbTitle.Text = string.Empty;
                tbCategory.Text = string.Empty;
                tbAuthor.Text = string.Empty;
                dtpDate.Value = DateTime.Today;
                tbArticles.Text = string.Empty;
                tbViews.Text = string.Empty;

                btnSave.Enabled = false;
                btnDelete.Enabled = false;

                _isNew = true;
            } else
            {
                // Add to database
                Article newArticle = MainController.Instance.RunDatabaseUtility(0, Int32.Parse(_lastArticleId),
                            tbTitle.Text,
                            tbCategory.Text,
                            tbAuthor.Text,
                            dtpDate.Value.Date,
                            tbArticles.Text,
                            tbViews.Text
                    );

                // Add to index
                MainController.Instance.RunIndexer(0, Int32.Parse(_lastArticleId));

                // Update binding list
                _bindingSource.Add(newArticle);

                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                _isNew = false; 
            }
          
        }
    }
}
