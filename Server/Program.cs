using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class ClientInfo
    {
        public TcpClient? Client { get; set; }
        public string? Username { get; set; }
        public NetworkStream? Stream { get; set; }
    }

    public class ChatServer
    {
        private TcpListener? listener;
        private List<ClientInfo> clients = new List<ClientInfo>();
        private bool isRunning = false;

        public async Task StartServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            isRunning = true;

            Console.WriteLine($"Server started on port {port}");

            while (isRunning)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Task ClientHandling = Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Accept error: {ex.Message}");
                }
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            ClientInfo clientInfo = new ClientInfo { Client = client, Stream = client.GetStream() };
            byte[] buffer = new byte[4096];

            try
            {
                int bytesRead = await clientInfo.Stream.ReadAsync(buffer, 0, buffer.Length);
                string loginData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string[] parts = loginData.Split('|');
                string username = parts[1];
                clientInfo.Username = username;
                clients.Add(clientInfo);
                BroadcastSystemMessage($"{username} has appear");
                await SendUserListToAll();
                Console.WriteLine($"{username} connected from {client.Client.RemoteEndPoint}");
                while (client.Connected)
                {
                    bytesRead = await clientInfo.Stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessClientMessage(clientInfo, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with client {clientInfo.Username}: {ex.Message}");
            }
            finally
            {
                clients.Remove(clientInfo);
                if (!string.IsNullOrEmpty(clientInfo.Username))
                {
                    BroadcastSystemMessage($"{clientInfo.Username} has cease to exist");
                    await SendUserListToAll();
                    Console.WriteLine($"{clientInfo.Username} disconnected");
                }
                client.Close();
            }
        }

        private void ProcessClientMessage(ClientInfo sender, string message)
        {
            string[] messages = message.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string msg in messages)
            {
                string[] parts = msg.Split('|');
                if (parts.Length < 2) continue;

                string command = parts[0];
                string content = parts[1];

                switch (command)
                {
                    case "MESSAGE":
                        if (parts.Length >= 2)
                        {
                            BroadcastMessage(sender.Username, content);
                        }
                        break;

                    case "LOGOUT":
                        Console.WriteLine($"{sender.Username ?? "Unknown"} mengirim sinyal logout");
                        break;
                }
            }
        }

        private async void BroadcastMessage(string sender, string message)
        {
            string formattedMessage = $"MESSAGE|{sender}|{message}";
            byte[] data = Encoding.UTF8.GetBytes(formattedMessage + Environment.NewLine);

            foreach (var client in clients.ToArray())
            {
                try
                {
                    await client.Stream.WriteAsync(data, 0, data.Length);
                }
                catch
                {
                    clients.Remove(client);
                }
            }

            Console.WriteLine($"{sender}: {message}");
        }

        private async void BroadcastSystemMessage(string message)
        {
            string formattedMessage = $"SYSTEM|{message}";
            byte[] data = Encoding.UTF8.GetBytes(formattedMessage + Environment.NewLine);

            foreach (var client in clients.ToArray())
            {
                try
                {
                    if (client.Stream != null)
                    {
                        await client.Stream.WriteAsync(data, 0, data.Length);
                    }
                }
                catch
                {
                    clients.Remove(client);
                }
            }

            Console.WriteLine($"System: {message}");
        }

        private async Task SendUserListToAll()
        {
            string userList = string.Join(",", GetUsernames());
            string formattedMessage = $"USERLIST|{userList}";
            byte[] data = Encoding.UTF8.GetBytes(formattedMessage + Environment.NewLine);

            foreach (var client in clients.ToArray())
            {
                try
                {
                    if (client.Stream != null)
                    {
                        await client.Stream.WriteAsync(data, 0, data.Length);
                    }
                }
                catch
                {
                    clients.Remove(client);
                }
            }
        }

        private List<string> GetUsernames()
        {
            var usernames = new List<string>();
            foreach (var client in clients)
            {
                if (client.Username != null)
                {
                    usernames.Add(client.Username);
                }
            }
            return usernames;
        }

        public void StopServer()
        {
            isRunning = false;
            listener?.Stop();

            foreach (var client in clients)
            {
                client.Client?.Close();
            }

            clients.Clear();
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            ChatServer server = new ChatServer();
            int port = 8888;
            if (args.Length > 0 && int.TryParse(args[0], out int customPort))
            {
                port = customPort;
            }
            Console.WriteLine("Starting chat server...");
            Console.WriteLine($"Press any key to stop the server");
            var serverTask = server.StartServer(port);
            Console.ReadKey();
            server.StopServer();
            await serverTask;

            Console.WriteLine("Server stopped");
        }
    }
}