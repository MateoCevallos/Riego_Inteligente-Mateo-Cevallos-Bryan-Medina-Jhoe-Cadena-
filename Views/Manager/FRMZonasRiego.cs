using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Riego_Inteligente.Config;

namespace Riego_Inteligente.Views.Manager
{
    public partial class FRMZonasRiego : Form
    {
        private int zonaSeleccionadaId = -1;

        public FRMZonasRiego()
        {
            InitializeComponent();
            dgvZonasRiego.CellClick += dgvZonasRiego_CellClick;
            btnAgregarZona.Click += btnAgregarZona_Click;
            btnEditarZona.Click += btnEditarZona_Click;
            btnEliminarZona.Click += btnEliminarZona_Click;
            btnSalirZona.Click += btnSalirZona_Click;
        }

        private void FRMZonasRiego_Load(object sender, EventArgs e)
        {
            CargarZonas();
        }

        private void CargarZonas()
        {
            try
            {
                Conexion conexion = new Conexion();
                using (var cn = conexion.AbrirConexion(2))
                {
                    string query = "SELECT zona_id, nombre_zona, ubicacion, tipo_cultivo, area_m2 FROM zonas_riego";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, (MySqlConnection)cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvZonasRiego.DataSource = dt;
                    dgvZonasRiego.Columns["zona_id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar zonas: " + ex.Message);
            }
        }

        private void btnAgregarZona_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                Conexion conexion = new Conexion();
                using (var cn = conexion.AbrirConexion(2))
                {
                    string query = "INSERT INTO zonas_riego (nombre_zona, ubicacion, tipo_cultivo, area_m2) VALUES (@nombre, @ubicacion, @cultivo, @area)";
                    using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)cn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombreZona.Text.Trim());
                        cmd.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text.Trim());
                        cmd.Parameters.AddWithValue("@cultivo", txtTipoCultivo.Text.Trim());
                        cmd.Parameters.AddWithValue("@area", Convert.ToDecimal(txtAreaM2.Text.Trim()));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Zona agregada correctamente.");
                        LimpiarCampos();
                        CargarZonas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar zona: " + ex.Message);
            }
        }

        private void btnEditarZona_Click(object sender, EventArgs e)
        {
            if (zonaSeleccionadaId == -1)
            {
                MessageBox.Show("Selecciona una zona para editar.");
                return;
            }

            if (!ValidarCampos()) return;

            try
            {
                Conexion conexion = new Conexion();
                using (var cn = conexion.AbrirConexion(2))
                {
                    string query = "UPDATE zonas_riego SET nombre_zona = @nombre, ubicacion = @ubicacion, tipo_cultivo = @cultivo, area_m2 = @area WHERE zona_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)cn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombreZona.Text.Trim());
                        cmd.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text.Trim());
                        cmd.Parameters.AddWithValue("@cultivo", txtTipoCultivo.Text.Trim());
                        cmd.Parameters.AddWithValue("@area", Convert.ToDecimal(txtAreaM2.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id", zonaSeleccionadaId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Zona actualizada correctamente.");
                        LimpiarCampos();
                        CargarZonas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar zona: " + ex.Message);
            }
        }

        private void btnEliminarZona_Click(object sender, EventArgs e)
        {
            if (zonaSeleccionadaId == -1)
            {
                MessageBox.Show("Selecciona una zona para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Estás seguro de eliminar esta zona?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            try
            {
                Conexion conexion = new Conexion();
                using (var cn = conexion.AbrirConexion(2))
                {
                    string query = "DELETE FROM zonas_riego WHERE zona_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)cn))
                    {
                        cmd.Parameters.AddWithValue("@id", zonaSeleccionadaId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Zona eliminada correctamente.");
                        LimpiarCampos();
                        CargarZonas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar zona: " + ex.Message);
            }
        }

        private void dgvZonasRiego_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvZonasRiego.Rows[e.RowIndex];
                zonaSeleccionadaId = Convert.ToInt32(fila.Cells["zona_id"].Value);
                txtNombreZona.Text = fila.Cells["nombre_zona"].Value.ToString();
                txtUbicacion.Text = fila.Cells["ubicacion"].Value.ToString();
                txtTipoCultivo.Text = fila.Cells["tipo_cultivo"].Value.ToString();
                txtAreaM2.Text = fila.Cells["area_m2"].Value.ToString();
            }
        }

        private void btnSalirZona_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreZona.Text) ||
                string.IsNullOrWhiteSpace(txtUbicacion.Text) ||
                string.IsNullOrWhiteSpace(txtTipoCultivo.Text) ||
                string.IsNullOrWhiteSpace(txtAreaM2.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return false;
            }

            if (!decimal.TryParse(txtAreaM2.Text, out _))
            {
                MessageBox.Show("Área inválida.");
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombreZona.Clear();
            txtUbicacion.Clear();
            txtTipoCultivo.Clear();
            txtAreaM2.Clear();
            zonaSeleccionadaId = -1;
        }
    }
}
