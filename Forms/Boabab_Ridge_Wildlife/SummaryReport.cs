using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boabab_Ridge_Wildlife
{
    public partial class frmSummaryReport : Form
    {
        public frmSummaryReport()
        {
            InitializeComponent();
        }

        private void frmSummaryReport_Load(object sender, EventArgs e)
        {

        }
        private void btnSummaryReport_Click(object sender, EventArgs e)
        { 
            Form1 main = new Form1();
            main.Show();
        }
    }
}
