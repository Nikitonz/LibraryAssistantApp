using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Text.RegularExpressions;
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
        public static async Task ExecProcedure(string procname, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(procname, await ConnectionManager.Instance.OpenConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка выполнения процедуры {procname}: {e.Message}");
            }
        }
        /*public static async Task ExecProcedure(string procname, params object[] parameters) {
            try
            {
                if (procname.ToLower().StartsWith("exec"))
                    procname = "Execute " + procname;
                using (SqlCommand command = new SqlCommand(procname, await ConnectionManager.Instance.OpenConnection()))
                {

                    Regex regex = new Regex(@"@\w+");
                    MatchCollection matches = regex.Matches(procname);

                    if (matches.Count != parameters.Length)
                    {
                        throw new ArgumentException("Количество параметров не соответствует количеству имен параметров в запросе.");
                    }


                    for (int i = 0; i < parameters.Length; i++)
                    {
                        string paramName = matches[i].Value;
                        command.Parameters.AddWithValue(paramName, parameters[i]);
                    }
                    await command.ExecuteNonQueryAsync();
                }
                
            }
            catch (Exception e)
            {
               
                MessageBox.Show($"Ошибка выполнения процедуры {procname}: {e.Message}");
            }
        }*/

        public static async Task<int> ExecuteFunction(string functionName, params object[] parameters)
        {
            try
            {
                string commandText = $"select dbo.{functionName}(";
                for (int i = 0; i < parameters.Length; i++)
                {
                    commandText += $"'{parameters[i]}',";
                }
                commandText = commandText.TrimEnd(',') + ");";
                
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                {
                    
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                       
                        int result = Convert.ToInt32(await command.ExecuteScalarAsync());
                        
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в {functionName}: {ex.Message}");
                return -1;
            }
        }

        public static async Task<DataTable> GetDataTable(string procname, params object[] parameters)
        {
            try
            {
                if (procname.ToLower().StartsWith("exec"))
                    procname = "Execute " + procname;
                using (SqlCommand command = new SqlCommand(procname, await ConnectionManager.Instance.OpenConnection()))
                {
                    
                    Regex regex = new Regex(@"@\w+");
                    MatchCollection matches = regex.Matches(procname);

                    if (matches.Count != parameters.Length)
                    {
                        throw new ArgumentException("Количество параметров не соответствует количеству имен параметров в запросе.");
                    }

                   
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        string paramName = matches[i].Value;
                        command.Parameters.AddWithValue(paramName, parameters[i]);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в {procname}: {ex.Message}");
                return null;
            }
        }
        public static async Task<DataTable> GetDataTable(string procname, params SqlParameter[] parameters)
        {
            try
            {
                if (procname.ToLower().StartsWith("exec"))
                    procname = "Execute " + procname;
                using (SqlCommand command = new SqlCommand(procname, await ConnectionManager.Instance.OpenConnection()))
                {
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в {procname}: {ex.Message}");
                return null;
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


        public static async Task<List<string>> GetLinkedTableNames(string parentTableName, string columnName)
        {

            List<string> linkedTables = new List<string>();
            try
            {
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                {
                    SqlCommand command = new SqlCommand("GetDependentTableName", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ParentTableName", parentTableName);
                    command.Parameters.AddWithValue("@ColumnName", columnName);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string referencedTable = reader[0].ToString();
                        linkedTables.Add(referencedTable);
                    }

                    reader.Close();
                }
            }
            catch
            {
            }
            return linkedTables;
        }

        private static async Task<IEnumerable<string>> CheckTablePermissions(string tableName, SqlConnection connection)
        {
            //TODO
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

                if (true)
                    return new List<string> { "SELECT", "INSERT", "UPDATE", "DELETE", "ALTER", "CREATE", "DROP", "EXECUTE", "GRANT", "REFERENCES", "VIEW DEFINITION" };
              


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

        

        

       
    }
    
}
