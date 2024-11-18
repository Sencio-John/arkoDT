using System;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using arkoDT.Classes;

namespace arkoDT
{
    public partial class frmServerHandler : Form
    {
        private IFirebaseClient client;
        private Dictionary<string, ControlCenter> controlCenters;
        Firebase_Config firebaseConfig = new Firebase_Config();
        private String my_ip;
        public frmServerHandler()
        {
            InitializeComponent();
            client = firebaseConfig.GetClient();

            if (client == null)
            {
                MessageBox.Show("Failed to connect to Database.");
            }
        }
        async private void frmServerHandler_Load(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("Controls_Center/");
            controlCenters = response.ResultAs<Dictionary<string, ControlCenter>>();
        }

        async private void btnSubmitData_Click(object sender, EventArgs e)
        {
            my_ip = GetLocalIPAddress();
            ControlCenter newCC = new ControlCenter{
                ccName = txtCCID.Text,
                ccPass = txtCCPassword.Text,
                ccAddress = txtAddress.Text,
                ccIP = my_ip
            };
            SetResponse response = await client.SetAsync("Controls_Center/" + "testingID", newCC);
        }


        static string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
