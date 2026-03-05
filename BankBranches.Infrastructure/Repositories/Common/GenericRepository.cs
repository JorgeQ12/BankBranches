using System.Data;
using Dapper;
using BankBranches.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BankBranches.Infrastructure.Repositories.Common;
public class GenericRepository : IGenericRepository
{
    private readonly string _connectionString;

    public GenericRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no encontrada.");
    }

    /// <summary>
    /// Ejecutar un Stored Procedure que retorna una colección de resultados.
    /// </summary>
    public async Task<IEnumerable<T>> GetProcedureAsync<T>(string procedureName, object? parameters = null, int? commandTimeout = null) where T : class
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(
            procedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout
        );
    }

    /// <summary>
    /// Ejecutar un Stored Procedure que retorna un único registro o nulo.
    /// </summary>
    public async Task<T?> GetProcedureSingleAsync<T>(string procedureName, object? parameters = null, int? commandTimeout = null) where T : class
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(
            procedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout
        );
    }

    /// <summary>
    /// Ejecutar un Stored Procedure que retorna un valor escalar (ID, conteo, etc.).
    /// </summary>
    public async Task<T> ExecuteScalarProcedureAsync<T>(string procedureName, object? parameters = null, int? commandTimeout = null)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QuerySingleAsync<T>(
            procedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout
        );
    }

    /// <summary>
    /// Ejecutar un Stored Procedure sin retorno de datos (INSERT, UPDATE, DELETE).
    /// </summary>
    public async Task<int> ExecuteProcedureAsync(string procedureName, object? parameters = null, int? commandTimeout = null)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.ExecuteAsync(
            procedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout
        );
    }
}
