using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullTextSearch.View
{
    public partial class UCCrawlerSettings : UserControl
    {
        public string BaseUrl
        {
            get { return tbBaseURL.Text; }
        }

        public string PageURL
        {
            get { return tbPageURL.Text; }
        }

        public string FromPage
        {
            get { return tbFrom.Text; }
        }

        public string ToPage
        {
            get { return tbTo.Text; }
        }

        public string XpToElements
        {
            get { return tbXpToElements.Text; }
        }

        public string XpToUrl
        {
            get { return tbXpToUrl.Text; }
        }

        public string XpToTitle
        {
            get { return tbXpToTitle.Text; }
        }

        public string XpToText
        {
            get { return tbXpToText.Text; }
        }

        public bool Comments
        {
            get { return cbComments.Checked; }
        }

        public string XpToCommentsUrl
        {
            get { return tbXpToCommentsUrl.Text; }
        }

        public string XpToComment
        {
            get { return tbXpToComment.Text; }
        }

        public string Type
        {
            get { return cbType.Text; }
        }

        public string CrawlerName
        {
            get { return tbName.Text; }
        }


        public UCCrawlerSettings()
        {
            InitializeComponent();
        }

        private void cbComments_CheckedChanged(object sender, EventArgs e)
        {
            gbComments.Visible = cbComments.Checked;
        }
    }
}
