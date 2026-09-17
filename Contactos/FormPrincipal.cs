using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Contactos
{
    public partial class FormPrincipal : Form
    {
        private ListaOrdenada listaContactos;
        private bool filtradoActivo = false;

        public FormPrincipal()
        {
            InitializeComponent();
            listaContactos = new ListaOrdenada();
            ConfigurarGrid();
            ActualizarVistaGrid();
        }

        private void ConfigurarGrid()
        {
            dgvContactos.Columns.Clear();
            dgvContactos.Columns.Add("ColNumero", "#");
            dgvContactos.Columns.Add("ColNombre", "Nombre");
            dgvContactos.Columns.Add("ColTelefono", "Teléfono");

            dgvContactos.Columns["ColNumero"].Width = 40;
            dgvContactos.Columns["ColNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvContactos.Columns["ColTelefono"].Width = 150;
        }

        private void ActualizarVistaGrid(List<Contactos> datos = null)
        {
            dgvContactos.Rows.Clear();
            var listaAMostrar = datos ?? listaContactos.ObtenerTodos();

            int contador = 1;
            foreach (var contacto in listaAMostrar)
            {
                dgvContactos.Rows.Add(contador, contacto.Nombre, contacto.Telefono);
                contador++;
            }

            lblTotal.Text = $"Total de contactos: {listaContactos.TotalContactos}";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (FormContacto frm = new FormContacto())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    listaContactos.InsertarOrdenado(frm.ContactoSeleccionado);
                    ActualizarVistaGrid();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarContactoSeleccionado();
        }

        private void dgvContactos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                EliminarContactoSeleccionado();
            }
        }

        private void EliminarContactoSeleccionado()
        {
            if (dgvContactos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un contacto para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string nombre = dgvContactos.SelectedRows[0].Cells["ColNombre"].Value.ToString();

            var confirm = MessageBox.Show($"¿Está seguro de eliminar el contacto '{nombre}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                listaContactos.Eliminar(nombre);

                if (filtradoActivo)
                    AplicarFiltro();
                else
                    ActualizarVistaGrid();
            }
        }

        private void dgvContactos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreActual = dgvContactos.Rows[e.RowIndex].Cells["ColNombre"].Value.ToString();
            string telActual = dgvContactos.Rows[e.RowIndex].Cells["ColTelefono"].Value.ToString();

            Contactos contactoAEditar = new Contactos(nombreActual, telActual);

            using (FormContacto frm = new FormContacto(contactoAEditar))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    listaContactos.Eliminar(nombreActual);
                    listaContactos.InsertarOrdenado(frm.ContactoSeleccionado);

                    if (filtradoActivo)
                        AplicarFiltro();
                    else
                        ActualizarVistaGrid();
                }
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            if (filtradoActivo)
            {
                // Quitar filtro
                txtFiltro.Text = "";
                filtradoActivo = false;
                btnFiltro.Text = "Filtrar";
                ActualizarVistaGrid();
            }
            else
            {
                // Aplicar filtro
                if (!string.IsNullOrWhiteSpace(txtFiltro.Text))
                {
                    AplicarFiltro();
                    filtradoActivo = true;
                    btnFiltro.Text = "Quitar Filtro";
                }
            }
        }

        private void AplicarFiltro()
        {
            var resultados = listaContactos.Buscar(txtFiltro.Text.Trim());
            ActualizarVistaGrid(resultados);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Archivos CSV|*.csv", Title = "Importar Contactos" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    GestorCSV.ImportarCSV(ofd.FileName, listaContactos);
                    ActualizarVistaGrid();
                    MessageBox.Show("Contactos importados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (listaContactos.TotalContactos == 0)
            {
                MessageBox.Show("No hay contactos para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Archivos CSV|*.csv", Title = "Exportar Contactos" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GestorCSV.ExportarCSV(sfd.FileName, listaContactos);
                    MessageBox.Show("Contactos exportados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}