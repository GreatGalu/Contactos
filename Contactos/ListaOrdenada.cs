using Contactos;
using System;
using System.Collections.Generic;

namespace Contactos
{
    internal class ListaOrdenada
    {
        public NodoContacto Head { get; private set; }

        public int TotalContactos { get; private set; }

        public ListaOrdenada()
        {
            Head = null;
            TotalContactos = 0;
        }

        public void InsertarOrdenado(Contactos nuevoContacto)
        {
            NodoContacto nuevoNodo = new NodoContacto(nuevoContacto);

            if (Head == null || string.Compare(nuevoContacto.Nombre, Head.Dato.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
            {
                nuevoNodo.Siguiente = Head;
                Head = nuevoNodo;
            }
            else
            {
                NodoContacto actual = Head;
                while (actual.Siguiente != null &&
                       string.Compare(actual.Siguiente.Dato.Nombre, nuevoContacto.Nombre, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    actual = actual.Siguiente;
                }

                nuevoNodo.Siguiente = actual.Siguiente;
                actual.Siguiente = nuevoNodo;
            }

            TotalContactos++;
        }

        public bool Eliminar(string nombre)
        {
            if (Head == null) return false;

            if (string.Equals(Head.Dato.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
            {
                Head = Head.Siguiente;
                TotalContactos--;
                return true;
            }

            NodoContacto actual = Head;
            while (actual.Siguiente != null)
            {
                if (string.Equals(actual.Siguiente.Dato.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    TotalContactos--;
                    return true;
                }
                actual = actual.Siguiente;
            }

            return false;
        }

        public List<Contactos> Buscar(string criterio)
        {
            List<Contactos> resultados = new List<Contactos>();
            NodoContacto actual = Head;

            while (actual != null)
            {

                if (actual.Dato.Nombre.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    actual.Dato.Telefono.Contains(criterio))
                {
                    resultados.Add(actual.Dato);
                }
                actual = actual.Siguiente;
            }

            return resultados;
        }

        public bool Modificar(string nombreOriginal, Contactos contactoModificado)
        {
            if (Eliminar(nombreOriginal))
            {
                InsertarOrdenado(contactoModificado);
                return true;
            }
            return false;
        }

        public void Limpiar()
        {
            Head = null;
            TotalContactos = 0;
        }

        public List<Contactos> ObtenerTodos()
        {
            List<Contactos> lista = new List<Contactos>();
            NodoContacto actual = Head;

            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }

            return lista;
        }
    }
}