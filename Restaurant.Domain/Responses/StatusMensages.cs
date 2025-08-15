namespace Restaurant.Domain.Responses;

public static class StatusMensages
{

    private static readonly Dictionary<int, string> _messages = new Dictionary<int, string>
    {
        { 200, "La solicitud se ha completado con éxito." },
        { 201, "El recurso ha sido creado correctamente." },
        { 202, "La solicitud ha sido aceptada para procesamiento, pero el procesamiento no ha sido completado." },
        { 400, "La solicitud contiene sintaxis incorrecta." },
        { 401, "No autorizado." },
        { 403, "Prohibido." },
        { 404, "No se encontró el recurso solicitado." },
        { 405, "Método no permitido." },
        { 409, "Error, hay fechas duplicadas en la lista" },
        { 415, "Tipo de medio no soportado." },
        { 422, "Entidad no procesable." },
        { 500, "Error interno del servidor." },
        { 503, "Servicio no disponible." },
        { 504, "Tiempo de espera de la puerta de enlace agotado." },
        { 547, "No se puede eliminar el recurso porque está referenciado ." }


    };

    public static string GetMessage(int statusCode)
    {
        return _messages.TryGetValue(statusCode, out var message) ? message : "Código de estado desconocido.";
    }
}