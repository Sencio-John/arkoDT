using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using ARKODesktop.Models;

namespace ARKODesktop.Controller.DAO
{
    class VesselDataAccess
    {
        private string connectionString = $@"Data Source={Application.StartupPath}\ArkoDB.db;Version=3;";
        public bool AddVessel(BluetoothComms btInfo)
        {
            MessageBox.Show($"Database Path: {connectionString}", "Debug Path", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string deviceName = btInfo.DeviceName;
            string ipAddress = btInfo.IpAddress;
            string networkName = btInfo.DeviceNetwork;
            string dateCreated = DateTime.Now.ToString("yyyy-MM-dd");
            string timeCreated = DateTime.Now.ToString("HH:mm:ss tt");

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    string addDevice = "INSERT INTO Vessel" +
                                        "([vessel_name], [ip_address], [network_name], [date_created], [time_created]) VALUES" +
                                        "(@DeviceName, @IpAddress, @NetworkName, @DateCreated, @TimeCreated)";

                    using (SQLiteCommand cmd = new SQLiteCommand(addDevice, con))
                    {
                        cmd.Parameters.AddWithValue("@DeviceName", deviceName);
                        cmd.Parameters.AddWithValue("@IpAddress", ipAddress);
                        cmd.Parameters.AddWithValue("@NetworkName", networkName);
                        cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
                        cmd.Parameters.AddWithValue("@TimeCreated", timeCreated);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<Vessel> SelectVessel()
        {
            
        }

    }
}
