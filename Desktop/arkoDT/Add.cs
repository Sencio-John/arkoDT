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
    public partial class frmAdd : Form
    {
        private frmDevices frmDevices;
        public frmAdd(frmDevices frmDevicesInstance)
        {
            InitializeComponent();
            frmDevices = frmDevicesInstance;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDevices.UpdateDeviceCards();
        }
    }
}
