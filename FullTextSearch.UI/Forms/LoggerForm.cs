using System.Windows.Forms;

namespace FullTextSearch.UI.Forms
{
    public partial class LoggerForm : Form
    {
        public RichTextBox LoggerTextBox
        {
            get { return loggerTextBox; }
            set { loggerTextBox = value; }
        }

        public LoggerForm()
        {
            InitializeComponent();
        }

        private void loggerTextBox_TextChanged(object sender, System.EventArgs e)
        {
            loggerTextBox.Select(loggerTextBox.Text.Length - 1, 0);
            loggerTextBox.ScrollToCaret();
        }
    }
}
