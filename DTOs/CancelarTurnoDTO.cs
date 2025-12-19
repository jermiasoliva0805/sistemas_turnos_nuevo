using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.DTOs
{
    internal class CancelarTurnoDTO
    {
        [Required(ErrorMessage = "El ID del turno es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor a 0")]
        public int TurnoId { get; set; }
    }
}
