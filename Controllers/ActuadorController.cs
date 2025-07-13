using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Models;

public class ActuadorController
{
    private string connectionString = "server=localhost;database=sistema_de_riego_inteligente;user=root;password=;";

    public List<ActuadorModel> ObtenerTodos()
    {
        List<ActuadorModel> lista = new List<ActuadorModel>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM actuadores";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new ActuadorModel
                    {
                        ActuadorId = reader.GetInt32("actuador_id"),
                        ZonaId = reader.GetInt32("zona_id"),
                        EstadoActual = reader.GetString("estado_actual"),
                        ModoOperacion = reader.GetString("modo_operacion"),
                        UltimaActivacion = reader.IsDBNull(reader.GetOrdinal("ultima_activacion"))
                                            ? (DateTime?)null
                                            : reader.GetDateTime("ultima_activacion"),
                        PinArduino = reader.GetInt32("pin_arduino"),
                        UltimaMantenimiento = reader.IsDBNull(reader.GetOrdinal("ultima_mantenimiento"))
                                                ? (DateTime?)null
                                                : reader.GetDateTime("ultima_mantenimiento")
                    });
                }
            }
        }

        return lista;
    }

    public void Insertar(ActuadorModel actuador)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO actuadores (actuador_id, zona_id, estado_actual, modo_operacion, ultima_activacion, pin_arduino, ultima_mantenimiento) 
                             VALUES (@id, @zona, @estado, @modo, @activacion, @pin, @mantenimiento)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", actuador.ActuadorId);
                cmd.Parameters.AddWithValue("@zona", actuador.ZonaId);
                cmd.Parameters.AddWithValue("@estado", actuador.EstadoActual);
                cmd.Parameters.AddWithValue("@modo", actuador.ModoOperacion);
                cmd.Parameters.AddWithValue("@activacion", actuador.UltimaActivacion);
                cmd.Parameters.AddWithValue("@pin", actuador.PinArduino);
                cmd.Parameters.AddWithValue("@mantenimiento", actuador.UltimaMantenimiento);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Actualizar(ActuadorModel actuador)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE actuadores 
                             SET zona_id = @zona, estado_actual = @estado, modo_operacion = @modo, 
                                 ultima_activacion = @activacion, pin_arduino = @pin, ultima_mantenimiento = @mantenimiento 
                             WHERE actuador_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", actuador.ActuadorId);
                cmd.Parameters.AddWithValue("@zona", actuador.ZonaId);
                cmd.Parameters.AddWithValue("@estado", actuador.EstadoActual);
                cmd.Parameters.AddWithValue("@modo", actuador.ModoOperacion);
                cmd.Parameters.AddWithValue("@activacion", actuador.UltimaActivacion);
                cmd.Parameters.AddWithValue("@pin", actuador.PinArduino);
                cmd.Parameters.AddWithValue("@mantenimiento", actuador.UltimaMantenimiento);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM actuadores WHERE actuador_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

