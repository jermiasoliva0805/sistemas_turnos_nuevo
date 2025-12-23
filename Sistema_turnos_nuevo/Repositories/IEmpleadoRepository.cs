using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEmpleadoRepository : IGenericRepository<Empleado>
{
    Task<Empleado?> GetWithHorariosAsync(int id);
}
