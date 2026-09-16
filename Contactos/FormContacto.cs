using System;
using System.Windows.Forms;

namespace Contactos
{
     partial class FormContacto : Form
    {
        public Contactos ContactoSeleccionado{ get; private set; }
        private bool esEdicion = false;

        public FormContacto()
        {
            InitializeComponent();
            this.Text = "Agregar Nuevo Contacto";
            esEdicion = false;
        }

        public FormContacto(Contactos contactoExistente)
        {
            InitializeComponent();
            this.Text = "Editar Contacto";
            esEdicion = true;

            // Cargar datos actuales
            txtNombre.Text = contactoExistente.Nombre;
            txtTelefono.Text = contactoExistente.Telefono;
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos y teclas de control como Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (telefono.Length != 10)
            {
                MessageBox.Show("El teléfono debe tener exactamente 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear el objeto de contacto
            ContactoSeleccionado = new Contactos(nombre, telefono);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}