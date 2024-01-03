﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    public class ConnectionManager
    {
        private static ConnectionManager _instance;
        private String connectionString;
        private const String CRYPTOKEY = "ThisIsASecretKey1234567890123456";
        private ConnectionManager() { }
        public static ConnectionManager Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new ConnectionManager();

                return _instance;
            }
        }

        public void Disconnect()
        {
            if (_instance != null)
            {
                _instance.connectionString = null;
                _instance = null;

            }

        }


        public Boolean SetupConnectionString(string login = null, string password = null)
        {
            string backup = connectionString;
            connectionString = $"Data Source={Properties.Settings.Default.dbConnSourceAddr}{(Properties.Settings.Default.DBPort == "" ? "" : $",{Properties.Settings.Default.DBPort}")};Database={Properties.Settings.Default.ICatalog};";

            if (login is null || password is null)
            {
                MessageBox.Show("Поля для регистрации не могут быть пусты");
                return false;
            }
            else
                connectionString += $"User Id = {login};Password='{password}'";

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
            catch (Exception e)
            {
                connectionString = backup;
                MessageBox.Show("Ошибка создания соединения. Проверьте данные и попробуйте ещё раз");
                Console.WriteLine(e.Message);
                return false;

            }
            return true;

        }



        public async Task<SqlConnection> OpenConnection()
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                await Task.Run(() => connection.OpenAsync());
                return connection;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка установки пользователя: " + ex.Message);
                connection?.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка открытия сеодинения");
                connection?.Dispose();
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
                MessageBox.Show("Ошибка регистрации");
            }
            finally
            {
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
            using (Aes aesAlg = Aes.Create())
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
            using (Aes aesAlg = Aes.Create())
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
