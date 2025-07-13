using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Config;
using Riego_Inteligente.Models;

namespace Riego_Inteligente.Controllers
{
    public class RiegoController
    {
        private readonly Conexion _conexion;

        public RiegoController()
        {
            _conexion = new Conexion();
        }

        // Insertar nuevo registro de riego
        public string Insertar(RiegoModel riego)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = @"INSERT INTO historial_riego 
                        (zona_id, fecha_hora_inicio, duracion_real, motivo_activacion, usuario_modificacion)
                        VALUES (@zona, @inicio, @duracion, @motivo, @usuario)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@zona", riego.ZonaId);
                        cmd.Parameters.AddWithValue("@inicio", riego.FechaHoraInicio);
                        cmd.Parameters.AddWithValue("@duracion", riego.DuracionReal);
                        cmd.Parameters.AddWithValue("@motivo", riego.MotivoActivacion);
                        cmd.Parameters.AddWithValue("@usuario", riego.UsuarioModificacion);

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0 ? "ok" : "error";
                    }
                }
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        // Actualizar fecha de fin del riego
        public string FinalizarRiego(int registroId, DateTime fechaFin)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = @"UPDATE historial_riego 
                                         SET fecha_hora_fin = @fin,
                                             duracion_real = TIMESTAMPDIFF(MINUTE, fecha_hora_inicio, @fin)
                                         WHERE registro_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@fin", fechaFin);
                        cmd.Parameters.AddWithValue("@id", registroId);

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0 ? "ok" : "error";
                    }
                }
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        // Listar historial de riego
        public List<RiegoModel> Listar()
        {
            var lista = new List<RiegoModel>();

            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = "SELECT * FROM historial_riego ORDER BY fecha_hora_inicio DESC";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var riego = new RiegoModel
                                {
                                    RegistroId = Convert.ToInt32(dr["registro_id"]),
                                    ZonaId = Convert.ToInt32(dr["zona_id"]),
                                    FechaHoraInicio = Convert.ToDateTime(dr["fecha_hora_inicio"]),
                                    FechaHoraFin = dr["fecha_hora_fin"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_hora_fin"]) : (DateTime?)null,
                                    DuracionReal = dr["duracion_real"] != DBNull.Value ? Convert.ToInt32(dr["duracion_real"]) : (int?)null,
                                    MotivoActivacion = dr["motivo_activacion"].ToString(),
                                    UsuarioModificacion = dr["usuario_modificacion"]?.ToString()
                                };

                                lista.Add(riego);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Puedes loggear el error si deseas
            }

            return lista;
        }
    }
}
