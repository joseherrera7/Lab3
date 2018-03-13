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
            this.Dato = dato;
            this.HijoIzquierdo = hijoIzquierdo;
            this.HijoDerecho = hijoDerecho;
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
        public T Dato
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
        public IArbolBinario<T> HijoIzquierdo
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
        public IArbolBinario<T> HijoDerecho
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

            if (this.HijoIzquierdo != null)
            {
                this.HijoIzquierdo.RecorrerPrefijo(visitar);
            }

            if (this.HijoDerecho != null)
            {
                this.HijoDerecho.RecorrerPrefijo(visitar);
            }

        }

        /// <summary>
        /// Realiza el recorrido en infijo del arbol
        /// Izquierda, Raiz, Derecha
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        public void RecorrerInfijo(VisitarArbolDelegate<T> visitar)
        {
            if (this.HijoIzquierdo != null)
            {
                this.HijoIzquierdo.RecorrerInfijo(visitar);
            }

            visitar(this);

            if (this.HijoDerecho != null)
            {
                this.HijoDerecho.RecorrerInfijo(visitar);
            }
        }

        /// <summary>
        /// Realiza el recorrido en posfijo del arbol
        /// Izquierda, Derecha, Raiz
        /// </summary>
        /// <param name="visitar">Función para visitar el arbol</param>
        public void RecorrerPosfijo(VisitarArbolDelegate<T> visitar)
        {
            if (this.HijoIzquierdo != null)
            {
                this.HijoIzquierdo.RecorrerPosfijo(visitar);
            }

            if (this.HijoDerecho != null)
            {
                this.HijoDerecho.RecorrerPosfijo(visitar);
            }

            visitar(this);
        }

        #endregion
    }
}
