using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class ZonaRiegoModel
    {
        public int ZonaId { get; set; } // ID de la zona
        public string NombreZona { get; set; } // Nombre descriptivo de la zona
        public string Ubicacion { get; set; } // Sector o lugar dentro del campo
        public string TipoCultivo { get; set; } // Tipo de cultivo (rosas, tulipanes, etc.)
        public decimal AreaM2 { get; set; } // Área en metros cuadrados
        public int? ResponsableId { get; set; } // Usuario responsable (puede ser null)
    }
}
