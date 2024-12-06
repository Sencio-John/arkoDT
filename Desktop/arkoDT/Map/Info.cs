using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arkoDT
{
    public partial class frmInfo : Form
    {
        private frmMap frmMap;
        public frmInfo(frmMap frmMapInstance)
        {
            InitializeComponent();
            frmMap = frmMapInstance;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmMap.UpdateLocationsCards();
            this.Close();
        }

        private void lblPopulation_Click(object sender, EventArgs e)
        {

        }

        private void lblAve_Click(object sender, EventArgs e)
        {

        }

        private void lblCurrent_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
