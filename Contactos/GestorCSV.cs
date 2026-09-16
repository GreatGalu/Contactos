using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos
{
    internal class GestorCSV
    {
        public static void ImportarCSV(string rutaArchivo, ListaOrdenada lista)
        {
            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException("El archivo no existe.");

            lista.Limpiar();

            using (FileStream fs = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                string linea = sr.ReadLine();
                if (linea == null) return;

                // Determinar el formato de las columnas por la cabecera
                string[] cabeceras = linea.Split(',');
                int indiceNombre = 0;
                int indiceTelefono = 1;

                bool tieneCabecera = false;

                for (int i = 0; i < cabeceras.Length; i++)
                {
                    string cab = cabeceras[i].Trim().ToLower();
                    if (cab == "nombre" || cab == "first name" || cab == "name")
                    {
                        indiceNombre = i;
                        tieneCabecera = true;
                    }
                    if (cab == "telefono" || cab == "teléfono" || cab == "phone 1 - value" || cab == "phone")
                    {
                        indiceTelefono = i;
                        tieneCabecera = true;
                    }
                }

                // Si no tiene cabecera o es un formato extraño, tratar de importar la primera línea
                if (!tieneCabecera)
                {
                    ProcesarLineaCSV(cabeceras, indiceNombre, indiceTelefono, lista);
                }

                while ((linea = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] valores = linea.Split(',');
                    ProcesarLineaCSV(valores, indiceNombre, indiceTelefono, lista);
                }
            }
        }

        private static void ProcesarLineaCSV(string[] valores, int indiceNombre, int indiceTelefono, ListaOrdenada lista)
        {
            string nombre = valores[0].Trim();
            string telefono = valores[1].Trim();
            if (valores.Length > Math.Max(indiceNombre, indiceTelefono))
            {

                if (string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(telefono)) return;

                if (nombre.Equals("Nombre", StringComparison.OrdinalIgnoreCase) &&
                    telefono.Equals("Telefono", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                // Google contacts phone numbers are sometimes appended with " ::: "
                if (telefono.Contains(":::"))
                {
                    telefono = telefono.Split(new string[] { ":::" }, StringSplitOptions.None)[0].Trim();
                }

                lista.InsertarOrdenado(new Contactos(nombre, telefono));
            }
        }

        public static void ExportarCSV(string rutaArchivo, ListaOrdenada lista)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                NodoContacto actual = lista.Head;

                while (actual != null)
                {
                    sw.WriteLine($"{actual.Dato.Nombre},{actual.Dato.Telefono}");
                    actual = actual.Siguiente;
                }
            }
        }
    }
}