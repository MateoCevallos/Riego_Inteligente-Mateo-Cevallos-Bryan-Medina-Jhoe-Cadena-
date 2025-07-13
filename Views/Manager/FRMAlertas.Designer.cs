namespace Riego_Inteligente.Views.Manager
{
    partial class FRMAlertas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.lblTipoAlerta = new System.Windows.Forms.Label();
            this.lblZonaRiego = new System.Windows.Forms.Label();
            this.btnSalirAlerta = new System.Windows.Forms.Button();
            this.btnEliminarAlerta = new System.Windows.Forms.Button();
            this.btnEditarAlerta = new System.Windows.Forms.Button();
            this.dgvAlertas = new System.Windows.Forms.DataGridView();
            this.btnAgregarAlerta = new System.Windows.Forms.Button();
            this.cmbTipoAlerta = new System.Windows.Forms.ComboBox();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUsuarioAsignado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbZonaRiego = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Fecha y Hora";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Mensaje";
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(54, 151);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(245, 20);
            this.txtMensaje.TabIndex = 22;
            // 
            // lblTipoAlerta
            // 
            this.lblTipoAlerta.AutoSize = true;
            this.lblTipoAlerta.Location = new System.Drawing.Point(51, 89);
            this.lblTipoAlerta.Name = "lblTipoAlerta";
            this.lblTipoAlerta.Size = new System.Drawing.Size(73, 13);
            this.lblTipoAlerta.TabIndex = 21;
            this.lblTipoAlerta.Text = "Tipo de Alerta";
            // 
            // lblZonaRiego
            // 
            this.lblZonaRiego.AutoSize = true;
            this.lblZonaRiego.Location = new System.Drawing.Point(51, 42);
            this.lblZonaRiego.Name = "lblZonaRiego";
            this.lblZonaRiego.Size = new System.Drawing.Size(78, 13);
            this.lblZonaRiego.TabIndex = 19;
            this.lblZonaRiego.Text = "Zona de Riego";
            // 
            // btnSalirAlerta
            // 
            this.btnSalirAlerta.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnSalirAlerta.Location = new System.Drawing.Point(583, 349);
            this.btnSalirAlerta.Name = "btnSalirAlerta";
            this.btnSalirAlerta.Size = new System.Drawing.Size(152, 60);
            this.btnSalirAlerta.TabIndex = 17;
            this.btnSalirAlerta.Text = "Salir";
            this.btnSalirAlerta.UseVisualStyleBackColor = false;
            this.btnSalirAlerta.Click += new System.EventHandler(this.btnSalirAlerta_Click_1);
            // 
            // btnEliminarAlerta
            // 
            this.btnEliminarAlerta.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnEliminarAlerta.Location = new System.Drawing.Point(408, 349);
            this.btnEliminarAlerta.Name = "btnEliminarAlerta";
            this.btnEliminarAlerta.Size = new System.Drawing.Size(152, 60);
            this.btnEliminarAlerta.TabIndex = 16;
            this.btnEliminarAlerta.Text = "Eliminar";
            this.btnEliminarAlerta.UseVisualStyleBackColor = false;
            this.btnEliminarAlerta.Click += new System.EventHandler(this.btnEliminarAlerta_Click);
            // 
            // btnEditarAlerta
            // 
            this.btnEditarAlerta.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnEditarAlerta.Location = new System.Drawing.Point(233, 349);
            this.btnEditarAlerta.Name = "btnEditarAlerta";
            this.btnEditarAlerta.Size = new System.Drawing.Size(152, 60);
            this.btnEditarAlerta.TabIndex = 15;
            this.btnEditarAlerta.Text = "Editar";
            this.btnEditarAlerta.UseVisualStyleBackColor = false;
            this.btnEditarAlerta.Click += new System.EventHandler(this.btnEditarAlerta_Click);
            // 
            // dgvAlertas
            // 
            this.dgvAlertas.AllowUserToAddRows = false;
            this.dgvAlertas.AllowUserToDeleteRows = false;
            this.dgvAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlertas.Location = new System.Drawing.Point(354, 58);
            this.dgvAlertas.Name = "dgvAlertas";
            this.dgvAlertas.ReadOnly = true;
            this.dgvAlertas.Size = new System.Drawing.Size(381, 256);
            this.dgvAlertas.TabIndex = 14;
            // 
            // btnAgregarAlerta
            // 
            this.btnAgregarAlerta.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnAgregarAlerta.Location = new System.Drawing.Point(54, 349);
            this.btnAgregarAlerta.Name = "btnAgregarAlerta";
            this.btnAgregarAlerta.Size = new System.Drawing.Size(152, 60);
            this.btnAgregarAlerta.TabIndex = 13;
            this.btnAgregarAlerta.Text = "Agregar";
            this.btnAgregarAlerta.UseVisualStyleBackColor = false;
            this.btnAgregarAlerta.Click += new System.EventHandler(this.btnAgregarAlerta_Click);
            // 
            // cmbTipoAlerta
            // 
            this.cmbTipoAlerta.FormattingEnabled = true;
            this.cmbTipoAlerta.Location = new System.Drawing.Point(54, 105);
            this.cmbTipoAlerta.Name = "cmbTipoAlerta";
            this.cmbTipoAlerta.Size = new System.Drawing.Size(245, 21);
            this.cmbTipoAlerta.TabIndex = 26;
            // 
            // dtpFechaHora
            // 
            this.dtpFechaHora.Location = new System.Drawing.Point(54, 199);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.Size = new System.Drawing.Size(245, 20);
            this.dtpFechaHora.TabIndex = 27;
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(54, 245);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(245, 21);
            this.cmbEstado.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Estado";
            // 
            // cmbUsuarioAsignado
            // 
            this.cmbUsuarioAsignado.FormattingEnabled = true;
            this.cmbUsuarioAsignado.Location = new System.Drawing.Point(54, 293);
            this.cmbUsuarioAsignado.Name = "cmbUsuarioAsignado";
            this.cmbUsuarioAsignado.Size = new System.Drawing.Size(245, 21);
            this.cmbUsuarioAsignado.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Usuario Asignado";
            // 
            // cmbZonaRiego
            // 
            this.cmbZonaRiego.FormattingEnabled = true;
            this.cmbZonaRiego.Location = new System.Drawing.Point(54, 58);
            this.cmbZonaRiego.Name = "cmbZonaRiego";
            this.cmbZonaRiego.Size = new System.Drawing.Size(245, 21);
            this.cmbZonaRiego.TabIndex = 32;
            // 
            // FRMAlertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbZonaRiego);
            this.Controls.Add(this.cmbUsuarioAsignado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaHora);
            this.Controls.Add(this.cmbTipoAlerta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblTipoAlerta);
            this.Controls.Add(this.lblZonaRiego);
            this.Controls.Add(this.btnSalirAlerta);
            this.Controls.Add(this.btnEliminarAlerta);
            this.Controls.Add(this.btnEditarAlerta);
            this.Controls.Add(this.dgvAlertas);
            this.Controls.Add(this.btnAgregarAlerta);
            this.Name = "FRMAlertas";
            this.Text = "FRMAlertas";
            this.Load += new System.EventHandler(this.FRMAlertas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Label lblTipoAlerta;
        private System.Windows.Forms.Label lblZonaRiego;
        private System.Windows.Forms.Button btnSalirAlerta;
        private System.Windows.Forms.Button btnEliminarAlerta;
        private System.Windows.Forms.Button btnEditarAlerta;
        private System.Windows.Forms.DataGridView dgvAlertas;
        private System.Windows.Forms.Button btnAgregarAlerta;
        private System.Windows.Forms.ComboBox cmbTipoAlerta;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbUsuarioAsignado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbZonaRiego;
    }
}