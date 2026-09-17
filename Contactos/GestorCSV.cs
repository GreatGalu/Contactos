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

                List<string> cabeceras = ParsearLineaCSV(linea);
                int indiceNombre = 0;
                int indiceSegundoNombre = -1;
                int indiceApellido = -1;
                List<int> indicesTelefono = new List<int>();

                bool tieneCabecera = false;

                for (int i = 0; i < cabeceras.Count; i++)
                {
                    string cab = cabeceras[i].Trim().ToLower();

                    if (cab == "nombre" || cab == "first name" || cab == "name")
                    {
                        indiceNombre = i;
                        tieneCabecera = true;
                    }
                    else if (cab == "segundo nombre" || cab == "middle name")
                    {
                        indiceSegundoNombre = i;
                        tieneCabecera = true;
                    }
                    else if (cab == "apellido" || cab == "apellidos" || cab == "last name")
                    {
                        indiceApellido = i;
                        tieneCabecera = true;
                    }
                    else if (cab == "telefono" || cab == "teléfono" || cab == "phone 1 - value" || cab == "phone" || cab == "celular" || cab == "móvil" || cab == "movil")
                    {
                        indicesTelefono.Add(i);
                        tieneCabecera = true;
                    }
                }

                if (indicesTelefono.Count == 0)
                {
                    indicesTelefono.Add(1);
                }

                if (!tieneCabecera)
                {
                    ProcesarLineaCSV(cabeceras, indiceNombre, indiceSegundoNombre, indiceApellido, indicesTelefono, lista);
                }

                while ((linea = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    List<string> valores = ParsearLineaCSV(linea);
                    ProcesarLineaCSV(valores, indiceNombre, indiceSegundoNombre, indiceApellido, indicesTelefono, lista);
                }
            }
        }
        private static List<string> ParsearLineaCSV(string linea)
        {
                List<string> resultado = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool dentroDeComillas = false;

            for (int i = 0; i < linea.Length; i++)
            {
                char c = linea[i];

                if (c == '"')
                {
                    if (dentroDeComillas && i + 1 < linea.Length && linea[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else
                    {
                        dentroDeComillas = !dentroDeComillas;
                    }
                }
                else if (c == ',' && !dentroDeComillas)
                {
                    resultado.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            resultado.Add(sb.ToString());
            return resultado;
        }

        private static void ProcesarLineaCSV(
            List<string> valores,
            int indiceNombre,
            int indiceSegundoNombre,
            int indiceApellido,
            List<int> indicesTelefono,
            ListaOrdenada lista)
        {
            if (valores == null || valores.Count == 0) return;

            string primerNombre = ObtenerValor(valores, indiceNombre);
            string segundoNombre = ObtenerValor(valores, indiceSegundoNombre);
            string apellido = ObtenerValor(valores, indiceApellido);

            string nombre;
            if (indiceSegundoNombre >= 0 || indiceApellido >= 0)
            {
                nombre = string.Join(" ", new[] { primerNombre, segundoNombre, apellido }
                    .Where(s => !string.IsNullOrWhiteSpace(s))).Trim();
            }
            else
            {
                nombre = primerNombre;
            }

            // Si el nombre aún está vacío, intentar buscar algún valor que no sea el teléfono
            if (string.IsNullOrWhiteSpace(nombre))
            {
                for (int i = 0; i < valores.Count; i++)
                {
                    if (!indicesTelefono.Contains(i) && !string.IsNullOrWhiteSpace(valores[i]))
                    {
                        nombre = valores[i].Trim();
                        break;
                    }
                }
            }

            // Obtener el teléfono del primer campo disponible
            string telefono = "";
            foreach (int idxTel in indicesTelefono)
            {
                string valorTel = ObtenerValor(valores, idxTel);
                if (!string.IsNullOrWhiteSpace(valorTel))
                {
                    telefono = valorTel;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(telefono)) return;

            if (nombre.Equals("Nombre", StringComparison.OrdinalIgnoreCase) &&
                (telefono.Equals("Telefono", StringComparison.OrdinalIgnoreCase) ||
                 telefono.Equals("Teléfono", StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrEmpty(telefono)))
                {
                return;
            }

                if (nombre.Equals("First Name", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

            // Google Contacts puede guardar múltiples números separados por " ::: ".
            // Conservar únicamente el primer número (el principal).
            if (!string.IsNullOrEmpty(telefono) && telefono.Contains(":::"))
            {
                telefono = telefono.Split(new string[] { ":::" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            }

            lista.InsertarOrdenado(new Contactos(nombre, telefono));
        }

        private static string ObtenerValor(List<string> valores, int indice)
        {
            if (indice >= 0 && indice < valores.Count)
            {
                return valores[indice].Trim();
            }
            return "";
        }

        public static void ExportarCSV(string rutaArchivo, ListaOrdenada lista)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                sw.WriteLine("Nombre,Teléfono");
                NodoContacto actual = lista.Head;

                while (actual != null)
                {

                    string nom = FormatearCampoCSV(actual.Dato.Nombre);
                    string tel = FormatearCampoCSV(actual.Dato.Telefono);
                    sw.WriteLine($"{nom},{tel}");
                    actual = actual.Siguiente;
                }
            }
        }

        private static string FormatearCampoCSV(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return "";
            if (valor.Contains(",") || valor.Contains("\"") || valor.Contains("\n") || valor.Contains("\r"))
            {
                return $"\"{valor.Replace("\"", "\"\"")}\"";
            }
            return valor;
        }
    }
}