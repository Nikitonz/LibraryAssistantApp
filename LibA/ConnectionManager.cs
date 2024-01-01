using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace LibA
{
    public class ConnectionManager
    {
        public static ConnectionManager Instance { get; private set; }
        private string connectionString;
        private const string CRYPTOKEY = "ThisSecuredKey123";

        public ConnectionManager()
        {
            if (Instance is null)
                Instance = this;
        }
        public void SetupConnectionString(string login, string password) {
            string backup = connectionString;
            connectionString = $"Server={Properties.Settings.Default.dbConnSourceAddr};Database={Properties.Settings.Default.ICatalog};User Id={login};Password='{password}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State is not ConnectionState.Open)
                    {
                        throw new Exception();
                    }
                }
            }
            catch {
                connectionString = backup;
            }
            
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



















        public async Task SendRegData(string name, string login, string password)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                
                await tcpClient.ConnectAsync(Properties.Settings.Default.dbConnSourceAddr, 8888);
                


                string credentials = $"{name},{EncryptString(login)},{EncryptString(password)}";
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(credentials);
                await networkStream.WriteAsync(bytes, 0, bytes.Length);

                string responce = await ReceiveResponceAsync(tcpClient);
                MessageBox.Show(responce);
                tcpClient.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally {
                tcpClient.Close();
                
            }
        }




        public static async Task<string> ReceiveResponceAsync(TcpClient tcpClient)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            StringBuilder responseBuilder = new StringBuilder();

            CancellationTokenSource cts = new CancellationTokenSource();
          
            cts.CancelAfter(TimeSpan.FromMinutes(1));

            int bytesRead;
            try
            {
                do
                {
                    bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length, cts.Token);
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                } while (networkStream.DataAvailable);
            }
            catch (OperationCanceledException)
            {
                
            }

            return responseBuilder.ToString();
        }


        private string EncryptString(string plainText)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(CRYPTOKEY);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private string DecryptString(string cipherText)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(CRYPTOKEY);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

    }
}
