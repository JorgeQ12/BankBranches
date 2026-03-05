namespace BankBranches.Domain.Interfaces;
public interface IGenericRepository
{
    /// <summary>
    /// Ejecuta un SP y retorna una lista de resultados.
    /// </summary>
    Task<IEnumerable<T>> GetProcedureAsync<T>(string procedureName, object? parameters = null, int? commandTimeout = null) where T : class;

    /// <summary>
    /// Ejecuta un SP y retorna un solo registro.
    /// </summary>
    Task<T?> GetProcedureSingleAsync<T>(string procedureName, object? parameters = null, int? commandTimeout = null) where T : class;

    /// <summary>
    /// Ejecuta un SP y retorna un valor escalar.
    /// </summary>
    Task<T> ExecuteScalarProcedureAsync<T>(string procedureName, object? parameters = null, int? commandTimeout = null);

    /// <summary>
    /// Ejecuta un SP sin retorno de datos.
    /// </summary>
    Task<int> ExecuteProcedureAsync(string procedureName, object? parameters = null, int? commandTimeout = null);
}
