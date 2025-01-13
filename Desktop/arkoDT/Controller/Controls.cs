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
        private ClientWebSocket _client;
        private MJPEGStream mjpegStream;
        private ClientWebSocket webSocket;
        private bool isFirstImage = true;
        private Image flashOnImage;
        private Image flashOffImage;

        public frmControl()
        {
            InitializeComponent();
            InitializeWebSocket();
            pbBattery.Value = 70;

            // Load the images once (make sure paths are correct)
            flashOnImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/flashon.png");
            flashOffImage = Image.FromFile("C:/Users/SENCIO/Documents/GitHub/arkoDT/Desktop/arkoDT/Resources/flashoff.png");

            // Set the initial image and BackgroundImageLayout to Zoom
            btnFlash.BackgroundImage = flashOffImage;
            btnFlash.BackgroundImageLayout = ImageLayout.Zoom;  // Set the layout to Zoom

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            StartCamera();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            this.KeyPreview = true;

            await ConnectWebSocket(); // Establish WebSocket connection when the form loads
        }
        private async void InitializeWebSocket()
        {
            _client = new ClientWebSocket();
            try
            {
                string serverUri = "ws://192.168.1.224:4343/controls";
                await _client.ConnectAsync(new Uri(serverUri), CancellationToken.None);
                Console.WriteLine("WebSocket connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting WebSocket: {ex.Message}");
            }
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

        // Event handler for W, A, S, D key presses
        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_client.State != WebSocketState.Open)
            {
                Console.WriteLine("WebSocket is not connected.");
                return;
            }

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        await SendCommand("{'speed': 10}");
                        btnW.BackColor = Color.LightGray;
                        break;

                    case Keys.A:
                        await SendCommand("{\"command\": \"rudder_left\"}");
                        btnA.BackColor = Color.LightGray;
                        break;

                    case Keys.S:
                        btnS.BackColor = Color.LightGray;
                        break;

                    case Keys.D:
                        await SendCommand("{\"command\": \"rudder_right\"}");
                        btnD.BackColor = Color.LightGray;
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
                        btnFlash.BackgroundImage = isFirstImage ? flashOnImage : flashOffImage;
                        isFirstImage = !isFirstImage;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task SendCommand(string command)
        {
            try
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(command);
                await _client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending command: {ex.Message}");
            }
        }

        // Event handler for when keys are released
        private async void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
 