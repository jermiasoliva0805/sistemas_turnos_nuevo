using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.DTOs
{
    internal class TurnoResponceDTO
    {
        public int Id_Turno { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Datos del servicio
        public string ServicioNombre { get; set; }
        public decimal ServicioPrecio { get; set; }
        public int ServicioDuracion { get; set; }

        // Datos del empleado
        public string EmpleadoNombre { get; set; }
        public string EmpleadoEmail { get; set; }

        // Datos del usuario
        public string UsuarioNombre { get; set; }
        public string UsuarioEmail { get; set; }
    }
}
