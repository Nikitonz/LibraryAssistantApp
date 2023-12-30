using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
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

        public SqlServerUserRegister()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            cancellationTokenSource = new CancellationTokenSource();
            tcpListener = new TcpListener(IPAddress.Any, 8888);
            tcpListener.Start();

            _ = AcceptConnectionsAsync(cancellationTokenSource.Token);
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
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                int bytesRead = await networkStream.ReadAsync(data, 0, data.Length, cancellationToken);

                string receivedData = Encoding.UTF8.GetString(data, 0, bytesRead);
                string[] credentials = receivedData.Split(',');

                if (credentials.Length == 3)
                {
                    string userName = credentials[0].Trim();
                    string login = credentials[1].Trim();
                    string password = credentials[2].Trim();



                    string connectionString = $"Data Source=localhost;Initial Catalog=Библиотека;Integrated Security=True";

                    string query = $"CREATE LOGIN {login} WITH PASSWORD = '{password}'; " +
                                   $"CREATE USER {userName} FOR LOGIN {login}; " +
                                   $"GRANT SELECT ON dbo.Авторы TO {userName}; " +
                                   $"GRANT SELECT ON dbo.Книга TO {userName};";
                    try
                    {
                        using (SqlConnection connectionTRUSTABLE = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(query, connectionTRUSTABLE);
                            connectionTRUSTABLE.Open();
                            await command.ExecuteNonQueryAsync(cancellationToken);
                            EventLog.WriteEntry("SQLSlujba", "создал", EventLogEntryType.Information);
                        }
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("SQLSlujba", "Ошибка в создании записи.\n" + e.Message + e.StackTrace, EventLogEntryType.Error);

                       
                        string errorMessage = $"500|{e.Message}";
                        byte[] errorData = Encoding.UTF8.GetBytes(errorMessage);
                        await networkStream.WriteAsync(errorData, 0, errorData.Length, cancellationToken);
                    }

                    byte[] responseData = BitConverter.GetBytes(200);
                    await networkStream.WriteAsync(responseData, 0, responseData.Length, cancellationToken);
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine("finally:" + ex.Message);
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
