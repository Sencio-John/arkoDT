using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKODesktop
{
    public partial class Operations : Form
    {
<<<<<<< Updated upstream
=======
        private Dictionary<string, Button> btnControls;
        private Dictionary<string, bool> btnToggleState;
>>>>>>> Stashed changes
        public Operations()
        {
            InitializeComponent();
<<<<<<< Updated upstream
=======
            KeyPreview = true;
            this.KeyUp += Operations_KeyUp;
            this.KeyDown += Operations_KeyDown;
            btnControls = new Dictionary<string, Button>();
            btnToggleState = new Dictionary<string, bool>();

            string rootPath = GetProjectRootPath();

            // Control Rudder and Throttle
            createBtnControls("A", 60, 720, "btnLeftRudder");
            createBtnControls("D", 280, 720, "btnRightRudder");
            createBtnControls("W", 170, 610, "btnThrottleUp");
            createBtnControls("S", 170, 720, "btnThrottleDown");

            // Toggle Controls
            createToggleControls(rootPath + @"Resources\control_icons\mic_off_white.png", 1350, 100, "btnMic");
            createToggleControls(rootPath + @"Resources\control_icons\audio_off.png", 1350, 160, "btnAudio");
            createToggleControls(rootPath + @"Resources\control_icons\light_off.png", 1350, 220, "btnLight");
            createButtonAction(rootPath + @"Resources\control_icons\pin.png", 1350, 280, "btnPin", Keys.P);
        }

        
        private void Operations_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
            {
                HighlightButton("A");
            }
            else if (e.KeyCode == Keys.D)
            {
                HighlightButton("D");
            }
            else if (e.KeyCode == Keys.W)
            {
                HighlightButton("W");
            }
            else if (e.KeyCode == Keys.S)
            {
                HighlightButton("S");
            }
            else if (e.KeyCode == Keys.M)
            {
                ToggleButtonState("btnMic");  // Toggle microphone button
            }
            else if (e.KeyCode == Keys.L)
            {
                ToggleButtonState("btnLight");  // Toggle light button
            }
            else if (e.KeyCode == Keys.P)  // Triggering highlight for Pin via key
            {
                HighlightButton("btnPin");
            }
            else if (e.KeyCode == Keys.N)
            {
                ToggleButtonState("btnAudio");
            }
        }

        private void Operations_KeyUp(object sender, KeyEventArgs e)
        {
            // Reset the button style when the key is released
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.P)
            {
                ResetButtonStyles();
            }
        }

        #region UI Behaviours

        #region Create Components
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
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

            };
            btnControl.FlatAppearance.BorderSize = 0;
            btnControls[btnTextName] = btnControl;
            Controls.Add(btnControl);
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

            // Add to the dictionary of controls
            btnControls[btnName] = btnToggle;
            btnToggleState[btnName] = false; // Initially off

            // Click event to toggle state
            btnToggle.Click += (sender, e) => ToggleButtonState(btnName);

            Controls.Add(btnToggle);
        }

        private void createButtonAction(string imagePath, int x, int y, string btnName, Keys pressKey)
        {
            Button btn = new Button
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
                btn.Image = Image.FromFile(imagePath);
                btn.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                MessageBox.Show($"Image not found at path: {imagePath}", "Error");
            }


            btnControls[btnName] = btn;  // Add to the dictionary

            if (pressKey != Keys.None)
            {
                btn.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == pressKey)
                    {
                        HighlightButton(btnName);
                    }
                };
            }
            Controls.Add(btn);

        }
        #endregion

        #region Behaviors
        private void HighlightButton(string btnTextName)
        {
            if (btnControls.ContainsKey(btnTextName))
            {
                Button btn = btnControls[btnTextName];
                btn.BackColor = Color.FromArgb(255, HexToColor("#113547"));  // Fully opaque background when button is pressed
            }
        }

        private void ResetButtonStyles()
        {
            foreach (var btn in btnControls.Values)
            {
                // Only reset the background of non-toggled buttons
                if (btn.Name != "btnMic" && btn.Name != "btnLight" && btn.Name != "btnAudio")
                {
                    btn.BackColor = Color.FromArgb(100, HexToColor("#277CA5"));  // Reset to semi-transparent background color
                }
            }
        }

        private void ToggleButtonState(string btnName)
        {
            string rootPath = GetProjectRootPath();
            if (btnToggleState.ContainsKey(btnName))
            {
                bool currentState = btnToggleState[btnName];
                btnToggleState[btnName] = !currentState;  // Toggle the state

                Button btn = btnControls[btnName];

                // Toggle the image and background color based on the button state
                if (btnToggleState[btnName])
                {
                    if (btnName == "btnMic")
                    {
                        btn.Image = Image.FromFile(rootPath + @"Resources\control_icons\mic_on_white.png");
                    }
                    else if (btnName == "btnLight")
                    {
                        btn.Image = Image.FromFile(rootPath + @"Resources\control_icons\light_on.png");
                    }
                    else if (btnName == "btnAudio")
                    {
                        btn.Image = Image.FromFile(rootPath + @"Resources\control_icons\audio_on.png");
                    }

                    btn.BackColor = Color.FromArgb(255, HexToColor("#113547"));  // On state (active)
                }
                else
                {
                    if (btnName == "btnMic")
                    {
                        btn.Image = Image.FromFile(rootPath + @"Resources\control_icons\mic_off_white.png");
                    }
                    else if (btnName == "btnLight")
                    {
                        btn.Image = Image.FromFile(rootPath + @"Resources\control_icons\light_off.png");
                    }
                    else if (btnName == "btnAudio")
                    {
                        btn.Image = Image.FromFile(rootPath + @"Resources\control_icons\audio_off.png");
                    }

                    btn.BackColor = Color.FromArgb(150, HexToColor("#277CA5"));  // Off state (inactive)
                }
            }
        }

        #endregion


        #region Styles
        private Color HexToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }

        private string GetProjectRootPath()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
>>>>>>> Stashed changes
        }
        #endregion

        #endregion
    }
}
