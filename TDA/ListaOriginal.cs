using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA
{
    public class ListaOriginal<T>
    {
        List<T> miLista = new List<T>();

        public void Insertar(T elemento) {
            miLista.Add(elemento);
        }

        public List<T> ObtenerListado() {
            return miLista;
        }

    }
}
