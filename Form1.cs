using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Config;
using Riego_Inteligente.Views.Manager;

namespace Riego_Inteligente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtContrasenia.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor ingrese su usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Conexion conexion = new Conexion();

                using (var cn = conexion.AbrirConexion(2)) // 2 = MySQL
                {
                    // Primero validamos el password
                    string query = "SELECT VerificarPassword(@usuario, @clave);";
                    using (var cmd = new MySqlCommand(query, (MySqlConnection)cn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@clave", clave);

                        int resultado = Convert.ToInt32(cmd.ExecuteScalar());

                        if (resultado == 1)
                        {
                            // Se obtiene el rol
                            string rolQuery = "SELECT rol FROM usuarios WHERE nombre_usuario = @usuario LIMIT 1;";
                            using (var cmdRol = new MySqlCommand(rolQuery, (MySqlConnection)cn))
                            {
                                cmdRol.Parameters.AddWithValue("@usuario", usuario);
                                string rol = cmdRol.ExecuteScalar()?.ToString();

                                if (!string.IsNullOrEmpty(rol))
                                {
                                    MessageBox.Show("¡Bienvenido al sistema!", "Acceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Abrimos el menú principal y le pasamos usuario + rol
                                    Views.Manager.FRMMenuPrincipal menu = new Views.Manager.FRMMenuPrincipal(usuario, rol);
                                    menu.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Error al obtener el rol del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
