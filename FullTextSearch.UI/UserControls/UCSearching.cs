using CrawlerIR2.Models;
using FullTextSearch.Core;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Article> articles = new List<Article>();
            
            if (_isBooleanModel)
            {
                articles = MainController.Instance.RunSearcher(_isBooleanModel, tbSearchText.Text);
            } else
            {
                articles = null;
            }

            DocumentsViewUC.FillTable(articles);
        }

        private void cmbSearchModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isBooleanModel = cmbSearchModel.SelectedIndex == 1;
        }
    }
}
