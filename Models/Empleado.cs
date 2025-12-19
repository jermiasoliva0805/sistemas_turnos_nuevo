using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sistemas_turnos.Models
{
  public class Empleado
{
    public int Id_Empleados { get; set; }
    public string Nombre_Completo { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    public List<HorarioEmpleado> Horarios { get; set; }
    public List<Turno> Turnos { get; set; }
}
}
