using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDA
{
    /// <summary>
    /// Interfaz para TDA Lista
    /// </summary>
    public interface ILista<T>
    {
        /// <summary>
        /// Inserta un elemento al final de la lista.
        /// </summary>
        /// <param name="item">Elemento a agregar</param>
        void Agregar(T item);

        /// <summary>
        /// Busca un elemento en la lsita.
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Elemento buscado</returns>
        /// <remarks>Devuelve null al no encontrarlo</remarks>
        T Buscar(T item);

        /// <summary>
        /// Busca el elemento indicado y devuelve su posición en la lista
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Posición en la lista</returns>
        /// <remarks>Devuelve -1 al no encontrarlo</remarks>
        int BuscarIndice(T item);

        /// <summary>
        /// Verifica si un elemento pertenece o no a la lista
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Verdadero en caso se encuentre en la lista</returns>
        bool Existe(T item);

        /// <summary>
        /// Elimina un elemento de la lista.
        /// </summary>
        /// <param name="item">Elemento a eliminar</param>
        /// <exception cref="Exception"></exception>
        void Eliminar(T item);

        /// <summary>
        /// Remueve un elemento de la lista en la posición indicada
        /// </summary>
        /// <param name="index">Posición del elemento a remover</param>
        /// <returns>Elemento removido (null si la posición no existe)</returns>
        T Remover(int index);

        /// <summary>
        /// Inserta un elemento a la lista en la posición indicada
        /// </summary>
        /// <param name="index">Posición a insertar [0..Count]</param>
        /// <param name="item">Elemento a insertar</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void Insertar(int index, T item);

        /// <summary>
        /// Indica la cantidad de elementos en la lista
        /// </summary>
        int Longitud { get; }

        /// <summary>
        /// Borra todos los elementos de la lista.
        /// </summary>
        void Limpiar();

        /// <summary>
        /// Busca el elemento en la posición indicada
        /// </summary>
        /// <param name="index">Posición a referenciar</param>
        /// <returns>Elemento en dicha posición (null si la posición no existe)</returns>
        T this[int index] { get; }

        /// <summary>
        /// Busca el elemento en la posición indicada
        /// </summary>
        /// <param name="index">Posición a referenciar</param>
        /// <returns>Elemento en dicha posición (null si la posición no existe)</returns>
        T Elemento(int index);

        /// <summary>
        /// Verifica si la lista no contiene elementos.
        /// </summary>
        bool ListaVacia { get; }
    }
}
