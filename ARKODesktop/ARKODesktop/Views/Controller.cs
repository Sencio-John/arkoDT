using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace ARKODesktop
{
    public partial class Operations : Form
    {
        private Dictionary<string, Button> btnControls;
        public Operations()
        {

            InitializeComponent();
            KeyPreview = true;
            this.KeyUp += Operations_KeyUp;
            this.KeyDown += Operations_KeyDown;
            btnControls = new Dictionary<string, Button>();
            string rootPath = GetProjectRootPath();

            // Control Rudder and Throttle
            createBtnControls("A", 60, 720, "btnLeftRudder");
            createBtnControls("D", 280, 720, "btnRightRudder");
            createBtnControls("W", 170, 610, "btnThrottleUp");
            createBtnControls("S", 170, 720, "btnThrottleDown");

            // Toggle Controls
            createToggleControls(rootPath + @"Resources\control_icons\mic_off.png", 1350, 99, "btnThrottleDown");
        }

        
        //Rudder and Throttle Control
        private void createBtnControls(string btnTextName, int x, int y, string btnName)
        {
            Button btnControl = new Button
            {
                Name = btnName,
                Size = new Size(100, 100),
                Location = new Point(x, y),
                BackColor = Color.FromArgb(150, HexToColor("#277CA5")),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                TabStop = true,
                Text = btnTextName,
                Font = new Font("Cera Pro", 16, FontStyle.Bold),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

            };
            btnControl.FlatAppearance.BorderSize = 0;
            btnControls[btnTextName] = btnControl;
            Controls.Add(btnControl);
        }

        //Btn Behavior when pressed
        private void Operations_KeyDown(object sender, KeyEventArgs e)
        {
            // Check which key is pressed and highlight the corresponding button
            if (e.KeyCode == Keys.A)
            {
                HighlightButton("A");  // Highlight the A button
            }
            else if (e.KeyCode == Keys.D)
            {

                HighlightButton("D");  // Highlight the D button
            }
            else if (e.KeyCode == Keys.W)
            {
                HighlightButton("W");  // Highlight the W button
            }
            else if (e.KeyCode == Keys.S)
            {

                HighlightButton("S");  // Highlight the S button
            }
        }

        //Btn Behavior when released
        private void Operations_KeyUp(object sender, KeyEventArgs e)
        {
            // Reset the button style when the key is released
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                ResetButtonStyles();  // Reset button styles
            }
        }

        //btn highlight
        private void HighlightButton(string btnTextName)
        {
            if (btnControls.ContainsKey(btnTextName))
            {
                Button btn = btnControls[btnTextName];
                btn.BackColor = Color.FromArgb(255, HexToColor("#277CA5"));  // Fully opaque background when button is pressed
            }
        }

        //Btn remove color
        private void ResetButtonStyles()
        {
            foreach (var btn in btnControls.Values)
            {
                btn.BackColor = Color.FromArgb(100, HexToColor("#277CA5"));  // Reset to semi-transparent background color
            }
        }

        private void createToggleControls(string imagePath, int x, int y, string btnName)
        {
            Button btnToggle = new Button
            {
                Name = btnName,
                Size = new Size(50, 50),
                Location = new Point(x, y),
                BackColor = Color.FromArgb(150, HexToColor("#277CA5")),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = true,
                FlatStyle = FlatStyle.Flat,
            };

            if (System.IO.File.Exists(imagePath))
            {
                btnToggle.Image = Image.FromFile(imagePath);
                btnToggle.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                MessageBox.Show($"Image not found at path: {imagePath}", "Error");
            }

            Controls.Add(btnToggle);

        }

        //convert to hex
        private Color HexToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }

        //Resource Path
        private string GetProjectRootPath()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        }
    }
}
