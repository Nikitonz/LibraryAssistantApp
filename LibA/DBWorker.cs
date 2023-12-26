using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibA
{
    internal class DBWorker
    {
        private const string INITAL_CATALOG = "Библиотека";
        private static string DEFAULT_CONNECTION = $"Data Source={Properties.Settings.Default.dbConnStrMain};Initial Catalog={INITAL_CATALOG};Integrated Security=True";

        public static async Task<string[]> BdGetDataMSSQL(string commandText)
        {
            List<string> result = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DEFAULT_CONNECTION))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }
                    connection.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return result.ToArray();
        }

        public static async Task<DataTable> GetDataTable(string commandText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DEFAULT_CONNECTION))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {

                            DataTable dataTable = new();
                            adapter.Fill(dataTable);
                            return dataTable;

                        }
                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return null;

        }

        public static async Task<DataTable> Transactable(Func<Task> operation)
        {
            using (SqlConnection connection = new SqlConnection(DEFAULT_CONNECTION))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await operation();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return new DataTable();
        }
    }
}
