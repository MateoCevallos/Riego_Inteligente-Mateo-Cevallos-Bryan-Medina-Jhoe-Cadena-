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
    public class SensorController
    {
        private readonly Conexion _conexion;

        public SensorController()
        {
            _conexion = new Conexion();
        }

        // Insertar sensor
        public string Insertar(SensorModel sensor)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = @"INSERT INTO sensores 
                        (tipo_sensor, zona_id, valor_actual, unidad, fecha_lectura, estado)
                        VALUES (@tipo, @zona, @valor, @unidad, @fecha, @estado)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@tipo", sensor.TipoSensor);
                        cmd.Parameters.AddWithValue("@zona", sensor.ZonaId);
                        cmd.Parameters.AddWithValue("@valor", sensor.ValorActual);
                        cmd.Parameters.AddWithValue("@unidad", sensor.Unidad);
                        cmd.Parameters.AddWithValue("@fecha", sensor.FechaLectura);
                        cmd.Parameters.AddWithValue("@estado", sensor.Estado);

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

        // Actualizar sensor
        public string Actualizar(SensorModel sensor)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = @"UPDATE sensores SET
                        tipo_sensor = @tipo,
                        zona_id = @zona,
                        valor_actual = @valor,
                        unidad = @unidad,
                        fecha_lectura = @fecha,
                        estado = @estado
                        WHERE sensor_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@tipo", sensor.TipoSensor);
                        cmd.Parameters.AddWithValue("@zona", sensor.ZonaId);
                        cmd.Parameters.AddWithValue("@valor", sensor.ValorActual);
                        cmd.Parameters.AddWithValue("@unidad", sensor.Unidad);
                        cmd.Parameters.AddWithValue("@fecha", sensor.FechaLectura);
                        cmd.Parameters.AddWithValue("@estado", sensor.Estado);
                        cmd.Parameters.AddWithValue("@id", sensor.SensorId);

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

        // Eliminar sensor
        public string Eliminar(int sensorId)
        {
            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = "DELETE FROM sensores WHERE sensor_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@id", sensorId);
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

        // Listar todos los sensores
        public List<SensorModel> Listar()
        {
            var lista = new List<SensorModel>();

            try
            {
                using (MySqlConnection cn = (MySqlConnection)_conexion.AbrirConexion(2))
                {
                    const string sql = "SELECT * FROM sensores";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var sensor = new SensorModel
                                {
                                    SensorId = Convert.ToInt32(dr["sensor_id"]),
                                    TipoSensor = dr["tipo_sensor"].ToString(),
                                    ZonaId = Convert.ToInt32(dr["zona_id"]),
                                    ValorActual = dr["valor_actual"] != DBNull.Value ? Convert.ToDecimal(dr["valor_actual"]) : (decimal?)null,
                                    Unidad = dr["unidad"]?.ToString(),
                                    FechaLectura = dr["fecha_lectura"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_lectura"]) : (DateTime?)null,
                                    Estado = dr["estado"].ToString()
                                };

                                lista.Add(sensor);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Puedes registrar el error si lo deseas
            }

            return lista;
        }
    }
}
