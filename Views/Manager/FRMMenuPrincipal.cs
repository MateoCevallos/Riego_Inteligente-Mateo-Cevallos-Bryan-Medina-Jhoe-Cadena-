using System;
using System.Windows.Forms;

namespace Riego_Inteligente.Views.Manager
{
    public partial class FRMMenuPrincipal : Form
    {
        private string usuario;
        private string rol;

        public FRMMenuPrincipal(string usuario, string rol)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.rol = rol;

            lblUsuarios.Text = $"Bienvenido, {usuario} – Rol: {rol}";

            // Solo muestra el botón Usuarios si es admin
            if (rol != "admin")
            {
                btnUsuarios.Visible = false;
            }
        }

        public FRMMenuPrincipal() // constructor vacío para el diseñador
        {
            InitializeComponent();
        }

        private void btnZonasRiego_Click(object sender, EventArgs e)
        {
            var FRM_ZonasRiego = new Views.Manager.FRMZonasRiego();

            FRM_ZonasRiego.Show();
        }

        private void btnRiegoManual_Click(object sender, EventArgs e)
        {
            var FRM_RiegoManual = new Views.Manager.FRMRiegoManual();
            FRM_RiegoManual.UsuarioActivo = this.usuario;
            FRM_RiegoManual.Show();
        }

        private void btnAlertas_Click(object sender, EventArgs e)
        {
            var FRM_Alertas = new Views.Manager.FRMAlertas();
            FRM_Alertas.UsuarioActivo = this.usuario;
            FRM_Alertas.RolActivo = this.rol;
            FRM_Alertas.Show();
        }

        private void btnSensores_Click(object sender, EventArgs e)
        {

        }
    }
}
/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Riego_Inteligente.Views.Manager
{
    public partial class FRMMenuPrincipal : Form
    {
        public FRMMenuPrincipal()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}*/
