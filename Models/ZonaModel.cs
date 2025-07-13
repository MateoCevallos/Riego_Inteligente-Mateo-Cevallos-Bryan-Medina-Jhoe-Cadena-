using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class ZonaModel
    {
        public int ZonaId { get; set; }
        public string NombreZona { get; set; }
        public string Ubicacion { get; set; }
        public string TipoCultivo { get; set; }
        public decimal AreaM2 { get; set; }
        public int? ResponsableId { get; set; } // Nullable por si no hay responsable asignado
    }
}
