using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class SensorModel
    {
        public int SensorId { get; set; }
        public string TipoSensor { get; set; }
        public int ZonaId { get; set; }
        public decimal? ValorActual { get; set; }
        public string Unidad { get; set; }
        public DateTime? FechaLectura { get; set; }
        public string Estado { get; set; }
    }
}
