using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    public class ConnectionManager
    {
        public static ConnectionManager Instance { get; private set; }
        private string connectionString;

        public ConnectionManager()
        {
            if (Instance is null)
                Instance = this;
        }


        public async Task<SqlConnection> OpenConnection(string login, string password)
        {
            SqlConnection connection = new(connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<SqlConnection> OpenConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }



















        public async Task SetupConnection(string name, string login, string password)
        {
            TcpClient tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(Properties.Settings.Default.dbConnStrMain, 8888);
            string credentials = $"{name},{login},{password}";

            string response = await SendDataAsync(tcpClient, credentials);

            // Парсим ответ
            if (TryParseResponse(response, out int responseCode, out string errorText))
            {
                if (responseCode == 200)
                {
                    this.connectionString = $"Data Source={Properties.Settings.Default.dbConnStrMain};Initial Catalog={Properties.Settings.Default.ICatalog};User ID={login};Password='{password}'";
                }
                else
                {
                    MessageBox.Show($"Ошибка при установке соединения: {errorText}");
                }
            }
            else
            {
                MessageBox.Show("Ошибка при разборе ответа.");
            }

            tcpClient.Close();
        }

        public static async Task<string> SendDataAsync(TcpClient tcpClient, string data)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            await networkStream.WriteAsync(bytes, 0, bytes.Length);
            return await ReceiveDataAsync(tcpClient);
        }

        private static bool TryParseResponse(string response, out int responseCode, out string errorText)
        {
            string[] parts = response.Split('|');
            if (parts.Length >= 1 && int.TryParse(parts[0], out responseCode))
            {
                errorText = (parts.Length >= 2) ? parts[1] : null;
                return true;
            }
            else
            {
                responseCode = -1;
                errorText = null;
                return false;
            }
        }

        public static async Task<string> ReceiveDataAsync(TcpClient tcpClient)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            StringBuilder responseBuilder = new StringBuilder();

            // Читаем данные, пока они поступают
            int bytesRead;
            do
            {
                bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            } while (networkStream.DataAvailable);

            return responseBuilder.ToString();
        }


    }
}
