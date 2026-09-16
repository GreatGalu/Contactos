using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos
{
    internal class NodoContacto
    {
        public Contactos Dato { get; set; }
        public NodoContacto Siguiente { get; set; }
        public NodoContacto (Contactos contactos)
        {
            Dato = contactos;
            Siguiente = null;
        }
        public NodoContacto (string nombre, string telefono)
        {
            Dato = new Contactos(nombre, telefono);
            Siguiente = null;
        }
    }
}
