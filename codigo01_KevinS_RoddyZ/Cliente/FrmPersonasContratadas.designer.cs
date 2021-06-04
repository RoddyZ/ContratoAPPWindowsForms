namespace Cliente
{
    partial class FrmPersonasContratadas
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
            this.cbxTipoPersonal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPersonasContratadas = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonasContratadas)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxTipoPersonal
            // 
            this.cbxTipoPersonal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoPersonal.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoPersonal.FormattingEnabled = true;
            this.cbxTipoPersonal.Items.AddRange(new object[] {
            "Ocasional",
            "Titular",
            "Invitado",
            "Honorario",
            "Ayudante"});
            this.cbxTipoPersonal.Location = new System.Drawing.Point(278, 44);
            this.cbxTipoPersonal.Name = "cbxTipoPersonal";
            this.cbxTipoPersonal.Size = new System.Drawing.Size(212, 31);
            this.cbxTipoPersonal.TabIndex = 0;
            this.cbxTipoPersonal.SelectedIndexChanged += new System.EventHandler(this.cbxTipoPersonal_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(83, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de Personal :";
            // 
            // dgvPersonasContratadas
            // 
            this.dgvPersonasContratadas.AllowUserToAddRows = false;
            this.dgvPersonasContratadas.AllowUserToDeleteRows = false;
            this.dgvPersonasContratadas.AllowUserToResizeColumns = false;
            this.dgvPersonasContratadas.AllowUserToResizeRows = false;
            this.dgvPersonasContratadas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPersonasContratadas.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dgvPersonasContratadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPersonasContratadas.Location = new System.Drawing.Point(48, 99);
            this.dgvPersonasContratadas.MultiSelect = false;
            this.dgvPersonasContratadas.Name = "dgvPersonasContratadas";
            this.dgvPersonasContratadas.ReadOnly = true;
            this.dgvPersonasContratadas.RowHeadersVisible = false;
            this.dgvPersonasContratadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPersonasContratadas.Size = new System.Drawing.Size(556, 254);
            this.dgvPersonasContratadas.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Personas :";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(221, 406);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(100, 31);
            this.txtTotal.TabIndex = 4;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmPersonasContratadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 492);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvPersonasContratadas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxTipoPersonal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPersonasContratadas";
            this.Text = "FrmPersonasContratadas";
            this.Load += new System.EventHandler(this.FrmPersonasContratadas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonasContratadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxTipoPersonal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPersonasContratadas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
    }
}