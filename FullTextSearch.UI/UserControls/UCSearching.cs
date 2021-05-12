using CrawlerIR2.Models;
using FullTextSearch.Core;
using FullTextSearch.Indexer;
using FullTextSearch.Indexer.Indexer.Models;
using FullTextSearch.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FullTextSearch.UI
{
    public partial class UCSearching : UserControl
    {
        private bool _isBooleanModel = true;
        static UCDocumentsView _ucDocumentsView;
        private bool _isLoaded = false;
        private List<Article> _articles;

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
            cmbSearchModel.SelectedIndex = 1;
        }

        private void SearchingPanel_Load(object sender, EventArgs e)
        {
            PanelContainer.Controls.Add(DocumentsViewUC);
        }

        public void AutoComplete()
        {
            if (MainController.Instance.GetIndex() == null) return;
            if (_isLoaded) return;
            var terms = MainController.Instance.GetIndex().InvertedIndex;
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            foreach (var entry in terms)
            {
                autoCompleteStringCollection.Add(entry.Key);
            }
            tbSearchText.AutoCompleteCustomSource = autoCompleteStringCollection;
            _isLoaded = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlLoading.Visible = true;
            SearchingWorker.RunWorkerAsync();
        }

        private void cmbSearchModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isBooleanModel = cmbSearchModel.SelectedIndex == 1;
        }

        private void SearchingWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _articles = MainController.Instance.RunSearcher(_isBooleanModel, tbSearchText.Text);
        }

        private void SearchingWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pnlLoading.Visible = false;

            if (_articles == null)
            {
                return;
            }

            if (_articles.Count < Convert.ToInt32(nudResults.Value))
            {
                DocumentsViewUC.FillTable(_articles);
            } else
            {
                DocumentsViewUC.FillTable(_articles.GetRange(0, Convert.ToInt32(nudResults.Value)));
            }
           
            occurLabel.Text = _articles.Count.ToString();
        }

        private void nudResults_ValueChanged(object sender, EventArgs e)
        {
            _articles = MainController.Instance.GetResults();
            if (_articles != null)
                DocumentsViewUC.FillTable(_articles.GetRange(0, Convert.ToInt32(nudResults.Value)));
        }
    }
}
