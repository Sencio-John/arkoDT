using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;

namespace arkoDT
{
    public partial class frmControl : Form
    {
        private MJPEGStream mjpegStream;
        private ClientWebSocket webSocket;

        public frmControl()
        {
            InitializeComponent();
            pbBattery.Value = 70;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            StartCamera();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            this.KeyPreview = true;

            await ConnectWebSocket(); // Establish WebSocket connection when the form loads
        }

        private void StartCamera()
        {
            string streamUrl = "http://192.168.1.153:8000";
            mjpegStream = new MJPEGStream(streamUrl);
            mjpegStream.NewFrame += new NewFrameEventHandler(video_NewFrame);
            mjpegStream.Start();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (System.Drawing.Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mjpegStream != null && mjpegStream.IsRunning)
            {
                mjpegStream.SignalToStop();
                mjpegStream.WaitForStop();
            }

            if (webSocket != null)
            {
                webSocket.Dispose();
            }
        }

        private async Task ConnectWebSocket()
        {
            webSocket = new ClientWebSocket();
            Uri serverUri = new Uri("wss://your-websocket-server-url"); // Replace with your WebSocket server URI

            try
            {
                await webSocket.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("WebSocket connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket connection failed: {ex.Message}");
            }
        }

        // Method to send commands via WebSocket
        private async Task SendCommand(string command)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(command);
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        // Event handler for W, A, S, D key presses
        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    btnW.BackColor = Color.LightGray;
                    break;
                case Keys.A:
                    btnA.BackColor = Color.LightGray;
                    await SendCommand("rudder_left"); // Send command to move rudder left
                    break;
                case Keys.S:
                    btnS.BackColor = Color.LightGray;
                    break;
                case Keys.D:
                    btnD.BackColor = Color.LightGray;
                    await SendCommand("rudder_right"); // Send command to move rudder right
                    break;
                case Keys.I:
                    btnI.BackColor = Color.LightGray;
                    break;
                case Keys.J:
                    btnJ.BackColor = Color.LightGray;
                    break;
                case Keys.K:
                    btnK.BackColor = Color.LightGray;
                    break;
                case Keys.L:
                    btnL.BackColor = Color.LightGray;
                    break;
                case Keys.F:
                    btnFlash.BackColor = Color.LightGray;
                    break;
            }
        }

        // Event handler for when keys are released
        private async void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    btnW.BackColor = Color.White;
                    break;
                case Keys.A:
                    btnA.BackColor = Color.White;
                    await SendCommand("rudder_stop"); // Send command to stop rudder movement
                    break;
                case Keys.S:
                    btnS.BackColor = Color.White;
                    break;
                case Keys.D:
                    btnD.BackColor = Color.White;
                    await SendCommand("rudder_stop"); // Send command to stop rudder movement
                    break;
                case Keys.I:
                    btnI.BackColor = Color.White;
                    break;
                case Keys.J:
                    btnJ.BackColor = Color.White;
                    break;
                case Keys.K:
                    btnK.BackColor = Color.White;
                    break;
                case Keys.L:
                    btnL.BackColor = Color.White;
                    break;
            }
        }

        private void btnPinnedLoc_Click(object sender, EventArgs e)
        {
            frmMap form2 = new frmMap();
            form2.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
 