using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TutorDesk.Infrastructure.Data
{
    public class DbSession : IAsyncDisposable, IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction? _transaction;
        private bool _disposed;

        public DbSession(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

            _connection = new SqlConnection(connectionString);
        }

        // -------------------------------------------------------------------------
        // Connection / Transaction
        // -------------------------------------------------------------------------

        public async Task OpenAsync(CancellationToken ct = default)
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync(ct);
        }

        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            await OpenAsync(ct);
            _transaction = (SqlTransaction)await _connection.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            if (_transaction is null)
                throw new InvalidOperationException("No active transaction to commit.");

            await _transaction.CommitAsync(ct);
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_transaction is null)
                throw new InvalidOperationException("No active transaction to roll back.");

            await _transaction.RollbackAsync(ct);
        }

        // -------------------------------------------------------------------------
        // Command factory
        // -------------------------------------------------------------------------

        public SqlCommand CreateCommand(string text, CommandType type = CommandType.StoredProcedure)
        {
            return new SqlCommand(text, _connection, _transaction)
            {
                CommandType = type
            };
        }

        // -------------------------------------------------------------------------
        // Execute — returns DataTable (SELECT / multi-row results)
        // -------------------------------------------------------------------------

        public async Task<DataTable> ExecuteAsync(SqlCommand cmd, CancellationToken ct = default)
        {
            await OpenAsync(ct);

            using var reader = await cmd.ExecuteReaderAsync(ct);
            var table = new DataTable();
            table.Load(reader);
            return table;
        }

        // -------------------------------------------------------------------------
        // Execute — returns number of rows affected (INSERT / UPDATE / DELETE)
        // -------------------------------------------------------------------------

        public async Task<int> ExecuteNonQueryAsync(SqlCommand cmd, CancellationToken ct = default)
        {
            await OpenAsync(ct);
            return await cmd.ExecuteNonQueryAsync(ct);
        }

        // -------------------------------------------------------------------------
        // Execute — returns single scalar value
        // -------------------------------------------------------------------------

        public async Task<T?> ExecuteScalarAsync<T>(SqlCommand cmd, CancellationToken ct = default)
        {
            await OpenAsync(ct);
            var result = await cmd.ExecuteScalarAsync(ct);

            if (result is null || result == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(result, typeof(T));
        }

        // -------------------------------------------------------------------------
        // Execute — maps rows to strongly typed objects
        // -------------------------------------------------------------------------

        public async Task<IReadOnlyList<T>> ExecuteAsync<T>(
            SqlCommand cmd,
            Func<IDataRecord, T> map,
            CancellationToken ct = default)
        {
            await OpenAsync(ct);

            var results = new List<T>();
            using var reader = await cmd.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
                results.Add(map(reader));

            return results;
        }

        // -------------------------------------------------------------------------
        // Convenience: run a unit of work inside a transaction automatically
        // -------------------------------------------------------------------------

        public async Task ExecuteInTransactionAsync(
            Func<Task> work,
            CancellationToken ct = default)
        {
            await BeginTransactionAsync(ct);
            try
            {
                await work();
                await CommitAsync(ct);
            }
            catch
            {
                await RollbackAsync(ct);
                throw;
            }
        }

        public async Task<T> ExecuteInTransactionAsync<T>(
            Func<Task<T>> work,
            CancellationToken ct = default)
        {
            await BeginTransactionAsync(ct);
            try
            {
                var result = await work();
                await CommitAsync(ct);
                return result;
            }
            catch
            {
                await RollbackAsync(ct);
                throw;
            }
        }

        // -------------------------------------------------------------------------
        // Disposal
        // -------------------------------------------------------------------------

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            _disposed = true;

            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            await _connection.DisposeAsync();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _transaction?.Dispose();
            _connection.Dispose();
        }
    }
}