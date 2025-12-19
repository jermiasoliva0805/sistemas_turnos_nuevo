using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class TurnoService
{
    private readonly AppDbContext _context;

    public TurnoService(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Validar solapamiento
    public async Task<bool> TieneSolapamiento(int empleadoId, DateTime fecha, TimeSpan inicio, TimeSpan fin)
    {
        return await _context.Turnos.AnyAsync(t =>
            t.Id_Empleados == empleadoId &&
            t.Fecha.Date == fecha.Date &&
            (
                (inicio >= t.Hora_Inicio && inicio < t.Hora_Fin) ||
                (fin > t.Hora_Inicio && fin <= t.Hora_Fin) ||
                (inicio <= t.Hora_Inicio && fin >= t.Hora_Fin)
            )
        );
    }

    // ✅ Reservar turno
    public async Task<string> ReservarTurno(int usuarioId, int empleadoId, int servicioId, DateTime fecha, TimeSpan inicio, TimeSpan fin)
    {
        if (await TieneSolapamiento(empleadoId, fecha, inicio, fin))
        {
            return "El empleado ya tiene un turno en ese horario.";
        }

        var turno = new Turno
        {
            Id_Usuarios = usuarioId,
            Id_Empleados = empleadoId,
            Id_Servicios = servicioId,
            Fecha = fecha,
            Hora_Inicio = inicio,
            Hora_Fin = fin,
            Estado = "Reservado",
            Fecha_Creacion = DateTime.Now
        };

        _context.Turnos.Add(turno);
        await _context.SaveChangesAsync();

        return "Turno reservado correctamente.";
    }

    // ✅ Cancelar turno
    public async Task<string> CancelarTurno(int turnoId)
    {
        var turno = await _context.Turnos.FindAsync(turnoId);
        if (turno == null)
        {
            return "Turno no encontrado.";
        }

        turno.Estado = "Cancelado";
        await _context.SaveChangesAsync();

        return "Turno cancelado correctamente.";
    }

    // ✅ Listar turnos por empleado
    public async Task<List<Turno>> ObtenerTurnosPorEmpleado(int empleadoId)
    {
        return await _context.Turnos
            .Include(t => t.Servicio)
            .Include(t => t.Usuario)
            .Where(t => t.Id_Empleados == empleadoId)
            .ToListAsync();
    }

    // ✅ Listar turnos por fecha
    public async Task<List<Turno>> ObtenerTurnosPorFecha(DateTime fecha)
    {
        return await _context.Turnos
            .Include(t => t.Servicio)
            .Include(t => t.Empleado)
            .Include(t => t.Usuario)
            .Where(t => t.Fecha.Date == fecha.Date)
            .ToListAsync();
    }
}
