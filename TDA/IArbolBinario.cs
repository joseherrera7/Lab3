using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TercerProyecto;

namespace TDA
{
    /// <summary>
    /// Procedimiento genérico para visitar el arbol
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    public delegate void VisitarArbolDelegate<T>(IArbolBinario<T> item);

    /// <summary>
    /// Interfaz de un árbol binario
    /// </summary>
    /// <typeparam name="T">Tipo de dato que almacena el árbol</typeparam>
    public interface IArbolBinario<T>
    {
        /// <summary>
        /// Dato almacenado
        /// </summary>
        T Dato { get; set; }

        /// <summary>
        /// Arbol binario izquierda
        /// </summary>
        IArbolBinario<T> HijoIzquierdo { get; set; }

        /// <summary>
        /// Arbol binario de la derecha
        /// </summary>
        IArbolBinario<T> HijoDerecho { get; set; }

        /// <summary>
        /// Arbol padre
        /// </summary>
        IArbolBinario<T> Padre { get; set; }

        /// <summary>
        /// Factor de balance del arbol diferencia de altura del árbol derecho con respecto al árbol izquierdo.
        /// </summary>
        int FactorBalance { get; set; }

        /// <summary>
        /// Realiza el recorrido en prefijo del arbol
        /// Raiz, izquierda, derecha
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        void RecorrerPrefijo(VisitarArbolDelegate<T> visitar);

        /// <summary>
        /// Realiza el recorrido en infijo del arbol
        /// Izquierda, Raiz, Derecha
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        void RecorrerInfijo(VisitarArbolDelegate<T> visitar);

        /// <summary>
        /// Realiza el recorrido en posfijo del arbol
        /// Izquierda, Derecha, Raiz
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        void RecorrerPosfijo(VisitarArbolDelegate<T> visitar);

        //Vaciar Árbol

    }
}
