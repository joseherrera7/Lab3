using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDA
{
    public interface ICola<T>
    {
        /// <summary>
        /// Mete un elemento a la cola
        /// </summary>
        /// <param name="item">Elemento a meter</param>
        void Encolar(T item);

        /// <summary>
        /// Saca el siguiente elemento en cola
        /// </summary>
        /// <returns>Elemento a sacar</returns>
        /// <exception cref="Exception">Cola sin elementos</exception>
        T SacarCola();

        /// <summary>
        /// Verifica cual es el valor en el tope de la cola sin sacarlo.
        /// </summary>
        /// <returns>Valor en el tope de la cola</returns>
        /// <exception cref="Exception">Cola sin elementos</exception>
        T VerPrimerElemento();

        /// <summary>
        /// Indica la cantidad de elementos en la cola.
        /// </summary>
        int Longitud { get; }

        /// <summary>
        /// Indica si la cola está vacía.
        /// </summary>
        bool EstaVacia { get; }

        /// <summary>
        /// Elimina todos los elementos de la cola.
        /// </summary>
        void Limpiar();
    }
}
