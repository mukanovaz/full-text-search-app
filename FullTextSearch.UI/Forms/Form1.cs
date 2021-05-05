using System;
using System.Windows.Forms;
using System.Drawing;
using FullTextSearch.UI;
using System.Collections.Generic;

namespace FullTextSearch
{
    public partial class Form1 : Form
    {

        static Form1 _obj;
        static UCDataSource _ucDataSource;
        static UCSearching _ucSearching;
        private Color _firstColor = Color.FromArgb(247, 243, 233);  // Main buttons
        private Color _secondColor = Color.FromArgb(94, 170, 168);  // Other buttons
        private Color _thirdColor = Color.FromArgb(247, 243, 233);

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
        }

        public Panel PanelContainer
        {
            get { return panelContainer; }
            set { panelContainer = value; }
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

            ShowUserControl(DataSourceUC);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

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
            // Set UC
            UCSearching ucSearching = new UCSearching();
            ucSearching.Dock = DockStyle.Fill;
            PanelContainer.Controls.Add(ucSearching);
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
