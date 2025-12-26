using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.DTOs
{
    public class EmpleadoDTO
    {
        public int Id_Empleados { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string NombreCompleto { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        [StringLength(20)]
        public string Telefono { get; set; }
    }
}
