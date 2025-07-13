using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class AlertasModel
    {
        public int AlertaId { get; set; } // ID de la alerta
        public int ZonaId { get; set; } // Zona afectada
        public string TipoAlerta { get; set; } // Ej: sensor_falla, rango_temperatura
        public string Mensaje { get; set; } // Descripción (desencriptada si aplica)
        public DateTime FechaHora { get; set; } // Fecha y hora de la alerta
        public string Estado { get; set; } // pendiente, resuelta o en_proceso
        public string UsuarioAsignado { get; set; } // Usuario responsable
    }
}
