using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.DTOs
{
    internal class UsuarioRegisterDTO
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [StringLength(50, ErrorMessage = "La contraseña no puede exceder 50 caracteres")]
        public string Password { get; set; }

        [RegularExpression("^(admin|cliente)$", ErrorMessage = "Rol inválido. Valores permitidos: admin, cliente")]
        public string Rol { get; set; } = "cliente";
    }
}
