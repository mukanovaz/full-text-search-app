using System;
using System.Windows.Forms;
using System.Drawing;
using FullTextSearch.UI;
using FullTextSearch.SimpleLogger;
using FullTextSearch.UI.Forms;
using FullTextSearch.UI.UserControls;

namespace FullTextSearch
{
    public partial class Form1 : Form
    {

        private static Form1 _obj;
        private static UCDataSource _ucDataSource;
        private static UCSearching _ucSearching;
        private static UCcrudDocuments _uccrudDocuments;
        private readonly Color _firstColor = Color.FromArgb(247, 243, 233);  // Main buttons
        private readonly Color _secondColor = Color.FromArgb(94, 170, 168);  // Other buttons

        public static Form1 Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new Form1();
                }
                return _obj;
            }
        }

        public Form1()
        {
            InitializeComponent();

            // Init logger form
            LoggerForm loggerForm = new LoggerForm();
            loggerForm.Show();
            // Redirect console output to TextBox
            Console.SetOut(new ControlWriter(loggerForm.LoggerTextBox));
        }

        public Panel PanelContainer
        {
            get { return panelContainer; }
            set { panelContainer = value; }
        }

        public UCcrudDocuments CRUDDocuments
        {
            get
            {
                if (_uccrudDocuments == null)
                {
                    _uccrudDocuments = new UCcrudDocuments();
                    _uccrudDocuments.Dock = DockStyle.Fill;
                }
                return _uccrudDocuments;
            }
        }

        public UCDataSource DataSourceUC
        {
            get 
            {
                if (_ucDataSource == null)
                {
                    _ucDataSource = new UCDataSource();
                    _ucDataSource.Dock = DockStyle.Fill;
                }
                return _ucDataSource;
            }
        }

        public UCSearching SearchingUC
        {
            get
            {
                if (_ucSearching == null)
                {
                    _ucSearching = new UCSearching();
                    _ucSearching.Dock = DockStyle.Fill;
                }
                return _ucSearching;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _obj = this;

            PanelContainer.Controls.Add(DataSourceUC);
            PanelContainer.Controls.Add(SearchingUC);
            PanelContainer.Controls.Add(CRUDDocuments);

            ShowUserControl(DataSourceUC);
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            // Set red nav
            pnlNav.Height = btnSearch.Height;
            pnlNav.Top = btnSearch.Top;
            // Set main button
            btnSearch.BackColor = _firstColor;
            btnSearch.ForeColor = Color.Black;
            // Set other buttons
            btnDataSource.BackColor = _secondColor;
            btnDocuments.BackColor = _secondColor;
            btnSettings.BackColor = _secondColor;
            btnDataSource.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;
            btnSettings.ForeColor = Color.White;

            ShowUserControl(SearchingUC);

            _ucSearching.AutoComplete();
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            // Set red nav
            pnlNav.Height = btnDocuments.Height;
            pnlNav.Top = btnDocuments.Top;
            // Set main button
            btnDocuments.BackColor = _firstColor;
            btnDocuments.ForeColor = Color.Black;
            // Set other buttons
            btnSettings.BackColor = _secondColor;
            btnSearch.BackColor = _secondColor;
            btnDataSource.BackColor = _secondColor;
            btnSettings.ForeColor = Color.White;
            btnSearch.ForeColor = Color.White;
            btnDataSource.ForeColor = Color.White;

            // Fill datasource
            ShowUserControl(_uccrudDocuments);
            _uccrudDocuments.FillTable(_ucDataSource.Articles);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Set red nav
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            // Set main button
            btnSettings.BackColor = _firstColor;
            btnSettings.ForeColor = Color.Black;
            // Set other buttons
            btnDataSource.BackColor = _secondColor;
            btnSearch.BackColor = _secondColor;
            btnDocuments.BackColor = _secondColor;
            btnDataSource.ForeColor = Color.White;
            btnSearch.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;
        }

        private void btnDataSource_Click(object sender, EventArgs e)
        {
            // Set red nav
            pnlNav.Height = btnDataSource.Height;
            pnlNav.Top = btnDataSource.Top;
            // Set main button
            btnDataSource.BackColor = _firstColor;
            btnDataSource.ForeColor = Color.Black;
            // Set other buttons
            btnSearch.BackColor = _secondColor;
            btnDocuments.BackColor = _secondColor;
            btnDocuments.BackColor = _secondColor;
            btnSearch.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;

            ShowUserControl(DataSourceUC);
        }

        private void ShowUserControl (UserControl userControl)
        {
            for (int i = 0; i < PanelContainer.Controls.Count; i++)
            {
                if (PanelContainer.Controls[i].Name == userControl.GetType().Name)
                {
                    PanelContainer.Controls[userControl.GetType().Name].BringToFront();
                    break;
                }
            }
           
        }
    }
}
