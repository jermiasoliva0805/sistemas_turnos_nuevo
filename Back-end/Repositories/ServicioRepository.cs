using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ServicioRepository : GenericRepository<Servicio>, IServicioRepository
{
    public ServicioRepository(AppDbContext context) : base(context) { }
}
