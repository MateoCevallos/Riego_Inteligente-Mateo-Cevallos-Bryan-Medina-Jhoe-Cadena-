using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class ActuadorModel
    {
        public int ActuadorId { get; set; } // Clave primaria
        public int ZonaId { get; set; } // Zona a la que pertenece el actuador
        public string EstadoActual { get; set; } // 'abierto' o 'cerrado'
        public string ModoOperacion { get; set; } // 'manual' o 'automatico'
        public DateTime? UltimaActivacion { get; set; } // Última vez que se activó
        public int PinArduino { get; set; } // Pin físico en Arduino
        public DateTime? UltimaMantenimiento { get; set; } // Fecha del último mantenimiento
    }
}
