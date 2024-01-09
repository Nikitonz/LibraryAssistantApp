using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    internal class DBWorker
    {
        private static DataTable oldTable;
        public static DataTable OldTable
        {
            set { oldTable = value; }
            get { return oldTable!; }
        }



        public static async Task<string[]> BdGetDataMSSQL(string commandText)
        {
            List<string> result = new List<string>();
            try
            {
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                {

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
                throw;
            }
            return result.ToArray();
        }
        public static DataTable GetDataTable(SqlCommand command) {
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                DataTable dataTable = new();
                adapter.Fill(dataTable);
                return dataTable;

            }

        }
        public static async Task<DataTable> GetDataTable(string tableName)
        {
            string commandText = null;
            if (tableName.ToLower().StartsWith("select"))
                commandText = tableName;
            
            
            else
                commandText = $"SELECT * FROM [{tableName}]";
            try
            {


                using (SqlCommand command = new SqlCommand(commandText, await ConnectionManager.Instance.OpenConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        DataTable dataTable = new(tableName);
                        adapter.Fill(dataTable);
                        return dataTable;

                    }
                }



            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
            return null;

        }

        public static async Task ExecProcedure(string procname, params SqlParameter[] parameters) {
            try
            {
                using (SqlCommand command = new SqlCommand(procname, await ConnectionManager.Instance.OpenConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    await command.ExecuteNonQueryAsync();
                }
                MessageBox.Show($"Выполнение процедуры {procname} успешно");
            }
            catch (Exception e)
            {
               
                MessageBox.Show($"Ошибка выполнения процедуры {procname}: {e.Message}");
            }
        }

        public static async Task BeginTransaction(DataGridView dgv)
        {
            DataTable dataTable = dgv.DataSource as DataTable;
            oldTable = dataTable;
            await BeginTransaction(dataTable);
        }

        public static async Task BeginTransaction(DataTable dt)
        {
            using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
            {
                IEnumerable<string> userPermissions = await CheckTablePermissions(dt.TableName, connection);

                if (!userPermissions.Except(new[] { "SELECT" }, StringComparer.OrdinalIgnoreCase).Any())
                {

                    MessageBox.Show("У вас нет прав на выполнение операций в данной таблице.");

                    throw new Exception();
                }

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    if (dt != null)
                    {
                        try
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM [{dt.TableName}]", connection))
                            {
                                adapter.SelectCommand.Transaction = transaction;

                                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                                if (userPermissions.Contains("INSERT", StringComparer.OrdinalIgnoreCase))
                                    adapter.InsertCommand = commandBuilder.GetInsertCommand();

                                if (userPermissions.Contains("UPDATE", StringComparer.OrdinalIgnoreCase))
                                    adapter.UpdateCommand = commandBuilder.GetUpdateCommand();

                                if (userPermissions.Contains("DELETE", StringComparer.OrdinalIgnoreCase))
                                    adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                                adapter.Update(dt);

                                transaction.Commit();
                            }
                        }
                        catch
                        {
                            transaction.Rollback();

                            //MessageBox.Show($"Ошибка при обновлении базы данных");
                            throw;
                        }
                    }
                }

            }
        }




        public static async Task RollbackTransaction()
        {
            try
            {
                await BeginTransaction(oldTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при транзакции: {ex.Message}");
            }

        }




        private static async Task<IEnumerable<string>> CheckTablePermissions(string tableName, SqlConnection connection)
        {
            var builder = new SqlConnectionStringBuilder(connection.ConnectionString);
            string currentUsername = builder.UserID;

            if (string.IsNullOrEmpty(currentUsername))
            {
                MessageBox.Show("Не удалось определить текущего пользователя.");
                return Enumerable.Empty<string>();
            }

            List<string> userPermissions = new List<string>();

            try
            {

                if (await CheckUserRights(connection))
                    return new List<string> { "SELECT", "INSERT", "UPDATE", "DELETE", "ALTER", "CREATE", "DROP", "EXECUTE", "GRANT", "REFERENCES", "VIEW DEFINITION", /* и другие операции */ };
              


                string getPermissionsQuery = "EXEC GetUserTablePermissions @DatabaseUserName, @TableName";
                using (SqlCommand permissionsCommand = new SqlCommand(getPermissionsQuery, connection))
                {
                    permissionsCommand.Parameters.AddWithValue("@DatabaseUserName", currentUsername);
                    permissionsCommand.Parameters.AddWithValue("@TableName", tableName);

                    using (SqlDataReader permissionsReader = await permissionsCommand.ExecuteReaderAsync())
                    {
                        while (permissionsReader.Read())
                        {
                            string permission = permissionsReader.GetString(0);
                            userPermissions.Add(permission);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке прав пользователя: {ex.Message}");
                return Enumerable.Empty<string>();
            }

            return userPermissions;
        }

        public static string FindMatchingTableName(string columnName, List<string> tableNames, string selectedTableName)
        {
            string matchingTableName = string.Empty;
            double maxSimilarity = 0;

            foreach (string tableName in tableNames)
            {
                // Проверяем, что текущая таблица не совпадает с выбранной таблицей
                if (!tableName.Equals(selectedTableName, StringComparison.OrdinalIgnoreCase))
                {
                    double similarity = CalculateSimilarity(columnName, tableName.ToLower());
                    if (similarity > maxSimilarity)
                    {
                        maxSimilarity = similarity;
                        matchingTableName = tableName;
                    }
                }
            }

            return matchingTableName;
        }

        private static int CalculateLevenshteinDistance(string str1, string str2)
        {
            int[,] matrix = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 0; i <= str1.Length; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j <= str2.Length; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost
                    );
                }
            }

            return matrix[str1.Length, str2.Length];
        }

        private static double CalculateSimilarity(string str1, string str2)
        {
            int distance = CalculateLevenshteinDistance(str1, str2);
            int maxLength = Math.Max(str1.Length, str2.Length);
            if (maxLength == 0)
            {
                return 1.0;  // Специальный случай: две пустые строки считаются полностью схожими
            }
            else
            {
                return 1.0 - (double)distance / maxLength;
            }
        }

        public static async Task<bool> CheckUserRights(SqlConnection connection)
        {
            var builder = new SqlConnectionStringBuilder(connection.ConnectionString);
            string userID = builder.UserID;
            string getRolesQuery = "EXEC GetUserRoles @LoginName";

            using (SqlCommand rolesCommand = new SqlCommand(getRolesQuery, connection))
            {
                rolesCommand.Parameters.AddWithValue("@LoginName", userID);

                using (SqlDataReader rolesReader = await rolesCommand.ExecuteReaderAsync())
                {
                    while (rolesReader.Read())
                    {
                        string roleName = rolesReader.GetString(0);

                      
                        if (Enum.TryParse<DatabaseRoles>(roleName, out DatabaseRoles role))
                        {
                          
                            if (role != DatabaseRoles.None)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
        }

        public enum DatabaseRoles
        {
            None,
            db_securityadmin,
            db_owner,
            db_denydatawriter,
            db_denydatareader,
            db_ddladmin,
            db_datawriter,
            db_datareader,
            db_backupoperator,
            db_accessadmin,
            db_executor,
            db_owner_sid,
            db_securityadmin_sid,
            db_accessadmin_sid,
            db_backupoperator_sid,
            db_ddladmin_sid,
            db_datareader_sid,
            db_datawriter_sid,
            db_denydatareader_sid
        }
    }
    
}
