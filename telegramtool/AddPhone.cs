using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telegramtool
{
    public partial class AddPhone : Form
    {
        public AddPhone()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = richTextBox1.Text;
            userIO user = new userIO();
            var path = user.getPath();
            user.readUserByFile(path);
            user.readUserByString(text);
            user.WriteUserByFile(path);
            this.Close();
        }
    }
}
