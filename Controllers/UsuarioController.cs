using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Models;

public class UsuarioController
{
    private string connectionString = "server=localhost;database=sistema_de_riego_inteligente;user=root;password=;";

    public List<UsuarioModel> ObtenerTodos()
    {
        List<UsuarioModel> usuarios = new List<UsuarioModel>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "SELECT * FROM usuarios";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuarios.Add(new UsuarioModel
                    {
                        UsuarioId = reader.GetInt32("usuario_id"),
                        NombreUsuario = reader.GetString("nombre_usuario"),
                        Rol = reader.GetString("rol"),
                        Email = System.Text.Encoding.UTF8.GetString((byte[])reader["email"]),
                        UltimaConexion = reader.IsDBNull(reader.GetOrdinal("ultima_conexion"))
                                         ? (DateTime?)null
                                         : reader.GetDateTime("ultima_conexion"),
                        Salt = (byte[])reader["salt"],
                        Hash = (byte[])reader["hash"]
                    });
                }
            }
        }

        return usuarios;
    }

    public void CrearUsuario(UsuarioModel usuario, string contrasenaPlano)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("CrearUsuarioSeguro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_nombre", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@p_rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@p_pass_plain", contrasenaPlano);
                cmd.Parameters.AddWithValue("@p_email", usuario.Email);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public bool VerificarLogin(string nombreUsuario, string contrasenaPlano)
    {
        bool valido = false;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("SELECT VerificarPassword(@p_user, @p_pass)", conn))
            {
                cmd.Parameters.AddWithValue("@p_user", nombreUsuario);
                cmd.Parameters.AddWithValue("@p_pass", contrasenaPlano);

                valido = Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        return valido;
    }

    public void ActualizarConexion(string nombreUsuario)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "UPDATE usuarios SET ultima_conexion = NOW() WHERE nombre_usuario = @nombre";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void EliminarUsuario(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "DELETE FROM usuarios WHERE usuario_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
