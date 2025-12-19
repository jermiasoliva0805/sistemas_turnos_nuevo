using Microsoft.EntityFrameworkCore;
using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
{
    public EmpleadoRepository(AppDbContext context) : base(context) { }

    public async Task<Empleado?> GetWithHorariosAsync(int id)
    {
        return await _dbSet
            .Include(e => e.Horarios)
            .FirstOrDefaultAsync(e => e.Id_Empleados == id);
    }
}
