using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class RiegoModel
    {
        public int RegistroId { get; set; }
        public int ZonaId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; }
        public int? DuracionReal { get; set; }
        public string MotivoActivacion { get; set; } // "programado", "manual", "automatico_por_sensor"
        public string UsuarioModificacion { get; set; }
    }
}
