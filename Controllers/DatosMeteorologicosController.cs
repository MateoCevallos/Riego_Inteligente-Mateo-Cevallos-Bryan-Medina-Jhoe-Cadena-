using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Models;

public class DatosMeteorologicosController
{
    private string connectionString = "server=localhost;database=sistema_de_riego_inteligente;user=root;password=;";

    public List<DatosMeteorologicosModel> ObtenerTodos()
    {
        List<DatosMeteorologicosModel> lista = new List<DatosMeteorologicosModel>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM datos_meteorologicos";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new DatosMeteorologicosModel
                    {
                        RegistroClimaId = reader.GetInt32("registro_clima_id"),
                        FechaHora = reader.GetDateTime("fecha_hora"),
                        Temperatura = reader.IsDBNull(reader.GetOrdinal("temperatura"))
                                      ? (decimal?)null : reader.GetDecimal("temperatura"),
                        HumedadAmbiente = reader.IsDBNull(reader.GetOrdinal("humedad_ambiente"))
                                          ? (decimal?)null : reader.GetDecimal("humedad_ambiente"),
                        Precipitacion = reader.IsDBNull(reader.GetOrdinal("precipitacion"))
                                        ? (decimal?)null : reader.GetDecimal("precipitacion"),
                        ZonaId = reader.GetInt32("zona_id"),
                        FuenteDato = reader["fuente_dato"] != DBNull.Value
                                     ? reader.GetString("fuente_dato")
                                     : null
                    });
                }
            }
        }

        return lista;
    }

    public void Insertar(DatosMeteorologicosModel datos)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO datos_meteorologicos 
                            (fecha_hora, temperatura, humedad_ambiente, precipitacion, zona_id, fuente_dato)
                             VALUES (@fecha, @temp, @humedad, @lluvia, @zona, @fuente)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@fecha", datos.FechaHora);
                cmd.Parameters.AddWithValue("@temp", datos.Temperatura);
                cmd.Parameters.AddWithValue("@humedad", datos.HumedadAmbiente);
                cmd.Parameters.AddWithValue("@lluvia", datos.Precipitacion);
                cmd.Parameters.AddWithValue("@zona", datos.ZonaId);
                cmd.Parameters.AddWithValue("@fuente", datos.FuenteDato);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Actualizar(DatosMeteorologicosModel datos)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE datos_meteorologicos SET 
                                fecha_hora = @fecha,
                                temperatura = @temp,
                                humedad_ambiente = @humedad,
                                precipitacion = @lluvia,
                                zona_id = @zona,
                                fuente_dato = @fuente
                             WHERE registro_clima_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@fecha", datos.FechaHora);
                cmd.Parameters.AddWithValue("@temp", datos.Temperatura);
                cmd.Parameters.AddWithValue("@humedad", datos.HumedadAmbiente);
                cmd.Parameters.AddWithValue("@lluvia", datos.Precipitacion);
                cmd.Parameters.AddWithValue("@zona", datos.ZonaId);
                cmd.Parameters.AddWithValue("@fuente", datos.FuenteDato);
                cmd.Parameters.AddWithValue("@id", datos.RegistroClimaId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM datos_meteorologicos WHERE registro_clima_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
