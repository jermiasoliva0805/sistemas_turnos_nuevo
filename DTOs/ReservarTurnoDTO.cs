using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.DTOs
{
    internal class ReservarTurnoDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El empleado es obligatorio")]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "El servicio es obligatorio")]
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria")]
        public TimeSpan HoraInicio { get; set; }
    }
}
