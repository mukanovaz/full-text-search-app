using System;
using System.Windows.Forms;
using System.Drawing;

namespace FullTextSearch
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
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
            btnSearch.BackColor = Color.FromArgb(247, 243, 233);
            btnSearch.ForeColor = Color.Black;
            // Set other buttons
            btnDataSource.BackColor = Color.FromArgb(94, 170, 168);
            btnDocuments.BackColor = Color.FromArgb(94, 170, 168);
            btnSettings.BackColor = Color.FromArgb(94, 170, 168);
            btnDataSource.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;
            btnSettings.ForeColor = Color.White;
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            // Set red nav
            pnlNav.Height = btnDocuments.Height;
            pnlNav.Top = btnDocuments.Top;
            // Set main button
            btnDocuments.BackColor = Color.FromArgb(247, 243, 233);
            btnDocuments.ForeColor = Color.Black;
            // Set other buttons
            btnSettings.BackColor = Color.FromArgb(94, 170, 168);
            btnSearch.BackColor = Color.FromArgb(94, 170, 168);
            btnDataSource.BackColor = Color.FromArgb(94, 170, 168);
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
            btnSettings.BackColor = Color.FromArgb(247, 243, 233);
            btnSettings.ForeColor = Color.Black;
            // Set other buttons
            btnDataSource.BackColor = Color.FromArgb(94, 170, 168);
            btnSearch.BackColor = Color.FromArgb(94, 170, 168);
            btnDocuments.BackColor = Color.FromArgb(94, 170, 168);
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
            btnDataSource.BackColor = Color.FromArgb(247, 243, 233);
            btnDataSource.ForeColor = Color.Black;
            // Set other buttons
            btnSearch.BackColor = Color.FromArgb(94, 170, 168);
            btnDocuments.BackColor = Color.FromArgb(94, 170, 168);
            btnDocuments.BackColor = Color.FromArgb(94, 170, 168);
            btnSearch.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;
            btnDocuments.ForeColor = Color.White;
        }
    }
}
