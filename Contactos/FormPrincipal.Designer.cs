namespace Contactos
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvContactos = new DataGridView();
            lblTotal = new Label();
            txtFiltro = new TextBox();
            btnFiltro = new Button();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnImportar = new Button();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvContactos).BeginInit();
            SuspendLayout();
            // 
            // dgvContactos
            // 
            dgvContactos.AllowUserToAddRows = false;
            dgvContactos.AllowUserToDeleteRows = false;
            dgvContactos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContactos.Location = new Point(23, 93);
            dgvContactos.Margin = new Padding(3, 4, 3, 4);
            dgvContactos.MultiSelect = false;
            dgvContactos.Name = "dgvContactos";
            dgvContactos.ReadOnly = true;
            dgvContactos.RowHeadersWidth = 51;
            dgvContactos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContactos.Size = new Size(697, 400);
            dgvContactos.TabIndex = 5;
            dgvContactos.CellContentClick += dgvContactos_CellContentClick;
            dgvContactos.CellDoubleClick += dgvContactos_CellDoubleClick;
            dgvContactos.KeyDown += dgvContactos_KeyDown;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(23, 520);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(154, 20);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "Total de contactos: 0";
            // 
            // txtFiltro
            // 
            txtFiltro.Location = new Point(149, 33);
            txtFiltro.Margin = new Padding(3, 4, 3, 4);
            txtFiltro.Name = "txtFiltro";
            txtFiltro.Size = new Size(228, 27);
            txtFiltro.TabIndex = 2;
            // 
            // btnFiltro
            // 
            btnFiltro.Location = new Point(389, 31);
            btnFiltro.Margin = new Padding(3, 4, 3, 4);
            btnFiltro.Name = "btnFiltro";
            btnFiltro.Size = new Size(103, 36);
            btnFiltro.TabIndex = 3;
            btnFiltro.Text = "Filtrar";
            btnFiltro.UseVisualStyleBackColor = true;
            btnFiltro.Click += btnFiltro_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAgregar.Location = new Point(23, 27);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(46, 40);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "+";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnEliminar.Location = new Point(80, 27);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(46, 40);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "-";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnImportar
            // 
            btnImportar.Location = new Point(503, 31);
            btnImportar.Margin = new Padding(3, 4, 3, 4);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(103, 36);
            btnImportar.TabIndex = 4;
            btnImportar.Text = "Importar CSV";
            btnImportar.UseVisualStyleBackColor = true;
            btnImportar.Click += btnImportar_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(617, 31);
            btnExportar.Margin = new Padding(3, 4, 3, 4);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(103, 36);
            btnExportar.TabIndex = 7;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(743, 573);
            Controls.Add(btnExportar);
            Controls.Add(btnImportar);
            Controls.Add(lblTotal);
            Controls.Add(dgvContactos);
            Controls.Add(btnFiltro);
            Controls.Add(txtFiltro);
            Controls.Add(btnEliminar);
            Controls.Add(btnAgregar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestor de Contactos";
            ((System.ComponentModel.ISupportInitialize)dgvContactos).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvContactos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Button btnFiltro;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnExportar;
    }
}