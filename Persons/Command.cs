using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
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
            _connection = new SqlCeConnection(_connectionString);           
            SqlCommand = new SqlCeCommand(queryString, _connection);
            SqlCommand.Transaction = _transaction;
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted); 
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        public SqlCeCommand SqlCommand { get; protected set; }

        public void Dispose()
        {
            _transaction.Commit(CommitMode.Deferred);
            _connection.Dispose();
        }
    }
}
