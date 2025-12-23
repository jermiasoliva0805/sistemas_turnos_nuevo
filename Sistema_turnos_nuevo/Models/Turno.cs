using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemas_turnos.Models
{
    public class Turno
    {
        [Key] // Indica que es la llave primaria
        [Column("id_turno")] // 👈 Úsalo si en la DB se llama id_central
        public int Id_Turno { get; set; }

        [ForeignKey("Usuario")]
        public int Id_Usuarios { get; set; }

        [ForeignKey("Empleado")]
        public int Id_Empleados { get; set; }

        [ForeignKey("Servicio")]
        public int Id_Servicios { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Fin { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
        public Empleado Empleado { get; set; }
        public Servicio Servicio { get; set; }
    }
}