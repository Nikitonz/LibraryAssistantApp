using System;

using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    internal class DBO
    {
        private static string connectionString = "Data Source=NIKNOTEBOOK;Initial Catalog=Библиотека;User ID=librarian;Password=123333;";

        public static string ConnectionString {
            get { return connectionString; }
        }


        public static void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }
                connection.Close();
            }
        }

        public static void CheckConn() {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();



        }
    }
}
