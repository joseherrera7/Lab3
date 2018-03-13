using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDA
{
    public interface IPila<T>
    {
        /// <summary>
        /// Mete un elemento a la pila
        /// </summary>
        /// <param name="item">Elemento a meter</param>
        void Push(T item);

        /// <summary>
        /// Saca el elemento del tope de la pila.
        /// </summary>
        /// <returns>Elemento a sacar</returns>
        /// <exception cref="Exception">Stack underflow exception</exception>
        T Pop();

        /// <summary>
        /// Verifica cual es el valor en el tope de la pila sin sacarlo.
        /// </summary>
        /// <returns>Valor en el tope de la pila</returns>
        /// <exception cref="Exception">Stack underflow exception</exception>
        T Peek();

        /// <summary>
        /// Indica la cantidad de elementos en la pila.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Indica si la pila está vacía.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Elimina todos los elementos de la pila.
        /// </summary>
        void Clear();
    }
}
