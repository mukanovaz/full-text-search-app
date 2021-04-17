using FullTextSearch.Utils;
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
    public partial class UCSearching : UserControl
    {
        public UCSearching()
        {
            InitializeComponent();
        }

        private void SearchingPanel_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Controller.Instance.Index.Search(tbSearchText.Text);
        }
    }
}
