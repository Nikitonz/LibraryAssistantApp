using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlCreateUser
{
    public partial class SqlServerUserRegister : ServiceBase
    {
        private TcpListener tcpListener;
        private CancellationTokenSource cancellationTokenSource;
        private const string CRYPTOKEY = "ThisIsASecretKey1234567890123456";
        private string connectionString = $"Data Source=localhost;Initial Catalog=Библиотека;User ID=sa; Password=33223311";

        public SqlServerUserRegister()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            cancellationTokenSource = new CancellationTokenSource();
            tcpListener = new TcpListener(IPAddress.Any, 8888);
            tcpListener.Start();

            // Проверка подключения к базе данных при старте службы
           
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        EventLog.WriteEntry("SQLSlujba", "Успешное подключение к базе данных.", EventLogEntryType.Information);
                        _ = AcceptConnectionsAsync(cancellationTokenSource.Token);
                    }
                    else
                    {
                        EventLog.WriteEntry("SQLSlujba", "Ошибка: Не удалось установить соединение с базой данных.", EventLogEntryType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                
                Stop();
              
            }
        }

        protected override void OnStop()
        {
            cancellationTokenSource?.Cancel();
            tcpListener?.Stop();
        }

        private async Task AcceptConnectionsAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    _ = HandleClientAsync(tcpClient, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при остановке службы
                Console.WriteLine(ex.Message);
            }
        }

        private async Task HandleClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            string responce = null;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                int bytesRead = await networkStream.ReadAsync(data, 0, data.Length, cancellationToken);

                string receivedData = Encoding.UTF8.GetString(data, 0, bytesRead);
                string[] credentials = receivedData.Split(',');
                if (credentials.Length >= 3)
                {
                    string userName = credentials[0].Trim();
                    string login = DecryptString(credentials[1].Trim());
                    string password = DecryptString(credentials[2].Trim());
                   
                    // Encrypt the password before storing it
                    string salt = GenerateSalt();
                    
                    string hashedPassword = HashPassword(password, salt);


                    

                    string query = $"INSERT INTO Пользователь ([Фамилия], [Имя], [Отчество], [Имя для входа], [Хэш пароля], [Соль]) " +
                                   $"VALUES (@Фамилия, @Имя, @Отчество, @ИмяДляВхода, @HashedPassword, @Salt)";
                   
                    try
                    {
                        using (SqlConnection connectionTRUSTABLE = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(query, connectionTRUSTABLE);
                            connectionTRUSTABLE.Open();

                            string[] ui = userName.Split('_');
                            command.Parameters.AddWithValue("@Фамилия", ui.Length > 0 ? ui[0] : (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Имя", ui.Length > 1 ? ui[1] : (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Отчество", ui.Length > 2 ? ui[2] : (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ИмяДляВхода", login);
                            command.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                            command.Parameters.AddWithValue("@Salt", salt);
                            
                            await command.ExecuteNonQueryAsync(cancellationToken);
                            
                        }
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("SQLSlujba", "Ошибка в создании записи.\n" + e.Message, EventLogEntryType.Error);
                        responce = $"500|{e.Message}";
                    }

                    if (responce is null)
                        responce = "200";

                    EventLog.WriteEntry("SQLSlujba", "Ответ сервера:\n" + responce, EventLogEntryType.Warning);

                    byte[] responseData = Encoding.UTF8.GetBytes(responce);
                    await networkStream.WriteAsync(responseData, 0, responseData.Length);
                    await networkStream.FlushAsync();

                    networkStream.Close();
                    tcpClient.Close();
                }
                else
                {
                    EventLog.WriteEntry("SQLSlujba", "Ошибка в создании записи.\nНекорректные данные полуцченыф" +  EventLogEntryType.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("finally:" + ex.Message);
            }
        }
        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        private static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password); // Используем Unicode
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt); // Используем Unicode

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hashBytes);
            }
        }
        /*
        public static string EncryptString(string plainText)
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
        */
        public static string DecryptString(string cipherText)
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
