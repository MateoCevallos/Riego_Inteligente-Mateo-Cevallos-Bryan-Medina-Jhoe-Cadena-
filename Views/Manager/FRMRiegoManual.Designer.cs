namespace Riego_Inteligente.Views.Manager
{
    partial class FRMRiegoManual
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.dgvHistorialRiegoManual = new System.Windows.Forms.DataGridView();
            this.btnActivarRiego = new System.Windows.Forms.Button();
            this.cmbZonaRiego = new System.Windows.Forms.ComboBox();
            this.nudDuracionMinutos = new System.Windows.Forms.NumericUpDown();
            this.lblUsuarioActual = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtArea_M2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialRiegoManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuracionMinutos)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Área (m2)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Usuario Actual";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Duración (minutos)";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(56, 69);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(78, 13);
            this.lblNombre.TabIndex = 19;
            this.lblNombre.Text = "Zona de Riego";
            // 
            // dgvHistorialRiegoManual
            // 
            this.dgvHistorialRiegoManual.AllowUserToAddRows = false;
            this.dgvHistorialRiegoManual.AllowUserToDeleteRows = false;
            this.dgvHistorialRiegoManual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialRiegoManual.Location = new System.Drawing.Point(359, 65);
            this.dgvHistorialRiegoManual.Name = "dgvHistorialRiegoManual";
            this.dgvHistorialRiegoManual.ReadOnly = true;
            this.dgvHistorialRiegoManual.Size = new System.Drawing.Size(381, 203);
            this.dgvHistorialRiegoManual.TabIndex = 14;
            // 
            // btnActivarRiego
            // 
            this.btnActivarRiego.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnActivarRiego.Location = new System.Drawing.Point(73, 316);
            this.btnActivarRiego.Name = "btnActivarRiego";
            this.btnActivarRiego.Size = new System.Drawing.Size(294, 60);
            this.btnActivarRiego.TabIndex = 13;
            this.btnActivarRiego.Text = "Activar Riego";
            this.btnActivarRiego.UseVisualStyleBackColor = false;
            this.btnActivarRiego.Click += new System.EventHandler(this.btnActivarRiego_Click_1);
            // 
            // cmbZonaRiego
            // 
            this.cmbZonaRiego.FormattingEnabled = true;
            this.cmbZonaRiego.Location = new System.Drawing.Point(59, 85);
            this.cmbZonaRiego.Name = "cmbZonaRiego";
            this.cmbZonaRiego.Size = new System.Drawing.Size(245, 21);
            this.cmbZonaRiego.TabIndex = 26;
            // 
            // nudDuracionMinutos
            // 
            this.nudDuracionMinutos.Location = new System.Drawing.Point(59, 133);
            this.nudDuracionMinutos.Name = "nudDuracionMinutos";
            this.nudDuracionMinutos.Size = new System.Drawing.Size(245, 20);
            this.nudDuracionMinutos.TabIndex = 27;
            // 
            // lblUsuarioActual
            // 
            this.lblUsuarioActual.AutoSize = true;
            this.lblUsuarioActual.Location = new System.Drawing.Point(56, 180);
            this.lblUsuarioActual.Name = "lblUsuarioActual";
            this.lblUsuarioActual.Size = new System.Drawing.Size(83, 13);
            this.lblUsuarioActual.TabIndex = 28;
            this.lblUsuarioActual.Text = "lblUsuarioActual";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnCerrar.Location = new System.Drawing.Point(427, 316);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(294, 60);
            this.btnCerrar.TabIndex = 29;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(506, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Historial de Riego";
            // 
            // txtArea_M2
            // 
            this.txtArea_M2.Location = new System.Drawing.Point(59, 227);
            this.txtArea_M2.Name = "txtArea_M2";
            this.txtArea_M2.Size = new System.Drawing.Size(245, 20);
            this.txtArea_M2.TabIndex = 30;
            // 
            // FRMRiegoManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(800, 425);
            this.Controls.Add(this.txtArea_M2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblUsuarioActual);
            this.Controls.Add(this.nudDuracionMinutos);
            this.Controls.Add(this.cmbZonaRiego);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.dgvHistorialRiegoManual);
            this.Controls.Add(this.btnActivarRiego);
            this.Name = "FRMRiegoManual";
            this.Text = "FRMRiegoManual";
            this.Load += new System.EventHandler(this.FRMRiegoManual_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialRiegoManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuracionMinutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.DataGridView dgvHistorialRiegoManual;
        private System.Windows.Forms.Button btnActivarRiego;
        private System.Windows.Forms.ComboBox cmbZonaRiego;
        private System.Windows.Forms.NumericUpDown nudDuracionMinutos;
        private System.Windows.Forms.Label lblUsuarioActual;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtArea_M2;
    }
}