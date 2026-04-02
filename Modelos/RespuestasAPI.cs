using System.Net;

namespace InmobiliariaMinimalAPI.Modelos;

public class RespuestasAPI
{
    public RespuestasAPI()
    {
        Errores = [];
    }
    public bool Success { get; set; }
    public Object Resultado { get; set; }
    public HttpStatusCode CodigoDeEstado { get; set; }
    public List<string> Errores { get; set; } = [];
}
