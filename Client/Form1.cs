using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;
        private string username = "";
        private readonly int port = 8888;

        private bool keepReceiving = false;

        public Form1()
        {
            InitializeComponent();
            ToggleChatFunctionality(false);
        }

        private void ToggleChatFunctionality(bool enable)
        {
            send_msg_btn.Enabled = enable;
            message_box.Enabled = enable;
            chat_list_box.Enabled = enable;
        }

        private async void Connect_ip_btn_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectFromServer();
                return;
            }

            await ConnectToServer();
        }

        private async Task ConnectToServer()
        {
            try
            {
                string ipAddress = connect_ip_box.Text.Trim();
                username = set_username_box.Text.Trim();

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter a username");
                    return;
                }

                if (string.IsNullOrEmpty(ipAddress))
                {
                    MessageBox.Show("Please enter a server IP address");
                    return;
                }

                connect_ip_btn.Enabled = false;
                SetStatus("Connecting...");

                client = new TcpClient();

                var connectTask = client.ConnectAsync(ipAddress, port);
                var timeoutTask = Task.Delay(3000);

                if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                {
                    throw new Exception("Connection timeout");
                }

                await connectTask; 

                stream = client.GetStream();
                isConnected = true;
                keepReceiving = true;

                byte[] data = Encoding.UTF8.GetBytes($"LOGIN|{username}");
                await stream.WriteAsync(data, 0, data.Length);

                Task receiveTask = Task.Run(ReceiveData);

                ToggleChatFunctionality(true);
                connect_ip_btn.Text = "Disconnect";
                connect_ip_btn.Enabled = true;
                set_username_box.Enabled = false;
                connect_ip_box.Enabled = false;
                SetStatus("Connected");

                AddSystemMessage("Connected to server successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
                connect_ip_btn.Enabled = true;
                DisconnectFromServer();
                SetStatus("Connection failed");
            }
        }

        private async Task ReceiveData()
        {
            byte[] buffer = new byte[4096];

            while (isConnected && keepReceiving)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Invoke(new Action(() =>
                        {
                            AddSystemMessage("Disconnected from server");
                            DisconnectFromServer();
                        }));
                        break;
                    }

                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessReceivedData(data);
                }
                catch (Exception ex)
                {
                    if (isConnected)
                    {
                        Invoke(new Action(() =>
                        {
                            AddSystemMessage($"Network error: {ex.Message}");
                            DisconnectFromServer();
                        }));
                    }
                    break;
                }
            }
        }

        private void ProcessReceivedData(string data)
        {
            string[] messages = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string message in messages)
            {
                string[] parts = message.Split('|');
                if (parts.Length < 2) continue;

                string command = parts[0];
                string content = parts[1];

                switch (command)
                {
                    case "MESSAGE":
                        if (parts.Length >= 3)
                        {
                            string sender = parts[1];
                            string msg = parts[2];
                            AddMessageToChat(sender, msg);
                        }
                        break;

                    case "USERLIST":
                        UpdateUserList(content);
                        break;

                    case "SYSTEM":
                        AddSystemMessage(content);
                        break;
                }
            }
        }

        private void AddMessageToChat(string sender, string message)
        {
            if (chat_list_box.InvokeRequired)
            {
                chat_list_box.Invoke(new Action<string, string>(AddMessageToChat), sender, message);
            }
            else
            {
                chat_list_box.AppendText($"[{DateTime.Now:HH:mm}] {sender}: {message}\r\n");
                chat_list_box.ScrollToCaret();
            }
        }

        private void AddSystemMessage(string message)
        {
            if (chat_list_box.InvokeRequired)
            {
                chat_list_box.Invoke(new Action<string>(AddSystemMessage), message);
            }
            else
            {
                chat_list_box.AppendText($"[{DateTime.Now:HH:mm}] System: {message}\r\n");
                chat_list_box.ScrollToCaret();
            }
        }

        private void UpdateUserList(string userList)
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(new Action<string>(UpdateUserList), userList);
            }
            else
            {
                listBox1.Items.Clear();
                string[] users = userList.Split(',');
                foreach (string user in users)
                {
                    if (!string.IsNullOrEmpty(user))
                    {
                        listBox1.Items.Add(user);
                    }
                }

                user_online.Text = $"Users online: {listBox1.Items.Count}";
            }
        }

        private void SetStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetStatus), status);
            }
            else
            {
                this.Text = $"Cultivation chat - {status}";
            }
        }

        private async void SendMessage()
        {
            if (!isConnected || stream == null)
            {
                MessageBox.Show("Not connected to server");
                return;
            }

            string message = message_box.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            try
            {
                string protocolMessage;
                protocolMessage = $"MESSAGE|{message}";
                byte[] data = Encoding.UTF8.GetBytes(protocolMessage);
                await stream.WriteAsync(data, 0, data.Length);
                message_box.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
                DisconnectFromServer();
            }
        }

        private void Send_msg_btn_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void DisconnectFromServer()
        {
            isConnected = false;
            keepReceiving = false;

            try
            {
                if (client != null && client.Connected && stream != null)
                {
                    byte[] data = Encoding.UTF8.GetBytes("LOGOUT|");
                    stream.Write(data, 0, data.Length);
                }
            }
            catch
            {

            }

            try { stream?.Close(); } catch { }
            try { client?.Close(); } catch { }

            stream = null;
            client = null;

            if (InvokeRequired)
            {
                Invoke(new Action(DisconnectFromServer));
                return;
            }

            ToggleChatFunctionality(false);
            connect_ip_btn.Text = "Connect";
            connect_ip_btn.Enabled = true;
            set_username_box.Enabled = true;
            connect_ip_box.Enabled = true;
            listBox1.Items.Clear();
            user_online.Text = "Users online: 0";
            SetStatus("Disconnected");
        }
    }
}