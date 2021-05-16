using FullTextSearch.Core;
using System;
using System.Windows.Forms;

namespace FullTextSearch.UI.UserControls
{
    public partial class UCSettings : UserControl
    {
        private Form1 _form1;

        public UCSettings(Form1 form)
        {
            InitializeComponent();
            _form1 = form;
        }

        private void cbLogs_CheckedChanged(object sender, EventArgs e)
        {
            _form1.MakeLogsVisible(cbLogs.Checked);
        }

        private void cbStemmer_CheckedChanged(object sender, EventArgs e)
        {
            MainController.Instance.SetStemmerSetting(cbStemmer.Checked);
        }
    }
}
