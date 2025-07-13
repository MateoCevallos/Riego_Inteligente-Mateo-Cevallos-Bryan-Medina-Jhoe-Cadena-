using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riego_Inteligente.Models
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
    }
}
