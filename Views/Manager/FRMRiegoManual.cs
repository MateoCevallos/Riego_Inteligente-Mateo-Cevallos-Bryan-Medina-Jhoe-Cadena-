using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Config;


namespace Riego_Inteligente.Views.Manager
{
    public partial class FRMRiegoManual : Form
    {
        public string UsuarioActivo { get; set; }
        public FRMRiegoManual()
        {
            InitializeComponent();
        }
        private void FRMRiegoManual_Load_1(object sender, EventArgs e)
        {
            lblUsuarioActual.Text = UsuarioActivo;
            CargarZonas();
            cmbZonaRiego.SelectedIndex = 0;
            cmbZonaRiego_SelectedIndexChanged(null, null);
            CargarHistorial();

            // Eventos para actualizar automáticamente
            cmbZonaRiego.SelectedIndexChanged += cmbZonaRiego_SelectedIndexChanged;
            nudDuracionMinutos.ValueChanged += ValidarCampos;
            cmbZonaRiego.SelectedIndexChanged += ValidarCampos;

            btnActivarRiego.Enabled = false;
        }

        private void CargarZonas()
        {
            using (MySqlConnection conn = (MySqlConnection)new Conexion().AbrirConexion(2))
            {
                string query = "SELECT zona_id, nombre_zona FROM zonas_riego";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn); ;
                DataTable dt = new DataTable();
                da.Fill(dt);
                //MessageBox.Show(dt.Rows.Count.ToString());
                cmbZonaRiego.DataSource = dt;
                cmbZonaRiego.DisplayMember = "nombre_zona";
                cmbZonaRiego.ValueMember = "zona_id";

                if (cmbZonaRiego.Items.Count > 0)
                {
                    cmbZonaRiego.SelectedIndex = 0;
                    cmbZonaRiego_SelectedIndexChanged(null, null); // Esto también actualiza el área automáticamente
                }
            }
        }

        private void cmbZonaRiego_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZonaRiego.SelectedValue != null)
            {
                int zonaId = Convert.ToInt32(cmbZonaRiego.SelectedValue);
                using (MySqlConnection conn = (MySqlConnection)new Conexion().AbrirConexion(2))
                {
                    string query = "SELECT area_m2 FROM zonas_riego WHERE zona_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", zonaId);
                    object result = cmd.ExecuteScalar();
                    txtArea_M2.Text = result != null ? result.ToString() : "0";
                }
            }
        }

        private void ValidarCampos(object sender, EventArgs e)
        {
            btnActivarRiego.Enabled = (cmbZonaRiego.SelectedIndex != -1 && nudDuracionMinutos.Value > 0);
        }


        private void btnActivarRiego_Click_1(object sender, EventArgs e)
        {
            try
            {
                int zonaId = Convert.ToInt32(cmbZonaRiego.SelectedValue);
                int duracion = (int)nudDuracionMinutos.Value;
                string usuario = lblUsuarioActual.Text;

                using (MySqlConnection conn = (MySqlConnection)new Conexion().AbrirConexion(2))

                {
                    string query = "CALL ActivarRiegoManual(@zona, @duracion, @usuario)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@zona", zonaId);
                    cmd.Parameters.AddWithValue("@duracion", duracion);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Riego activado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al activar el riego: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistorial()
        {
            using (MySqlConnection conn = (MySqlConnection)new Conexion().AbrirConexion(2))
            {
                string query = "SELECT fecha_hora_inicio AS 'Inicio', duracion_real AS 'Duración (min)',motivo_activacion AS" +
                    " 'Motivo',usuario_modificacion AS 'Usuario' FROM historial_riego ORDER BY fecha_hora_inicio DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvHistorialRiegoManual.AutoGenerateColumns = true;
                dgvHistorialRiegoManual.DataSource = dt;

                //MessageBox.Show("Historial cargado: " + dt.Rows.Count + " filas.");
            }
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}