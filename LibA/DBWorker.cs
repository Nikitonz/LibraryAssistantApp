﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    internal class DBWorker
    {
        private static DataTable oldTable;
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

        public static async Task<DataTable> GetDataTable(string tableName)
        {
            string commandText = $"SELECT * FROM [{tableName}]";
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
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());

            }
            return null;

        }
        public static async Task BeginTransaction(DataGridView dgv) {
            DataTable dataTable = dgv.DataSource as DataTable;
            oldTable = dataTable;
            await BeginTransaction(dataTable);
        }

        public static async Task BeginTransaction(DataTable dt)
        {
            
            
            if (dt != null)
            {
                try
                {
                    using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
                    {

                        IEnumerable<string> userPermissions = await CheckTablePermissions(dt.TableName, connection);

                        if (!userPermissions.Any())
                        {
                            MessageBox.Show("У вас нет прав на выполнение операций в данной таблице.");

                            throw new Exception();
                        }

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {dt.TableName}", connection))
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
                    }
                }
                catch (Exception ex)
                {
                    
                 
                    MessageBox.Show($"Ошибка при обновлении базы данных: {ex.Message}");
                    throw ex;
                }
            }
        }




        public static async Task<DataTable> RollbackTransaction()
        {
            try
            {
               await BeginTransaction(oldTable);
               

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при транзакции: {ex.Message}");

            }
            return oldTable;
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

            string query = $@"
        SELECT dp.permission_name
        FROM sys.database_permissions dp
        JOIN sys.database_principals dpn ON dp.grantee_principal_id = dpn.principal_id
        WHERE dpn.name = (SELECT dp.name AS DatabaseUserName
        FROM sys.database_principals dp
        JOIN sys.server_principals sp ON dp.sid = sp.sid
        WHERE sp.name = '{currentUsername}') AND dp.major_id = OBJECT_ID('{tableName}')";

            List<string> userPermissions = new List<string>();

            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            string permission = reader.GetString(0);
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
