namespace BankBranches.Domain.Common;
public sealed class RequestResult<T>
{
    /// Indica si la operación se completó de manera exitosa.
    public bool IsSuccess { get; private set; }
    /// Contiene el valor de retorno en caso de una operación exitosa.
    public T? Value { get; private set; }
    /// Describe el error ocurrido en caso de que la operación falle.
    public string? Error { get; private set; }
    /// Representa el código de estado HTTP asociado al resultado de la operación.
    public int StatusCode { get; private set; }

    /// <summary>
    /// Constructor privado
    /// </summary>
    /// <param name="isSuccess"></param>
    /// <param name="value"></param>
    /// <param name="error"></param>
    /// <param name="statusCode"></param>
    private RequestResult(bool isSuccess, T? value, string? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Generar un resultado de éxito con el valor proporcionado.
    /// </summary>
    public static RequestResult<T> Success(T value, int statusCode = 200) => new(true, value, null, statusCode);
    
    /// <summary>
    /// Generar un resultado de fallo con el mensaje de error especificado.
    /// </summary>
    public static RequestResult<T> Failure(string error, int statusCode = 400) => new(false, default, error, statusCode);
    
    /// <summary>
    /// Generar un resultado que indica que el recurso solicitado no fue encontrado.
    /// </summary>
    public static RequestResult<T> NotFound(string error = "Recurso no encontrado") => new(false, default, error, 404);
    
    /// <summary>
    /// Generar un resultado que indica falta de autorización para la operación.
    /// </summary>
    public static RequestResult<T> Unauthorized(string error = "No autorizado") => new(false, default, error, 401);
}
