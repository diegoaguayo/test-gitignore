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
            CreateMap<Faena, FaenaDTO>().ReverseMap();
            CreateMap<FaenaCreacionDTO, Faena>();
            CreateMap<Especialidad, EspecialidadDTO>().ReverseMap();
            CreateMap<EspecialidadCreacionDTO, Especialidad>();
            CreateMap<Trabajador, TrabajadorDTO>().ReverseMap();
            CreateMap<TrabajadorCreacionDTO, Trabajador>();
            CreateMap<Inasistencia, InasistenciaDTO>().ReverseMap();
            CreateMap<InasistenciaCreacionDTO, Inasistencia>();


        }
    }
}
