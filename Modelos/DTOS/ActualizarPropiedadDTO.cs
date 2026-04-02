namespace InmobiliariaMinimalAPI.Modelos.DTOS;
public class ActualizarPropiedadDTO
{
    public int IdPropiedad { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Ubicacion { get; set; }
    public bool Activa { get; set; }
}
