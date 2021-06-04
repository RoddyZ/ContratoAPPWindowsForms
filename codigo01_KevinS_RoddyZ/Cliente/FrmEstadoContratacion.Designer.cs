namespace Cliente
{
    partial class FrmEstadoContratacion
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnCargarDocumento = new System.Windows.Forms.Button();
            this.btnValidar = new System.Windows.Forms.Button();
            this.dgvEstadoPersonas = new System.Windows.Forms.DataGridView();
            this.btnVerEstado = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadoPersonas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(101, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar : ";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(233, 54);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(255, 31);
            this.txtNombre.TabIndex = 2;
            // 
            // btnCargarDocumento
            // 
            this.btnCargarDocumento.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCargarDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarDocumento.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarDocumento.ForeColor = System.Drawing.Color.Snow;
            this.btnCargarDocumento.Location = new System.Drawing.Point(365, 406);
            this.btnCargarDocumento.Name = "btnCargarDocumento";
            this.btnCargarDocumento.Size = new System.Drawing.Size(258, 42);
            this.btnCargarDocumento.TabIndex = 4;
            this.btnCargarDocumento.Text = "Adjuntar Documento";
            this.btnCargarDocumento.UseVisualStyleBackColor = false;
            this.btnCargarDocumento.Visible = false;
            this.btnCargarDocumento.Click += new System.EventHandler(this.btnCargarDocumento_Click);
            // 
            // btnValidar
            // 
            this.btnValidar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnValidar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidar.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.ForeColor = System.Drawing.Color.Snow;
            this.btnValidar.Location = new System.Drawing.Point(99, 406);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(207, 42);
            this.btnValidar.TabIndex = 5;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = false;
            this.btnValidar.Visible = false;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // dgvEstadoPersonas
            // 
            this.dgvEstadoPersonas.AllowUserToAddRows = false;
            this.dgvEstadoPersonas.AllowUserToDeleteRows = false;
            this.dgvEstadoPersonas.AllowUserToResizeColumns = false;
            this.dgvEstadoPersonas.AllowUserToResizeRows = false;
            this.dgvEstadoPersonas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstadoPersonas.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dgvEstadoPersonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstadoPersonas.Location = new System.Drawing.Point(91, 169);
            this.dgvEstadoPersonas.MultiSelect = false;
            this.dgvEstadoPersonas.Name = "dgvEstadoPersonas";
            this.dgvEstadoPersonas.ReadOnly = true;
            this.dgvEstadoPersonas.RowHeadersVisible = false;
            this.dgvEstadoPersonas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstadoPersonas.Size = new System.Drawing.Size(532, 185);
            this.dgvEstadoPersonas.TabIndex = 6;
            this.dgvEstadoPersonas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstadoPersonas_CellClick);
            // 
            // btnVerEstado
            // 
            this.btnVerEstado.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnVerEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerEstado.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerEstado.ForeColor = System.Drawing.Color.Snow;
            this.btnVerEstado.Location = new System.Drawing.Point(233, 103);
            this.btnVerEstado.Name = "btnVerEstado";
            this.btnVerEstado.Size = new System.Drawing.Size(207, 42);
            this.btnVerEstado.TabIndex = 7;
            this.btnVerEstado.Text = "Ver Estado";
            this.btnVerEstado.UseVisualStyleBackColor = false;
            this.btnVerEstado.Click += new System.EventHandler(this.btnVerEstado_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmEstadoContratacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 540);
            this.Controls.Add(this.btnVerEstado);
            this.Controls.Add(this.dgvEstadoPersonas);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.btnCargarDocumento);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEstadoContratacion";
            this.Text = "FrmEstadoContratacion";
            this.Load += new System.EventHandler(this.FrmEstadoContratacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadoPersonas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnCargarDocumento;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.DataGridView dgvEstadoPersonas;
        private System.Windows.Forms.Button btnVerEstado;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}