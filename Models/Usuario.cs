using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.Models
{
    public class Usuario
    {
        public int Id_Usuarios { get; set; }
        public string Nombre_Completo { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Rol { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        public List<Turno> Turnos { get; set; }
    }

}
