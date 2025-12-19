using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITurnoRepository : IGenericRepository<Turno>
{
    Task<IEnumerable<Turno>> GetByEmpleadoAsync(int empleadoId);
    Task<IEnumerable<Turno>> GetByFechaAsync(DateTime fecha);
}
