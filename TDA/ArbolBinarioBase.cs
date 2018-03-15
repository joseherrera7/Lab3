using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDALibrary;

namespace TDA
{
   
    public class ArbolBinarioBase<T>: IArbolBinario<T>
    {
 
        #region Variables
        /// <summary>
        /// Variables de cada árbol insertado.
        /// </summary>
        T _dato;
        IArbolBinario<T> _hijoDerecho = null;
        IArbolBinario<T> _hijoIzquierdo = null;
        IArbolBinario<T> _padre = null;
        int _factor;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor básico
        /// </summary>
        /// <param name="dato"></param>
        public ArbolBinarioBase(T dato): this(dato, null, null)
        {
            
        }

        /// <summary>
        /// Constructor con parámetros incluidos
        /// </summary>
        /// <param name="dato">El dato a insertar</param>
        /// <param name="hijoIzquierdo">Si posee hijo izquierdo lo agregamos de una vez</param>
        /// <param name="hijoDerecho">Si posee hijo derecho lo agregamos de un vez</param>
        public ArbolBinarioBase(T dato, IArbolBinario<T> hijoIzquierdo,
            IArbolBinario<T> hijoDerecho)
        {
            this.valor = dato;
            this.izquierdo = hijoIzquierdo;
            this.derecho = hijoDerecho;
            this.Padre = null;
            this.FactorBalance = 0;
        }

        public ArbolBinarioBase()
        {

        }



        #endregion

        #region IArbolBinario<T> Members

        /// <summary>
        /// Factor de balance del arbol diferencia de altura del árbol derecho con respecto al árbol izquierdo.
        /// </summary>
        public int FactorBalance
        {
            get 
            {
                return _factor;
            }
            set 
            {
                _factor = value;
            }
        }

        /// <summary>
        /// Dato almacenado
        /// </summary>
        public T valor
        {
            get
            {
                return _dato;
            }
            set
            {
                _dato = value;
            }
        }

        /// <summary>
        /// Arbol binario izquierda
        /// </summary>
        public IArbolBinario<T> izquierdo
        {
            get
            {
                return _hijoIzquierdo;
            }
            set
            {
                _hijoIzquierdo = value;
            }
        }

        /// <summary>
        /// Arbol binario derecha
        /// </summary>
        public IArbolBinario<T> derecho
        {
            get
            {
                return _hijoDerecho;
            }
            set
            {
                _hijoDerecho = value;
            }
        }

        /// <summary>
        /// Arbol padre
        /// </summary>
        public IArbolBinario<T> Padre
        {
            get
            {
                return _padre;
            }
            set
            {
                _padre = value;
            }
        }

        /// <summary>
        /// Realiza el recorrido en prefijo del arbol
        /// Raiz, izquierda, derecha
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        public void RecorrerPrefijo(VisitarArbolDelegate<T> visitar)
        {
            visitar(this);

            if (this.izquierdo != null)
            {
                this.izquierdo.RecorrerPrefijo(visitar);
            }

            if (this.derecho != null)
            {
                this.derecho.RecorrerPrefijo(visitar);
            }

        }

        /// <summary>
        /// Realiza el recorrido en infijo del arbol
        /// Izquierda, Raiz, Derecha
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        public void RecorrerInfijo(VisitarArbolDelegate<T> visitar)
        {
            if (this.izquierdo != null)
            {
                this.izquierdo.RecorrerInfijo(visitar);
            }

            visitar(this);

            if (this.derecho != null)
            {
                this.derecho.RecorrerInfijo(visitar);
            }
        }

        /// <summary>
        /// Realiza el recorrido en posfijo del arbol
        /// Izquierda, Derecha, Raiz
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        public void RecorrerPosfijo(VisitarArbolDelegate<T> visitar)
        {
            if (this.izquierdo != null)
            {
                this.izquierdo.RecorrerPosfijo(visitar);
            }

            if (this.derecho != null)
            {
                this.derecho.RecorrerPosfijo(visitar);
            }

            visitar(this);
        }

        #endregion
    }
}
