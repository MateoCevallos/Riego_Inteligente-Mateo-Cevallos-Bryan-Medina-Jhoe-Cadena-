using MySql.Data.MySqlClient;
using Riego_Inteligente.Config;
using Riego_Inteligente.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Riego_Inteligente.Controllers
{
    public class ZonaController
    {
        private readonly Conexion _conexion;

        public ZonaController()
        {
            _conexion = new Conexion();
        }

        // Insertar zona
        public string Insertar(ZonaModel zona)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = @"INSERT INTO zonas_riego 
                                         (nombre_zona, ubicacion, tipo_cultivo, area_m2, responsable_id) 
                                         VALUES (@nombre, @ubicacion, @cultivo, @area, @responsable)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", zona.NombreZona);
                        cmd.Parameters.AddWithValue("@ubicacion", zona.Ubicacion);
                        cmd.Parameters.AddWithValue("@cultivo", zona.TipoCultivo);
                        cmd.Parameters.AddWithValue("@area", zona.AreaM2);
                        cmd.Parameters.AddWithValue("@responsable", zona.ResponsableId);

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

        // Actualizar zona
        public string Actualizar(ZonaModel zona)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = @"UPDATE zonas_riego 
                                         SET nombre_zona = @nombre,
                                             ubicacion = @ubicacion,
                                             tipo_cultivo = @cultivo,
                                             area_m2 = @area,
                                             responsable_id = @responsable
                                         WHERE zona_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", zona.NombreZona);
                        cmd.Parameters.AddWithValue("@ubicacion", zona.Ubicacion);
                        cmd.Parameters.AddWithValue("@cultivo", zona.TipoCultivo);
                        cmd.Parameters.AddWithValue("@area", zona.AreaM2);
                        cmd.Parameters.AddWithValue("@responsable", zona.ResponsableId);
                        cmd.Parameters.AddWithValue("@id", zona.ZonaId);

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

        // Eliminar zona
        public string Eliminar(int zonaId)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = "DELETE FROM zonas_riego WHERE zona_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@id", zonaId);
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

        // Obtener todas las zonas
        public List<ZonaModel> Listar()
        {
            var lista = new List<ZonaModel>();

            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = "SELECT * FROM zonas_riego";
                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var zona = new ZonaModel
                                {
                                    ZonaId = Convert.ToInt32(dr["zona_id"]),
                                    NombreZona = dr["nombre_zona"].ToString(),
                                    Ubicacion = dr["ubicacion"].ToString(),
                                    TipoCultivo = dr["tipo_cultivo"].ToString(),
                                    AreaM2 = Convert.ToDecimal(dr["area_m2"]),
                                    ResponsableId = dr["responsable_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["responsable_id"])
                                };

                                lista.Add(zona);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Podrías registrar el error si quieres
            }

            return lista;
        }
    }
}

