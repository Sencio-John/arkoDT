using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARKODesktop.Views;

namespace ARKODesktop
{
    public partial class EntryForm : Form
    {
        public EntryForm()
        {
            InitializeComponent();
        }

        private void dashTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public void loadForm(object Form) 
        { 
            if (this.pnlLoadForm.Controls.Count > 0)
            {
                this.pnlLoadForm.Controls.RemoveAt(0);
            }

            Form form = Form as Form;

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.pnlLoadForm.Controls.Add(form);
            form.Tag = form;
            form.Show();
        }



        private void btnDashboard_Click(object sender, EventArgs e)
        {
            loadForm(new Dashboard());
        }

        private void btnDevices_Click(object sender, EventArgs e)
        {
            loadForm(new Devices());
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {
            loadForm(new Operation());
        }
    }
}
