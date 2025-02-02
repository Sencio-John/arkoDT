using System;
using System.Windows.Forms;
using ARKODesktop.Views;

namespace ARKODesktop
{
    public partial class EntryForm : Form
    {
        private Form frmDevices;
        private Form frmOperation;
        private Form frmUserCreation;
        private Form frmMap;

        public EntryForm()
        {
            InitializeComponent();
            btnUserCreation.Visible = false;
        }

        private void dashTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public Form loadForm(Form form)
        {
            if (this.pnlLoadForm.Controls.Count > 0)
            {
                this.pnlLoadForm.Controls.RemoveAt(0);
            }

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.pnlLoadForm.Controls.Add(form);
            form.Tag = form;
            form.Show();

            return form;
        }

        private void HideAllFormsExcept(Form visibleForm)
        {
            if (frmDevices != null && frmDevices != visibleForm) frmDevices.Hide();
            if (frmOperation != null && frmOperation != visibleForm) frmOperation.Hide();
            if (frmUserCreation != null && frmUserCreation != visibleForm) frmUserCreation.Hide();
            if (frmMap != null && frmMap != visibleForm) frmMap.Hide();

            if (this.pnlLoadForm.Controls.Count > 0)
            {
                this.pnlLoadForm.Controls.RemoveAt(0);
            }
            this.pnlLoadForm.Controls.Add(visibleForm);
        }

        private void ShowForm(ref Form currentForm, Form newForm)
        {

            if (currentForm == null || currentForm.IsDisposed)
            {
                currentForm = loadForm(newForm);
                
            }
            else
            {
                currentForm.Show();
            }

            HideAllFormsExcept(currentForm);


        }

        private void btnDevices_Click(object sender, EventArgs e)
        {
            ShowForm(ref frmDevices, new Devices());
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {
            ShowForm(ref frmOperation, new Operation());
        }

        private void btnUserCreation_Click(object sender, EventArgs e)
        {
            ShowForm(ref frmUserCreation, new UserCreation());
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("New Location Tagged in Map", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {

                ShowForm(ref frmMap, new Map());
            }
        }
    }
}
