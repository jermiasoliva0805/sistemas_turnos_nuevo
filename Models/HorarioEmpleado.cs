using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.Models
{
    public class HorarioEmpleado
    {
        public int Id_Horario { get; set; }
        public int Id_Empleados { get; set; }
        public string Dia_Semana { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Fin { get; set; }

        public Empleado Empleado { get; set; }
    }

}
