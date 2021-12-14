using AutoMapper;
using saam_webapi.DTOs;
using saam_webapi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            
            CreateMap<Lugar, LugarDTO>().ReverseMap();
            CreateMap<Tipocontrato, TipocontratoDTO>().ReverseMap();
            CreateMap<Faena, FaenaDTO>().ReverseMap();
            CreateMap<Especialidad, EspecialidadDTO>().ReverseMap();
            CreateMap<Maximoturno, MaximoturnoDTO>().ReverseMap();
            CreateMap<Lista, ListaDTO>().ReverseMap();
            CreateMap<Cartola, CartolaDTO>().ReverseMap();
            CreateMap<Trabajador, TrabajadorDTO>().ReverseMap();
            CreateMap<Inasistencia, InasistenciaDTO>().ReverseMap();

        }
    }
}
