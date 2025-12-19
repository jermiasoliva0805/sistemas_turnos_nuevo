using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemas_turnos.Models
{
    public class Servicio
    {
        public int Id_Servicios { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion_Minutos { get; set; }
        public decimal Precio { get; set; }

    }
}