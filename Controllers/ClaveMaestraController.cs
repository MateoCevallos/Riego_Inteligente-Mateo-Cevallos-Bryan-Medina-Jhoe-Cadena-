using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Models;

public class ClaveMaestraController
{
    private string connectionString = "server=localhost;database=sistema_de_riego_inteligente;user=root;password=;";

    public List<ClaveMaestraModel> ObtenerTodas()
    {
        List<ClaveMaestraModel> claves = new List<ClaveMaestraModel>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM claves_maestras";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    claves.Add(new ClaveMaestraModel
                    {
                        ClaveId = reader.GetInt32("clave_id"),
                        Nombre = reader["nombre"] != DBNull.Value ? reader.GetString("nombre") : null,
                        Clave = reader["clave"] != DBNull.Value ? (byte[])reader["clave"] : null,
                        FechaAlta = reader.GetDateTime("fecha_alta"),
                        Activa = Convert.ToBoolean(reader["activa"])
                    });
                }
            }
        }

        return claves;
    }

    public ClaveMaestraModel ObtenerClaveActiva()
    {
        ClaveMaestraModel clave = null;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM claves_maestras WHERE activa = 1 LIMIT 1";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    clave = new ClaveMaestraModel
                    {
                        ClaveId = reader.GetInt32("clave_id"),
                        Nombre = reader["nombre"] != DBNull.Value ? reader.GetString("nombre") : null,
                        Clave = reader["clave"] != DBNull.Value ? (byte[])reader["clave"] : null,
                        FechaAlta = reader.GetDateTime("fecha_alta"),
                        Activa = Convert.ToBoolean(reader["activa"])
                    };
                }
            }
        }

        return clave;
    }

    public void Insertar(ClaveMaestraModel nuevaClave)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO claves_maestras (clave_id, nombre, clave, fecha_alta, activa)
                             VALUES (@id, @nombre, @clave, @fecha, @activa)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", nuevaClave.ClaveId);
                cmd.Parameters.AddWithValue("@nombre", nuevaClave.Nombre);
                cmd.Parameters.AddWithValue("@clave", nuevaClave.Clave);
                cmd.Parameters.AddWithValue("@fecha", nuevaClave.FechaAlta);
                cmd.Parameters.AddWithValue("@activa", nuevaClave.Activa);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Actualizar(ClaveMaestraModel clave)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE claves_maestras SET 
                                nombre = @nombre,
                                clave = @clave,
                                fecha_alta = @fecha,
                                activa = @activa
                             WHERE clave_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", clave.ClaveId);
                cmd.Parameters.AddWithValue("@nombre", clave.Nombre);
                cmd.Parameters.AddWithValue("@clave", clave.Clave);
                cmd.Parameters.AddWithValue("@fecha", clave.FechaAlta);
                cmd.Parameters.AddWithValue("@activa", clave.Activa);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM claves_maestras WHERE clave_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void ActivarClave(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();

            // Desactiva todas
            new MySqlCommand("UPDATE claves_maestras SET activa = 0", conn).ExecuteNonQuery();

            // Activa la seleccionada
            string activar = "UPDATE claves_maestras SET activa = 1 WHERE clave_id = @id";
            using (MySqlCommand cmd = new MySqlCommand(activar, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

