using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.Models
{
    public class Turno
    {
        public int Id_Turno { get; set; }
        public int Id_Usuarios { get; set; }
        public int Id_Empleados { get; set; }
        public int Id_Servicios { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Fin { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        public Usuario Usuario { get; set; }
        public Empleado Empleado { get; set; }
        public Servicio Servicio { get; set; }
    }

}
