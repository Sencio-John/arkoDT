using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using Newtonsoft.Json;
using NAudio.Wave;

namespace arkoDT.Controller
{
    public partial class frmControls : Form
    {
        private WebSocket _client;
        private byte speed = 0;
        private UdpClient udpClient;
        private WaveInEvent waveIn;
        private WaveOutEvent waveOut;
        private BufferedWaveProvider waveProvider;
        private IPEndPoint serverEndPoint;
        private bool isMicEnabled = true;
        private bool isSpeakerEnabled = true;

        public frmControls()
        {
            InitializeComponent();
            _client = new WebSocket("ws://192.168.1.224:4343/controls");
            serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.224"), 8712);
            this.KeyPreview = true;

        }

        private void frmControls_Load(object sender, EventArgs e)
        {
            StartRecording();
            StartReceiving();
            try
            {
                _client.Connect();
                MessageBox.Show("Connected to WebSocket server.", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                StartConnectionMonitoring();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to WebSocket server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {

            if (_client != null && _client.IsAlive)
            {
                _client.Close();
                _client = null;
            }

            waveIn?.StopRecording();
            waveIn?.Dispose();

            waveOut?.Stop();
            waveOut?.Dispose();
        }

        #region Websocket/Controls

        private async Task SendControlCommandAsync(string message)
        {
            try
            {
                if (_client != null && _client.IsAlive)
                {
                    await Task.Run(() => _client.Send(message));
                    Console.WriteLine($"Command sent: {message}");
                }
                else
                {
                    Console.WriteLine("WebSocket is not connected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending control command: {ex.Message}");
            }
        }

        private async void FrmControls_KeyDown(object sender, KeyEventArgs e)
        {
            lblTest.Text = "this is not working??";
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.W:
                        if (speed < 100) speed += 10;
                        lblTest.Text = "you clicked W";
                        break;

                    case Keys.S:
                        if (speed > 0) speed -= 10;
                        break;

                    case Keys.D:
                        await SendControlCommandAsync(JsonConvert.SerializeObject(new { turn = "starboard" }));
                        break;

                    case Keys.A:
                        await SendControlCommandAsync(JsonConvert.SerializeObject(new { turn = "port" }));
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling key press: {ex.Message}");
            }
        }

        private async void FrmControls_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
                {
                    await SendControlCommandAsync(JsonConvert.SerializeObject(new { speed }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling key release: {ex.Message}");
            }
        }

        private Task EnsureWebSocketConnectedAsync()
        {
            if (_client == null)
            {
                _client = new WebSocket("ws://192.168.1.224:4343/controls");
            }
            if (_client == null || !_client.IsAlive)
            {
                try
                {
                    if (_client == null)
                    {
                        _client = new WebSocket("ws://192.168.1.224:4343/controls");
                    }
                    _client.Connect();
                    Console.WriteLine("WebSocket connected successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"WebSocket connection failed: {ex.Message}");
                    throw;
                }
            }
            return Task.CompletedTask; // No need for async/await
        }

        private void StartConnectionMonitoring()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await EnsureWebSocketConnectedAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error Websocket Connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    await Task.Delay(10000);
                }
            });
        }

        #endregion Websocket/Controls

        #region Voice Calls
        private void StartRecording()
        {
            try
            {
                udpClient = new UdpClient();
                serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.224"), 8712);

                waveIn = new WaveInEvent
                {
                    WaveFormat = new WaveFormat(48000, 16, 1),
                    BufferMilliseconds = 100
                };
                waveIn.DataAvailable += WaveIn_DataAvailable;

                Task.Run(() =>
                {
                    waveIn.StartRecording();
                });

                Console.WriteLine("Recording started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting recording: {ex.Message}");
            }
        }

        private async void StartReceiving()
        {
            try
            {
                udpClient = new UdpClient(8712);

                waveOut = new WaveOutEvent();
                waveProvider = new BufferedWaveProvider(new WaveFormat(44100, 16, 1))
                {
                    BufferLength = 8192 * 10,
                    DiscardOnBufferOverflow = true 
                };

                waveOut.Init(waveProvider);
                waveOut.Play();

                serverEndPoint = new IPEndPoint(IPAddress.Any, 8712);

                await Task.Run(ReceiveAudioData); 

                Console.WriteLine("Receiving audio started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting receiving audio: {ex.Message}");
            }
        }

        private async Task ReceiveAudioData()
        {
            while (true)
            {
                try
                {
                    byte[] receivedBytes = udpClient.Receive(ref serverEndPoint); 
                    waveProvider.AddSamples(receivedBytes, 0, receivedBytes.Length); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving audio data: {ex.Message}");
                    break;
                }
                await Task.Delay(15); 
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    if (udpClient != null && serverEndPoint != null)
                    {
                        udpClient.Send(e.Buffer, e.BytesRecorded, serverEndPoint);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending audio data: {ex.Message}");
                }
            });
        }

        private async void ToggleMic(object sender, EventArgs e)
        {
            try
            {
                if (isMicEnabled)
                {
                    waveIn?.StopRecording();
                    waveIn?.Dispose();
                    waveIn = null;
                    btnMicToggle.Text = "Mic: Off";
                }
                else
                {
                    waveIn = new WaveInEvent
                    {
                        WaveFormat = new WaveFormat(48000, 16, 1),
                        BufferMilliseconds = 100
                    };
                    waveIn.DataAvailable += WaveIn_DataAvailable;
                    btnMicToggle.Text = "Mic: On";
                    await Task.Run(() =>
                    {
                        waveIn.StartRecording();
                    });
                }

                isMicEnabled = !isMicEnabled;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling mic: {ex.Message}");
            }
        }

        private void ToggleSpeaker(object sender, EventArgs e)
        {
            try
            {
                if (isSpeakerEnabled)
                {
                    waveOut?.Stop();
                    waveProvider?.ClearBuffer();
                    btnSpeakerToggle.Text = "Speaker: Off";
                }
                else
                {
                    waveOut?.Play();
                    btnSpeakerToggle.Text = "Speaker: On";
                }

                isSpeakerEnabled = !isSpeakerEnabled;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling speaker: {ex.Message}");
            }
        }

        #endregion Voice Calls

    }
}
