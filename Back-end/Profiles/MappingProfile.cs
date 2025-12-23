using AutoMapper;
using sistemas_turnos.DTOs;
using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sistemas_turnos.DTOs;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 🔹 Turno → TurnoResponseDto
        CreateMap<Turno, TurnoResponceDTO>()
            .ForMember(dest => dest.Id_Turno, opt => opt.MapFrom(src => src.Id_Turno))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
            .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.Hora_Inicio))
            .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => src.Hora_Fin))
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Fecha_Creacion))
            .ForMember(dest => dest.ServicioNombre, opt => opt.MapFrom(src => src.Servicio.Nombre))
            .ForMember(dest => dest.ServicioPrecio, opt => opt.MapFrom(src => src.Servicio.Precio))
            .ForMember(dest => dest.ServicioDuracion, opt => opt.MapFrom(src => src.Servicio.Duracion_Minutos))
            .ForMember(dest => dest.EmpleadoNombre, opt => opt.MapFrom(src => src.Empleado.Nombre_Completo))
            .ForMember(dest => dest.EmpleadoEmail, opt => opt.MapFrom(src => src.Empleado.Email))
            .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.Nombre_Completo))
            .ForMember(dest => dest.UsuarioEmail, opt => opt.MapFrom(src => src.Usuario.Email));

        // 🔹 Servicio ↔ ServicioDto
        CreateMap<Servicio, ServicioDTO>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.Duracion_Minutos, opt => opt.MapFrom(src => src.Duracion_Minutos))
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
            .ReverseMap();

        // 🔹 Empleado ↔ EmpleadoDto
        CreateMap<Empleado, EmpleadoDTO>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre_Completo))
            .ReverseMap()
            .ForMember(src => src.Nombre_Completo, opt => opt.MapFrom(dest => dest.NombreCompleto));

        // 🔹 Usuario → Usuario dto para login/register
        CreateMap<Usuario, UsuarioRegisterDTO>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre_Completo))
            .ReverseMap()
            .ForMember(src => src.Nombre_Completo, opt => opt.MapFrom(dest => dest.NombreCompleto));

        CreateMap<UsuarioLoginDTO, Usuario>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password_Hash, opt => opt.MapFrom(src => src.Password));
    }
}
