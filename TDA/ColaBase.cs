using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDA
{
    public class ColaBase<T> : ICola<T>
    {
        ILista<T> _lista = new ListaBase<T>();

        #region ICola Members

        /// <summary>
        /// Procedimiento para ingresar un nuevo elemento a la cola
        /// </summary>
        /// <param name="item">El elemento a encolar</param>
        public void Encolar(T item)
        {
            _lista.Agregar(item);
        }

        /// <summary>
        /// Retira al elemento más antiguo de la cola
        /// </summary>
        /// <returns>El primer elemento ingresado</returns>
        /// <exception cref="Esception">Si la cola está vacia</exception>
        public T SacarCola()
        {
            if (this.EstaVacia)
            {
                throw new Exception("La cola está vacía");
            }

            return _lista.Remover(0);
        }

        /// <summary>
        /// Devuelve el primer elemento pero no lo saca de la cola
        /// </summary>
        /// <returns>El primer elemento de la cola sin retirarlo</returns>
        /// <exception cref="Esception">Si la cola está vacia</exception>
        public T VerPrimerElemento()
        {
            if (this.EstaVacia)
            {
                throw new Exception("La cola está vacía");
            }

            return _lista[0];
        }

        /// <summary>
        /// Devuelve la longitud de la cola
        /// </summary>
        public int Longitud
        {
            get
            {
                return _lista.Longitud;
            }
        }

        /// <summary>
        /// Verifica si la cola está vacia
        /// </summary>
        public bool EstaVacia
        {
            get
            {
                return _lista.ListaVacia;
            }
        }

        /// <summary>
        /// Elimina todos los nodos de la pila
        /// </summary>
        public void Limpiar()
        {
            _lista.Limpiar();
        }

        #endregion
    }

}