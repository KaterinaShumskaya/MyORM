namespace Persons.DataAccessors.DBUtils
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlServerCe;

    public class Command : IDisposable
    {
        
        private static string _connectionString = ConfigurationManager.ConnectionStrings["MainDB"].ToString();

        private SqlCeConnection _connection;

        private SqlCeTransaction _transaction;

        public Command(string queryString)
        {
            this._connection = new SqlCeConnection(_connectionString);           
            this.SqlCommand = new SqlCeCommand(queryString, this._connection);
            this.SqlCommand.Transaction = this._transaction;
            try
            {
                this._connection.Open();
                this._transaction = this._connection.BeginTransaction(IsolationLevel.ReadCommitted); 
            }
            catch (Exception ex)
            {
                if (this._transaction != null)
                {
                    this._transaction.Rollback();
                }
                Console.WriteLine(ex.Message);
            }
        }

        public SqlCeCommand SqlCommand { get; protected set; }

        public void Dispose()
        {
            this._transaction.Commit(CommitMode.Deferred);
            this._connection.Dispose();
        }
    }
}
