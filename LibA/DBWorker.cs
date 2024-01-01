using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibA
{
    internal class DBWorker
    {
       
        public static async Task<string[]> BdGetDataMSSQL(string commandText)
        {
            List<string> result = new List<string>();
            try
            {
                using (SqlConnection connection = await ConnectionManager.Instance.OpenConnection())
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

       /*
        public static async void BeginTransaction(DataGridView dgv)
        {
            DataTable dataTable = dgv.DataSource as DataTable;

            if (dataTable != null)
            {
                
                

                try
                {
                   
                    transaction = .BeginTransaction();

                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {dataTable.TableName}", await ConnectionManager.Instance.OpenConnection()))
                    {
                        adapter.SelectCommand.Transaction = transaction;

                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                       
                        adapter.RowUpdated += (sender, e) =>
                        {
                            if (e.StatementType == StatementType.Insert || e.StatementType == StatementType.Update || e.StatementType == StatementType.Delete)
                            {
                                transaction.Commit();
                                MessageBox.Show("Изменения сохранены в базе данных.");
                            }
                        };

                      
                        adapter.RowUpdating += (sender, e) =>
                        {
                            if (e.StatementType == StatementType.Insert || e.StatementType == StatementType.Update || e.StatementType == StatementType.Delete)
                            {
                                if (e.Errors != null)
                                {
                                    e.Status = UpdateStatus.SkipCurrentRow;
                                    MessageBox.Show($"Произошла ошибка при обновлении строки: {e.Errors.Message}");
                                    transaction.Rollback();
                                }
                            }
                        };

                        adapter.Update(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    MessageBox.Show($"Произошла ошибка при сохранении изменений: {ex.Message}");
                }
                finally
                {
                    if (await ConnectionManager.Instance.OpenConnection().State == ConnectionState.Open)
                    {
                        .Close();
                    }
                }
            }
        }

        public static async void RollbackTransaction()
        {
            try
            {
               
                    await .OpenAsync();
                    transaction = .BeginTransaction();
                    if (transaction != null)
                    {
                        transaction.Commit();
                        MessageBox.Show("Изменения сохранены. Используйте кнопку отката изменений, чтобы откатить изменения");
                    }


                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при транзакции: {ex.Message}");
            }
        }

        */

    }
}
