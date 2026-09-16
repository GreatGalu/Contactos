using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos
{
    internal class Contactos
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public Contactos(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
        }
        public override string ToString()
        {
            return $"Nombre: {Nombre}, Teléfono: {Telefono}";
        }
    }
}
