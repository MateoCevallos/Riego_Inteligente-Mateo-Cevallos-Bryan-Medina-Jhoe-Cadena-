using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Models;

public class ZonaRiegoController
{
    private string connectionString = "server=localhost;database=sistema_de_riego_inteligente;user=root;password=;";

    public List<ZonaRiegoModel> ObtenerTodas()
    {
        List<ZonaRiegoModel> lista = new List<ZonaRiegoModel>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM zonas_riego";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new ZonaRiegoModel
                    {
                        ZonaId = reader.GetInt32("zona_id"),
                        NombreZona = reader.GetString("nombre_zona"),
                        Ubicacion = reader.GetString("ubicacion"),
                        TipoCultivo = reader["tipo_cultivo"] != DBNull.Value
                                      ? reader.GetString("tipo_cultivo")
                                      : null,
                        AreaM2 = reader.GetDecimal("area_m2"),
                        ResponsableId = reader["responsable_id"] != DBNull.Value
                                        ? reader.GetInt32("responsable_id")
                                        : (int?)null
                    });
                }
            }
        }

        return lista;
    }

    public void Insertar(ZonaRiegoModel zona)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO zonas_riego 
                            (zona_id, nombre_zona, ubicacion, tipo_cultivo, area_m2, responsable_id)
                             VALUES (@id, @nombre, @ubicacion, @cultivo, @area, @responsable)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", zona.ZonaId);
                cmd.Parameters.AddWithValue("@nombre", zona.NombreZona);
                cmd.Parameters.AddWithValue("@ubicacion", zona.Ubicacion);
                cmd.Parameters.AddWithValue("@cultivo", zona.TipoCultivo);
                cmd.Parameters.AddWithValue("@area", zona.AreaM2);
                cmd.Parameters.AddWithValue("@responsable", zona.ResponsableId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Actualizar(ZonaRiegoModel zona)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE zonas_riego SET 
                                nombre_zona = @nombre,
                                ubicacion = @ubicacion,
                                tipo_cultivo = @cultivo,
                                area_m2 = @area,
                                responsable_id = @responsable
                             WHERE zona_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", zona.ZonaId);
                cmd.Parameters.AddWithValue("@nombre", zona.NombreZona);
                cmd.Parameters.AddWithValue("@ubicacion", zona.Ubicacion);
                cmd.Parameters.AddWithValue("@cultivo", zona.TipoCultivo);
                cmd.Parameters.AddWithValue("@area", zona.AreaM2);
                cmd.Parameters.AddWithValue("@responsable", zona.ResponsableId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM zonas_riego WHERE zona_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
