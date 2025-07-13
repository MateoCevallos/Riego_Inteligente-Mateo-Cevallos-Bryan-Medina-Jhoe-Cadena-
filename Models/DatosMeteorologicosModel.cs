using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class DatosMeteorologicosModel
    {
        public int RegistroClimaId { get; set; } // ID del registro meteorológico
        public DateTime FechaHora { get; set; } // Fecha y hora de la medición
        public decimal? Temperatura { get; set; } // Temperatura en °C
        public decimal? HumedadAmbiente { get; set; } // Humedad relativa %
        public decimal? Precipitacion { get; set; } // Lluvia en mm
        public int ZonaId { get; set; } // Zona a la que pertenece el dato
        public string FuenteDato { get; set; } // Sensor o API de origen
    }
}
