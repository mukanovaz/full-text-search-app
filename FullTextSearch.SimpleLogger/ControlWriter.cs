using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FullTextSearch.SimpleLogger
{
    public class ControlWriter : TextWriter
    {
        /// <summary>
        /// GUI control where we will display text
        /// </summary>
        private Control _textbox;

        /// <summary>
        /// Constructor which creates instance of ControlWriter class
        /// </summary>
        /// <param name="textbox"></param>
        public ControlWriter(Control textbox)
        {
            this._textbox = textbox;
        }

        public override void Write(char value)
        {
            _textbox.Invoke(new Action(() => _textbox.Text += value));
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
