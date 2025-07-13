namespace Riego_Inteligente.Views.Manager
{
    partial class FRMMenuPrincipal
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
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.btnRiegoManual = new System.Windows.Forms.Button();
            this.btnAlertas = new System.Windows.Forms.Button();
            this.btnZonasRiego = new System.Windows.Forms.Button();
            this.btnSensores = new System.Windows.Forms.Button();
            this.btnProgramacion = new System.Windows.Forms.Button();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.Location = new System.Drawing.Point(31, 284);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(35, 13);
            this.lblUsuarios.TabIndex = 0;
            this.lblUsuarios.Text = "label1";
            // 
            // btnRiegoManual
            // 
            this.btnRiegoManual.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnRiegoManual.Location = new System.Drawing.Point(181, 33);
            this.btnRiegoManual.Name = "btnRiegoManual";
            this.btnRiegoManual.Size = new System.Drawing.Size(123, 40);
            this.btnRiegoManual.TabIndex = 1;
            this.btnRiegoManual.Text = "Riego Manual";
            this.btnRiegoManual.UseVisualStyleBackColor = false;
            this.btnRiegoManual.Click += new System.EventHandler(this.btnRiegoManual_Click);
            // 
            // btnAlertas
            // 
            this.btnAlertas.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnAlertas.Location = new System.Drawing.Point(181, 92);
            this.btnAlertas.Name = "btnAlertas";
            this.btnAlertas.Size = new System.Drawing.Size(123, 40);
            this.btnAlertas.TabIndex = 3;
            this.btnAlertas.Text = "Alertas";
            this.btnAlertas.UseVisualStyleBackColor = false;
            this.btnAlertas.Click += new System.EventHandler(this.btnAlertas_Click);
            // 
            // btnZonasRiego
            // 
            this.btnZonasRiego.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnZonasRiego.Location = new System.Drawing.Point(34, 33);
            this.btnZonasRiego.Name = "btnZonasRiego";
            this.btnZonasRiego.Size = new System.Drawing.Size(123, 40);
            this.btnZonasRiego.TabIndex = 4;
            this.btnZonasRiego.Text = "Zonas de Riego";
            this.btnZonasRiego.UseVisualStyleBackColor = false;
            this.btnZonasRiego.Click += new System.EventHandler(this.btnZonasRiego_Click);
            // 
            // btnSensores
            // 
            this.btnSensores.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnSensores.Location = new System.Drawing.Point(34, 92);
            this.btnSensores.Name = "btnSensores";
            this.btnSensores.Size = new System.Drawing.Size(123, 40);
            this.btnSensores.TabIndex = 5;
            this.btnSensores.Text = "Sensores";
            this.btnSensores.UseVisualStyleBackColor = false;
            this.btnSensores.Click += new System.EventHandler(this.btnSensores_Click);
            // 
            // btnProgramacion
            // 
            this.btnProgramacion.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnProgramacion.Location = new System.Drawing.Point(34, 218);
            this.btnProgramacion.Name = "btnProgramacion";
            this.btnProgramacion.Size = new System.Drawing.Size(123, 40);
            this.btnProgramacion.TabIndex = 6;
            this.btnProgramacion.Text = "Programacion";
            this.btnProgramacion.UseVisualStyleBackColor = false;
            // 
            // btnHistorial
            // 
            this.btnHistorial.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnHistorial.Location = new System.Drawing.Point(181, 218);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(123, 40);
            this.btnHistorial.TabIndex = 7;
            this.btnHistorial.Text = "Historial";
            this.btnHistorial.UseVisualStyleBackColor = false;
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnUsuarios.Location = new System.Drawing.Point(34, 154);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(123, 40);
            this.btnUsuarios.TabIndex = 8;
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = false;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnCerrarSesion.Location = new System.Drawing.Point(181, 154);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(123, 40);
            this.btnCerrarSesion.TabIndex = 9;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            // 
            // FRMMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(339, 328);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnUsuarios);
            this.Controls.Add(this.btnHistorial);
            this.Controls.Add(this.btnProgramacion);
            this.Controls.Add(this.btnSensores);
            this.Controls.Add(this.btnZonasRiego);
            this.Controls.Add(this.btnAlertas);
            this.Controls.Add(this.btnRiegoManual);
            this.Controls.Add(this.lblUsuarios);
            this.Name = "FRMMenuPrincipal";
            this.Text = "FRMMenuPrincipal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Button btnRiegoManual;
        private System.Windows.Forms.Button btnAlertas;
        private System.Windows.Forms.Button btnZonasRiego;
        private System.Windows.Forms.Button btnSensores;
        private System.Windows.Forms.Button btnProgramacion;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}