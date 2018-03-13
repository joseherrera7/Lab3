using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDA
{
    public class ListaBase<T> : ILista<T> //where T: IPersona
    {
        /// <summary>
        /// Compara 2 elementos y devuelve verdadero se los elementos son iguales.
        /// </summary>
        /// <param name="item1">Item 1</param>
        /// <param name="item2">Item 2</param>
        /// <returns>Devuelve verdadero se los elementos son iguales.</returns>
        public delegate bool CompararElementosDelegate(T item1, T item2);

        /// <summary>
        /// Estructura que representa un nodo en la lista simple encadenada.
        /// </summary>
        private class NodoLista<TN>
        {
            #region "Variables"

            TN _valor = default(TN);
            NodoLista<T> _siguiente = null;

            #endregion "Variables"

            /// <summary>
            /// Constructor de NodoLista<T>
            /// </summary>
            /// <param name="valor">Valor a guardar</param>
            /// <param name="siguiente">Referencia a siguiente nodo</param>
            public NodoLista(TN valor, NodoLista<T> siguiente)
            {
                this.Valor = valor;
                this.Siguiente = siguiente;
            }

            #region "Public Properties"

            /// <summary>
            /// Valor almacenado
            /// </summary>
            public TN Valor
            {
                get { return _valor; }
                set { _valor = value; }
            }

            /// <summary>
            /// Referencia a siguiente nodo
            /// </summary>
            public NodoLista<T> Siguiente
            {
                get { return _siguiente; }
                set { _siguiente = value; }
            }

            /// <summary>
            /// Función que me sirve para comparar nodos, verdadero si son iguales
            /// </summary>
            /// <param name="N1">Nodo a comparar</param>
            /// <param name="N2">Nodo a comparar</param>
            /// <returns>Verdadero si son iguales</returns>
            public static bool CompararNodos(T N1, T N2)
            {
                return object.Equals(N1, N2);
            }

            #endregion "Public Properties"
        }

        #region "Variables"

        NodoLista<T> _head = null;

        #endregion "Variables"

        CompararElementosDelegate _funcComparar;

        /// <summary>
        /// Constructor básico ListaBase
        /// </summary>
        public ListaBase(CompararElementosDelegate comparaElementos)
        {
            _funcComparar = comparaElementos;
            _head = null;
        }

        public ListaBase(): this(NodoLista<T>.CompararNodos)
        { 

        }

        #region ILista Members

        /// <summary>
        /// Inserta un elemento al final de la lista.
        /// </summary>
        /// <param name="item">Elemento a agregar</param>
        public void Agregar(T item)
        {
            Insertar(this.Longitud, item);
        }

        /// <summary>
        /// Busca un elemento en la lsita.
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Elemento buscado</returns>
        /// <remarks>Devuelve null al no encontrarlo</remarks>
        public T Buscar(T item)
        {
            int index = 0;
            NodoLista<T> nodo = BuscarNodoPorValor(item, out index);
            ////if (nodo == null)
            ////{
            ////    return null;
            ////}
            ////else
            ////{
            ////    return nodo.Valor;
            ////}
            return nodo == null
                ? default(T)
                : nodo.Valor;
        }

        /// <summary>
        /// Busca el elemento indicado y devuelve su posición en la lista
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Posición en la lista</returns>
        /// <remarks>Devuelve -1 al no encontrarlo</remarks>
        public int BuscarIndice(T item)
        {
            int index = 0;
            BuscarNodoPorValor(item, out index);
            return index;
        }

        /// <summary>
        /// Verifica si un elemento pertenece o no a la lista
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Verdadero en caso se encuentre en la lista</returns>
        public bool Existe(T item)
        {
            int index = BuscarIndice(item);
            return index >= 0;
        }

        /// <summary>
        /// Elimina un elemento de la lista.
        /// </summary>
        /// <param name="item">Elemento a eliminar</param>
        /// <exception cref="Exception"></exception>
        public void Eliminar(T item)
        {
            int index = 0;
            NodoLista<T> nodo = BuscarNodoPorValor(item, out index);
            if (nodo == null)
            {
                throw new Exception("Valor no encontrado en la lista");
            }
            else
            {
                EliminarNodo(nodo, index);
            }
        }

        /// <summary>
        /// Remueve un elemento de la lista en la posición indicada
        /// </summary>
        /// <param name="index">Posición del elemento a remover</param>
        /// <returns>Elemento removido (null si la posición no existe)</returns>
        public T Remover(int index)
        {
            if (index < 0 || index >= this.Longitud)
            {
                return default(T);
            }

            NodoLista<T> nodoEliminar = BuscarNodo(index);
            return EliminarNodo(nodoEliminar, index);
        }

        /// <summary>
        /// Inserta un elemento a la lista en la posición indicada
        /// </summary>
        /// <param name="index">Posición a insertar [0..Count]</param>
        /// <param name="item">Elemento a insertar</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Insertar(int index, T item)
        {
            if (index < 0 || index > this.Longitud)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index == 0)
            {
                // Insertar en la primera posición.
                NodoLista<T> tmp = _head;

                // Crea el nuevo nodo.
                NodoLista<T> nuevoNodo = new NodoLista<T>(item, tmp);

                // Re-encadena la lista
                _head = nuevoNodo;
            }
            else
            {
                // Buscamos el nodo anterior a donde insertamos
                NodoLista<T> nodoActual = BuscarNodo(index - 1);

                // Crea el nuevo nodo
                NodoLista<T> nuevoNodo = new NodoLista<T>(item, nodoActual.Siguiente);

                // Re-encadenamos con el nuevo.
                nodoActual.Siguiente = nuevoNodo;
            }
        }

        /// <summary>
        /// Indica la cantidad de elementos en la lista
        /// </summary>
        public int Longitud
        {
            get
            {
                int conteo = 0;
                NodoLista<T> nodoActual = _head;
                while (nodoActual != null)
                {
                    nodoActual = nodoActual.Siguiente;
                    conteo++;
                }

                return conteo;
            }
        }

        /// <summary>
        /// Borra todos los elementos de la lista.
        /// </summary>
        public void Limpiar()
        {
            // Limpia/Dispose la información de los nodos.
            NodoLista<T> nodoActual = _head;
            NodoLista<T> tmp = null;
            while (nodoActual != null)
            {
                tmp = nodoActual.Siguiente;

                // Clear: Nodo Actual

                nodoActual = tmp;
            }
            _head = null;
        }

        /// <summary>
        /// Busca el elemento en la posición indicada
        /// </summary>
        /// <param name="index">Posición a referenciar</param>
        /// <returns>Elemento en dicha posición (null si la posición no existe)</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Longitud)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                NodoLista<T> nodoActual = BuscarNodo(index);
                return nodoActual.Valor;
            }
        }

        /// <summary>
        /// Busca el elemento en la posición indicada
        /// </summary>
        /// <param name="index">Posición a referenciar</param>
        /// <returns>Elemento en dicha posición (null si la posición no existe)</returns>
        public T Elemento(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Verifica si la lista no contiene elementos.
        /// </summary>
        public bool ListaVacia
        {
            get { return _head == null; }
        }

        #endregion

        #region "Private Members"

        /// <summary>
        /// Obtiene el NodoLista<T> en la posición indicada.
        /// </summary>
        /// <param name="index">Posición a referenciar</param>
        /// <returns>NodoLista<T> Indicado</returns>
        private NodoLista<T> BuscarNodo(int index)
        {
            int posActual = 0;
            NodoLista<T> nodoActual = _head;

            // Buscamos el nodo secuencialmente
            while (posActual < index)
            {
                nodoActual = nodoActual.Siguiente;
                posActual++;
            }

            return nodoActual;
        }

        /// <summary>
        /// Busca el primer nodo que concuerde con el valor indicado.
        /// </summary>
        /// <param name="valor">Valor a buscar</param>
        /// <param name="index">Posición relacionada</param>
        /// <returns>NodoLista<T> Indicado</returns>
        private NodoLista<T> BuscarNodoPorValor(T valor, out int index)
        {
            index = -1;
            int posActual = 0;
            NodoLista<T> nodoActual = _head;

            // Buscamos el nodo secuencialmente
            while (nodoActual != null)
            {
                if (_funcComparar(nodoActual.Valor, valor))
                {
                    index = posActual;
                    return nodoActual;
                }
                nodoActual = nodoActual.Siguiente;
                posActual++;
            }

            // No lo encontró
            return null;
        }

        /// <summary>
        /// Elimina el nodo indicado de una posición relacionada
        /// </summary>
        /// <param name="nodoEliminar">Nodo a eliminar</param>
        /// <param name="index">Posición de dicho nodo</param>
        /// <returns>Valor del nodo</returns>
        private T EliminarNodo(NodoLista<T> nodoEliminar, int index)
        {
            if (index == 0)
            {
                _head = nodoEliminar.Siguiente;
            }
            else
            {
                NodoLista<T> nodoAnterior = BuscarNodo(index - 1);
                nodoAnterior.Siguiente = nodoEliminar.Siguiente;
            }

            T result = nodoEliminar.Valor;
            // Clear: NodoEliminar

            return result;
        }

        #endregion "Private Members"
    }
}
