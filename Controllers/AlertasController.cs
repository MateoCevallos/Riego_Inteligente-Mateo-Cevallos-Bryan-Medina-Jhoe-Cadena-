using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Models;

public class AlertasController
{
    private string connectionString = "server=localhost;database=sistema_de_riego_inteligente;user=root;password=;";

    public List<AlertasModel> ObtenerTodas()
    {
        List<AlertasModel> lista = new List<AlertasModel>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM alertas";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new AlertasModel
                    {
                        AlertaId = reader.GetInt32("alerta_id"),
                        ZonaId = reader.GetInt32("zona_id"),
                        TipoAlerta = reader.GetString("tipo_alerta"),
                        Mensaje = reader["mensaje"] != DBNull.Value
                                  ? Encoding.UTF8.GetString((byte[])reader["mensaje"])
                                  : string.Empty,
                        FechaHora = reader.GetDateTime("fecha_hora"),
                        Estado = reader.GetString("estado"),
                        UsuarioAsignado = reader["usuario_asignado"] != DBNull.Value
                                          ? reader.GetString("usuario_asignado")
                                          : null
                    });
                }
            }
        }

        return lista;
    }

    public void Insertar(AlertasModel alerta)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO alertas (zona_id, tipo_alerta, mensaje, fecha_hora, estado, usuario_asignado)
                             VALUES (@zona_id, @tipo_alerta, @mensaje, @fecha_hora, @estado, @usuario_asignado)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@zona_id", alerta.ZonaId);
                cmd.Parameters.AddWithValue("@tipo_alerta", alerta.TipoAlerta);
                cmd.Parameters.AddWithValue("@mensaje", Encoding.UTF8.GetBytes(alerta.Mensaje));
                cmd.Parameters.AddWithValue("@fecha_hora", alerta.FechaHora);
                cmd.Parameters.AddWithValue("@estado", alerta.Estado);
                cmd.Parameters.AddWithValue("@usuario_asignado", alerta.UsuarioAsignado);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void ActualizarEstado(int alertaId, string nuevoEstado)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE alertas SET estado = @estado WHERE alerta_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@id", alertaId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM alertas WHERE alerta_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
