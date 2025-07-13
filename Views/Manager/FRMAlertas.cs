using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Riego_Inteligente.Views.Manager
{
    public partial class FRMAlertas : Form
    {
        public string UsuarioActivo { get; set; }
        public string RolActivo { get; set; }

        public FRMAlertas()
        {
            InitializeComponent();
            dgvAlertas.CellClick += dgvAlertas_CellClick;
        }

        private void FRMAlertas_Load(object sender, EventArgs e)
        {
            // Cargar datos iniciales
            CargarZonasRiego();
            CargarTiposAlerta();
            CargarEstados();
            CargarUsuarios();

            // Establecer fecha actual automáticamente
            dtpFechaHora.Value = DateTime.Now;
            dtpFechaHora.Enabled = false;

            // Asegurar que el ComboBox no permita escribir manualmente
            cmbUsuarioAsignado.DropDownStyle = ComboBoxStyle.DropDownList;

            // Asignar automáticamente el usuario activo, si está en la lista
            if (!string.IsNullOrEmpty(UsuarioActivo) && cmbUsuarioAsignado.Items.Contains(UsuarioActivo))
            {
                cmbUsuarioAsignado.SelectedItem = UsuarioActivo;
            }

            // Solo permitir modificar el combo si el usuario es admin
            cmbUsuarioAsignado.Enabled = (RolActivo == "admin");

            // Cargar alertas en el DataGridView
            CargarAlertas();
        }


        private void CargarZonasRiego()
        {
            string cadena = "server=localhost; database=sistema_de_riego_inteligente; uid=root; pwd=;";
            using (MySqlConnection conn = new MySqlConnection(cadena))
            {
                conn.Open();
                string query = "SELECT zona_id, nombre_zona FROM zonas_riego";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbZonaRiego.DataSource = dt;
                    cmbZonaRiego.DisplayMember = "nombre_zona";
                    cmbZonaRiego.ValueMember = "zona_id";
                }
                conn.Close();
            }
        }

        private void CargarTiposAlerta()
        {
            cmbTipoAlerta.Items.Clear();
            cmbTipoAlerta.Items.AddRange(new string[]
            {
                "sensor_falla", "rango_humedad", "lluvia_detectada", "rango_temperatura", "sensor_mantenimiento"
            });
            cmbTipoAlerta.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new string[]
            {
                "pendiente", "en_proceso", "resuelta"
            });
            cmbEstado.SelectedIndex = 0;
        }

        private void CargarUsuarios()
        {
            cmbUsuarioAsignado.Items.Clear();
            string cadena = "server=localhost; database=sistema_de_riego_inteligente; uid=root; pwd=;";
            using (MySqlConnection conn = new MySqlConnection(cadena))
            {
                conn.Open();
                string query = "SELECT nombre_usuario FROM usuarios";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        cmbUsuarioAsignado.Items.Add(reader.GetString(0));
                }
                conn.Close();
            }
            if (!string.IsNullOrEmpty(UsuarioActivo) && cmbUsuarioAsignado.Items.Contains(UsuarioActivo))
            {
                cmbUsuarioAsignado.SelectedItem = UsuarioActivo;
            }
            //if (cmbUsuarioAsignado.Items.Contains(UsuarioActivo))
            //cmbUsuarioAsignado.SelectedItem = UsuarioActivo;
        }

        private void CargarAlertas()
        {
            string cadena = "server=localhost; database=sistema_de_riego_inteligente; uid=root; pwd=;";
            using (MySqlConnection conn = new MySqlConnection(cadena))
            {
                conn.Open();
                string query = @"SELECT 
                                    alerta_id, 
                                    zona_id, 
                                    tipo_alerta, 
                                    CONVERT(mensaje USING utf8) AS mensaje, 
                                    fecha_hora, 
                                    estado, 
                                    usuario_asignado 
                                 FROM alertas 
                                 ORDER BY fecha_hora DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAlertas.DataSource = dt;
                }

                conn.Close();
            }
        }

        private void dgvAlertas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAlertas.Rows[e.RowIndex];
                cmbZonaRiego.SelectedValue = row.Cells["zona_id"].Value;
                cmbTipoAlerta.Text = row.Cells["tipo_alerta"].Value.ToString();
                txtMensaje.Text = row.Cells["mensaje"].Value.ToString();
                cmbEstado.Text = row.Cells["estado"].Value.ToString();
                cmbUsuarioAsignado.Text = row.Cells["usuario_asignado"].Value.ToString();
            }
        }
        private void btnAgregarAlerta_Click(object sender, EventArgs e)
        {
            try
            {
                int zonaID = Convert.ToInt32(cmbZonaRiego.SelectedValue);
                string tipo = cmbTipoAlerta.Text;
                string mensaje = txtMensaje.Text.Trim();
                DateTime fecha = DateTime.Now;
                string estado = cmbEstado.Text;
                string usuario = cmbUsuarioAsignado.Text;

                string cadena = "server=localhost; database=sistema_de_riego_inteligente; uid=root; pwd=;";
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO alertas (zona_id, tipo_alerta, mensaje, fecha_hora, estado, usuario_asignado)
                                     VALUES (@zona, @tipo, @mensaje, @fecha, @estado, @usuario)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@zona", zonaID);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@mensaje", mensaje);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("✅ Alerta agregada correctamente.");
                    CargarAlertas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al agregar alerta:\n" + ex.Message);
            }
        }

        private void btnEditarAlerta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAlertas.CurrentRow == null || dgvAlertas.CurrentRow.Cells["alerta_id"].Value == null)
                {
                    MessageBox.Show("⚠️ Por favor, selecciona una alerta para editar.");
                    return;
                }

                int alertaID = Convert.ToInt32(dgvAlertas.CurrentRow.Cells["alerta_id"].Value);
                string nuevoMensaje = txtMensaje.Text.Trim();
                string nuevoEstado = cmbEstado.Text;

                string cadena = "server=localhost; database=sistema_de_riego_inteligente; uid=root; pwd=;";
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                    string query = @"UPDATE alertas 
                                     SET mensaje = @mensaje, estado = @estado 
                                     WHERE alerta_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@mensaje", nuevoMensaje);
                        cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", alertaID);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("✅ Alerta actualizada correctamente.");
                    CargarAlertas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al editar alerta:\n" + ex.Message);
            }
        }

        private void btnEliminarAlerta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAlertas.CurrentRow == null || dgvAlertas.CurrentRow.Cells["alerta_id"].Value == null)
                {
                    MessageBox.Show("⚠️ Por favor, selecciona una alerta para eliminar.");
                    return;
                }

                int alertaID = Convert.ToInt32(dgvAlertas.CurrentRow.Cells["alerta_id"].Value);

                DialogResult resultado = MessageBox.Show("¿Estás seguro de eliminar esta alerta?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.No)
                    return;

                string cadena = "server=localhost; database=sistema_de_riego_inteligente; uid=root; pwd=;";
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                    string query = "DELETE FROM alertas WHERE alerta_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", alertaID);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("🗑️ Alerta eliminada correctamente.");
                    CargarAlertas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al eliminar alerta:\n" + ex.Message);
            }
        }

        private void btnSalirAlerta_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
