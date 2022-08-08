using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseHelper
{
	public class SqlDatabase : IDisposable
	{
		protected bool _useSingleton;
		protected SqlConnection _connection;
		protected SqlTransaction _transaction;

		#region Constructors
		private SqlDatabase(bool useSingleton = false)
		{
			_useSingleton = useSingleton;
		}

		public SqlDatabase(string connectionString, bool useSingleton = false) : this(useSingleton)
		{
			ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		}
		#endregion

		public event Action<SqlConnection> TransactionBegin;
		public event Action<SqlConnection> TransactionCommit;
		public event Action<SqlConnection> TransactionRollback;

		public string ConnectionString { get; private set; }

		public SqlConnection GetConnection()
		{
			if (_connection == default || !_useSingleton)
			{
				_connection = new(ConnectionString);
			}

			return _connection;
		}

		public SqlCommand GetCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
		{
			SqlCommand command = new()
			{
				Connection = GetConnection(),
				CommandText = commandText,
				CommandType = commandType
			};
			if (parameters != null)
			{
				command.Parameters.AddRange(parameters);
			}
			if (_transaction != default)
			{
				command.Transaction = _transaction;
			}
			return command;
		}

		public SqlCommand GetCommand(string commandText, params SqlParameter[] parameters)
		{
			return GetCommand(commandText, CommandType.Text, parameters);
		}

		public void BeginTransaction()
		{
			if (!_useSingleton)
			{
				throw new Exception("Transaction is supported only in singleton mode.");
			}

			GetConnection().Open();
			_transaction = GetConnection().BeginTransaction();
			TransactionBegin?.Invoke(_connection);
		}

		public void CommitTransaction()
		{
			if (!_useSingleton || _transaction == default)
			{
				throw new Exception("Transaction is supported only in singleton mode and with active transaction.");
			}

			_transaction.Commit();
			TransactionCommit?.Invoke(_connection);
		}

		public void RollbackTransaction()
		{
			if (!_useSingleton || _transaction == default)
			{
				throw new Exception("Transaction is supported only in singleton mode and with active transaction.");
			}

			_transaction.Rollback();
			TransactionRollback?.Invoke(_connection);
		}

		public SqlDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
		{
			var command = GetCommand(commandText, commandType, parameters);
			if (command.Connection.State != ConnectionState.Open)
			{
				command.Connection.Open();
			}
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}

		public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
		{
			return ExecuteReader(commandText, CommandType.Text, parameters);
		}

		public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
		{
			var command = GetCommand(commandText, commandType, parameters);
			try
			{
				if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				return command.ExecuteNonQuery();
			}
			finally
			{
				if (!_useSingleton && _transaction == default)
				{
					command.Connection.Close();
				}
			}
		}

		public int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
		{
			return ExecuteNonQuery(commandText, CommandType.Text, parameters);
		}

		public T ExecuteScalar<T>(string commandText, CommandType commandType, params SqlParameter[] parameters)
		{
			var command = GetCommand(commandText, commandType, parameters);
			try
			{
				if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				return (T)Convert.ChangeType(command.ExecuteScalar(), typeof(T));
			}
			finally
			{
				command.Connection.Close();
			}
		}

		public T ExecuteScalar<T>(string commandText, params SqlParameter[] parameters)
		{
			return ExecuteScalar<T>(commandText, CommandType.Text, parameters);
		}

		public DataTable GetDataTable(string commandText, CommandType commandType, params SqlParameter[] parameters)
		{
			DataTable table = new();
			var command = GetCommand(commandText, commandType, parameters);
			try
			{
				if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				table.Load(command.ExecuteReader());
				return table;
			}
			finally
			{
				if (!_useSingleton && _transaction == default)
				{
					command.Connection.Close();
				}
			}
		}

		public DataTable GetDataTable(string commandText, params SqlParameter[] parameters)
		{
			return GetDataTable(commandText, CommandType.Text, parameters);
		}

		public void Dispose()
		{
			if (_connection != default && _connection.State == ConnectionState.Open)
			{
				_connection.Close();
			}
			GC.SuppressFinalize(this);
		}
	}
}