using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW3
{
    public partial class InputDialog : Form
    {
        public string InputText
        {
            get { return tbInputText.Text; }
            set { tbInputText.Text = value; }
        }
        public InputDialog()
        {
            InitializeComponent();
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
