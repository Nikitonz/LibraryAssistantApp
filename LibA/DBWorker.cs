using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibA
{
    internal class DBWorker
    {
        private const string INITAL_CATALOG = "Библиотека";
        private static string DEFAULT_CONNECTION = $"Data Source={Properties.Settings.Default.dbConnStrMain};Initial Catalog={INITAL_CATALOG};Integrated Security=True";

        public static async Task<Object> BdGetDataMSSQL(string commandText)
        {
            List<String> result = new List<String>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DEFAULT_CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }
                    connection.Close();
                    return result.ToArray();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return 0;
            }
            

        }

       
    }
}
