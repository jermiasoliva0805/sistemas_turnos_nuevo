using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sistemas_turnos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TurnosController : ControllerBase
{
    private readonly TurnoService _turnoService;
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;

    public TurnosController(TurnoService turnoService, IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _turnoService = turnoService;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    // ✅ Reservar turno
    [HttpPost("reservar")]
    public async Task<IActionResult> Reservar([FromBody] ReservarTurnoDTO dto)
    {
        try
        {
            //este if para convertir los string de dto a timespan
            if (!TimeSpan.TryParse(dto.HoraInicio, out var inicio) ||
            !TimeSpan.TryParse(dto.HoraFin, out var fin))
            {
                return BadRequest(new { message = "El formato de hora es inválido. Use 'HH:mm:ss'" });
            }

            // Validar que el usuario exista
            var usuarioExiste = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);
            if (usuarioExiste == null)
            {
                return BadRequest(new { message = "El usuario no existe en la base de datos" });
            }

            var turno = await _turnoService.ReservarTurno(
                dto.UsuarioId,
                dto.EmpleadoId,
                dto.ServicioId,
                dto.Fecha,
                inicio, // convertir a TimeSpan del dto
                fin
            );

            if (turno == null)
                return BadRequest(new { message = "El empleado ya tiene un turno en ese horario." });

            var turnoResponse = _mapper.Map<TurnoResponceDTO>(turno);
            return Ok(turnoResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error al reservar turno: " + ex.Message });
        }
    }

    // ✅ Cancelar turno
    [HttpDelete("cancelar/{id}")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var turno = await _turnoService.CancelarTurno(id);
        if (turno == null)
            return NotFound("Turno no encontrado.");

        var turnoResponse = _mapper.Map<TurnoResponceDTO>(turno);
        return Ok(turnoResponse);
    }

    // ✅ Listar turnos por empleado
    [HttpGet("empleado/{id}")]
    public async Task<IActionResult> GetPorEmpleado(int id)
    {
        var turnos = await _turnoService.ObtenerTurnosPorEmpleado(id);
        var turnosDto = _mapper.Map<List<TurnoResponceDTO>>(turnos);
        return Ok(turnosDto);
    }

    // ✅ Listar turnos por fecha
    [HttpGet("fecha/{fecha}")]
    public async Task<IActionResult> GetPorFecha(DateTime fecha)
    {
        var turnos = await _turnoService.ObtenerTurnosPorFecha(fecha);
        var turnosDto = _mapper.Map<List<TurnoResponceDTO>>(turnos);
        return Ok(turnosDto);
    }
}