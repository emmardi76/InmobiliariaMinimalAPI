using AutoMapper;
using InmobiliariaMinimalAPI.Modelos;
using InmobiliariaMinimalAPI.Modelos.DTOS;

namespace InmobiliariaMinimalAPI.Mapper;


public class ConfiguracionDeMapper: Profile
{
    public ConfiguracionDeMapper()
    {
        CreateMap<Propiedad, CrearPropiedadDTO>().ReverseMap();
        CreateMap<Propiedad, PropiedadDTO>().ReverseMap();
    }
}
