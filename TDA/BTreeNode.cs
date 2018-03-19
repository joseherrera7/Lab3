using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA
{
    class BTreeNode<T> where T : IComparable
    {
        public T[] Valores { get; set; }
        public T[] ValoresTemp { get; set; }
        public BTreeNode<T>[] Hijos { get; set; }
        public BTreeNode<T> Padre { get; set; }
        public BTreeNode(int order)
        {
            Valores = new T[order - 1];
            Hijos = new BTreeNode<T>[order];
            Padre = null;
        }
        public bool EsHoja()
        {
            return Hijos.Any(x => x != null);
        }
        public bool TieneEspacio()
        {
            return Valores.Any(x => x == null);
        }
    }
    class Pelicula
    {
        public string Nombre { get; set; }
        public int Año { get; set; }
        public string genero { get; set; }
        public bool plus18 { get; set; }

        //Tamaño de cada registro en el archivo

        public int FixedSizeText = 150;

        //Cadena a escribir en el archivo
        
    }
}
