using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class ClaveMaestraModel
    {
        public int ClaveId { get; set; } // ID de la clave
        public string Nombre { get; set; } // Nombre descriptivo de la clave
        public byte[] Clave { get; set; } // Valor binario de la clave
        public DateTime FechaAlta { get; set; } // Fecha de creación
        public bool Activa { get; set; } // Si la clave está activa o no
    }
}
