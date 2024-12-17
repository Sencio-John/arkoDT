using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ARKODesktop
{
    public class ThrottleControl : Control
    {
        private int throttleValue = 0;
        public int ThrottleValue
        {
            get { return throttleValue; }
            set
            {
                throttleValue = Math.Max(0, Math.Min(value, 100));
                Invalidate(); // Redraw control
            }
        }

        public ThrottleControl()
        {
            this.Size = new Size(300, 60);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Rectangle bounds = this.ClientRectangle;

            // Draw border (optional for better UI appearance)
            using (Pen borderPen = new Pen(Color.Gray, 2))
            {
                g.DrawRectangle(borderPen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            }

            // Draw background
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(255, 39, 124, 165)))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            // Draw the throttle fill (starting from bottom)
            int fillHeight = (int)(ThrottleValue / 100.0 * bounds.Height);
            using (SolidBrush fillBrush = new SolidBrush(Color.FromArgb(200, 17, 53, 71)))
            {
                g.FillRectangle(fillBrush, new Rectangle(bounds.X, bounds.Bottom - fillHeight, bounds.Width, fillHeight));
            }

            // Draw throttle percentage text at the center
            string label = $"{ThrottleValue}%";
            using (Font font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(label, font);
                PointF textPos = new PointF(
                    bounds.X + (bounds.Width - textSize.Width) / 2,
                    bounds.Y + (bounds.Height - textSize.Height) / 2);
                g.DrawString(label, font, textBrush, textPos);
            }
        }
    }


    public partial class Operations : Form
    {

        private Dictionary<string, Button> btnControls;
        private Dictionary<string, bool> btnToggleState;
        private ThrottleControl customThrottleControl;
        public Operations()
        {
            InitializeComponent();
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
            createToggleControls(rootPath + @"Resources\control_icons\mic_off_white.png", 1525, 100, "btnMic");
            createToggleControls(rootPath + @"Resources\control_icons\audio_off.png", 1525, 160, "btnAudio");
            createToggleControls(rootPath + @"Resources\control_icons\light_off.png", 1525, 220, "btnLight");
            createButtonAction(rootPath + @"Resources\control_icons\pin.png", 1525, 280, "btnPin", Keys.P);

            //Time Panel
            createPanelTime("pnlTime", 1375, 12);
            lblIRSensor.ForeColor = Color.FromArgb(225, HexToColor("#113547"));
            lblLatitude.ForeColor = Color.FromArgb(225, HexToColor("#113547"));
            lblLongitude.ForeColor = Color.FromArgb(225, HexToColor("#113547"));
            lblWaterLvl.ForeColor = Color.FromArgb(225, HexToColor("#113547"));

            //Throttle
            CreateThrottleControl();
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
                IncreaseThrottle();
            }
            else if (e.KeyCode == Keys.S)
            {
                HighlightButton("S");
                DecreaseThrottle();
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

        private void createPanelTime(string pnlName, int x, int y)
        {
            Panel panel = new Panel
            {
                Name = pnlName,
                Size = new Size(200, 50),
                Location = new Point(x, y),
                BackColor = Color.FromArgb(150, HexToColor("#113547")),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = true,
            };

            Label timeLabel = new Label
            {
                Name = pnlName + "_TimeLabel",
                AutoSize = false,
                Size = new Size(panel.Width, panel.Height),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),

            };

            timeLabel.Location = new Point(
                (panel.Width - timeLabel.Width) / 2, // Center horizontally
                (panel.Height - timeLabel.Height) / 2 // Center vertically
            );



            Timer timer = new Timer
            {
                Interval = 1000
            };

            // Update the Label text every tick
            timer.Tick += (sender, args) =>
            {
                timeLabel.Text = DateTime.Now.ToString("hh:mm tt");
            };

            // Start the Timer
            timer.Start();
            panel.Controls.Add(timeLabel);
            this.Controls.Add(panel);

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

        private void CreateThrottleControl()
        {
            customThrottleControl = new ThrottleControl
            {
                Location = new Point(50, 300),
                Size = new Size(50, 300),
                Dock = DockStyle.Left,
                Anchor = AnchorStyles.Left,
            };
            Controls.Add(customThrottleControl);
        }

        private void IncreaseThrottle()
        {
            if (customThrottleControl.ThrottleValue < 100)
            {
                customThrottleControl.ThrottleValue += 10;
            }
        }

        private void DecreaseThrottle()
        {
            if (customThrottleControl.ThrottleValue > 0)
            {
                customThrottleControl.ThrottleValue -= 10;
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

        }
        #endregion

        #endregion

    }
}


