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
            .ForMember(dest => dest.ServicioNombre, opt => opt.MapFrom(src => src.Servicio.Nombre))
            .ForMember(dest => dest.EmpleadoNombre, opt => opt.MapFrom(src => src.Empleado.Nombre_Completo))
            .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.Nombre_Completo));

        // 🔹 Servicio → ServicioDto
        CreateMap<Servicio, ServicioDTO>();

        // 🔹 Empleado → EmpleadoDto
        CreateMap<Empleado, EmpleadoDTO>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre_Completo));

        // 🔹 Usuario → UsuarioRegisterDto (para registro)
        CreateMap<Usuario, UsuarioRegisterDTO>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre_Completo))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));

        // 🔹 UsuarioLoginDto → Usuario (para login)
        CreateMap<UsuarioLoginDTO, Usuario>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password_Hash, opt => opt.MapFrom(src => src.Password));
    }
}
