using Microsoft.EntityFrameworkCore;
using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class TurnoRepository : GenericRepository<Turno>, ITurnoRepository
{
    public TurnoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Turno>> GetByEmpleadoAsync(int empleadoId)
    {
        return await _dbSet
            .Include(t => t.Servicio)
            .Include(t => t.Usuario)
            .Where(t => t.Id_Empleados == empleadoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Turno>> GetByFechaAsync(DateTime fecha)
    {
        return await _dbSet
            .Include(t => t.Servicio)
            .Include(t => t.Empleado)
            .Include(t => t.Usuario)
            .Where(t => t.Fecha.Date == fecha.Date)
            .ToListAsync();
    }
}
