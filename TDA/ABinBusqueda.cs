using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDALibrary;

namespace TDA
{
    public class ABinBusqueda<T, K>: IArbolBusquedaBinario<T, K>
    {
        #region Variables
        protected ArbolBinarioBase<T> _raiz;
        CompararLlavesDelegate<K> _fnCompararLave;
        ObtenerLlaveDelegate<T, K> _fnObtenerLlave;
        ListaBase<T> miLista;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor en el cual se incluyen las funciones de comparar y de obtener llaves
        /// </summary>
        /// <param name="p_FuncionCompararLlaves">Funcion necesaria para el funcionamiento del arbol</param>
        /// <param name="p_FuncionObtenerLlaves">Funcion necesaria para el funcionamiento del arbol</param>
        public ABinBusqueda(CompararLlavesDelegate<K> p_FuncionCompararLlaves, ObtenerLlaveDelegate<T, K> p_FuncionObtenerLlaves)
        {
            miLista = new ListaBase<T>();
            _raiz = null;
            _raiz.Padre = null;
            this.FuncionCompararLlave = p_FuncionCompararLlaves;
            this.FuncionObtenerLlave = p_FuncionObtenerLlaves;
        }

        /// <summary>
        /// Constructor básico, será necesario agregarle las funciones de busqueda y comparación luego.
        /// </summary>
        public ABinBusqueda()
        {
            _raiz = null;
            _fnCompararLave = null;
            _fnObtenerLlave = null;
            miLista = new ListaBase<T>();
        }
        #endregion

        #region Miembros publicos
        /// <summary>
        /// Busca y devuelve un Dato del árbol por medio de su llave principal
        /// </summary>
        /// <param name="llave">El dato representativo del conjunto de datos que almacena un nodo en el arbol</param>
        /// <returns>El conjunto de datos que almacena un nodo del arbol, si no existe la llave retornará default(T)</returns>
        public T Buscar(K llave)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (Equals(llave, default(K)))
                throw new ArgumentNullException("La llave enviada no es valida");

            if (_raiz == null)
                return default(T);
            else
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);
                bool encontrado = false;

                while (!encontrado)
                {
                    llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

                    if (comparacion == 0)
                    {
                        return siguiente.Dato;
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.HijoDerecho == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                siguiente = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.HijoIzquierdo == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                siguiente = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún dato.

            return default(T);
        }

        /// <summary>
        /// Busca, devuelve y elimina un nodo del arbol, teniendo cuidado de que el Arbol siga cumpliendo con las caracteristicas de que sea
        /// arbol binario de búsqueda, el método de eliminación utilizado es reemplazando el menor del mayor
        /// </summary>
        /// <param name="llave">El dato representativo</param>
        /// <returns>Conjunto de datos del nodo eliminado.</returns>
        public T Eliminar(K llave)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (Equals(llave, default(K)))
                throw new ArgumentNullException("La llave enviada no es valida");

            if (_raiz == null)
                throw new Exception("El arbol se encuentra vacio");
            else //Si el árbol no está vacio
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                ArbolBinarioBase<T> padre = null;
                bool EsHijoIzquierdo = false;
                bool encontrado = false;

                while (!encontrado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

                    if (comparacion == 0)
                    {

                        if ((siguiente.HijoDerecho == null) && (siguiente.HijoIzquierdo == null)) //Si es una hoja
                        {
                            T miDato = siguiente.Dato;
                            if ((padre != null))
                            {
                                if (EsHijoIzquierdo)
                                    padre.HijoIzquierdo = null;
                                else
                                    padre.HijoDerecho = null;
                            }
                            else //Si padre es null entonces es la raiz
                            {
                                _raiz = null;
                            }

                            return miDato;
                        }
                        else
                        {
                            if (siguiente.HijoDerecho == null) //Si solo tiene rama izquierda
                            {
                                T miDato = siguiente.Dato;
                                if ((padre != null))
                                {
                                    if (EsHijoIzquierdo)
                                        padre.HijoIzquierdo = siguiente.HijoIzquierdo;
                                    else
                                        padre.HijoDerecho = siguiente.HijoDerecho;
                                }
                                else
                                {
                                    _raiz = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                                }

                                return miDato;
                            }
                            else if (siguiente.HijoIzquierdo == null)  //Si solo tiene rama derecha
                            {
                                T miDato = siguiente.Dato;
                                if ((padre != null))
                                {
                                    if (EsHijoIzquierdo)
                                        padre.HijoIzquierdo = siguiente.HijoDerecho;
                                    else
                                        padre.HijoDerecho = siguiente.HijoDerecho;
                                }
                                else
                                {
                                    _raiz = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                                }

                                return miDato;
                            }
                            else  //Tiene ambas ramas el que lo sustituirá será el mas izquierdo de los derechos
                            {
                                ArbolBinarioBase<T> aEliminar = siguiente;
                                siguiente = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                                int cont = 0;
                                while (siguiente.HijoIzquierdo != null)
                                {
                                    padre = siguiente;
                                    siguiente = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                                    cont++;
                                }

                                if (cont > 0)
                                {
                                    if (padre != null)
                                    {
                                        T miDato = aEliminar.Dato;
                                        aEliminar.Dato = siguiente.Dato;
                                        padre.HijoIzquierdo = null;
                                        return miDato;
                                    }
                                    
                                }
                                else
                                {
                                    siguiente.HijoIzquierdo = aEliminar.HijoIzquierdo;

                                    if (padre != null)
                                    {
                                        if (EsHijoIzquierdo)
                                            padre.HijoIzquierdo = aEliminar.HijoDerecho;
                                        else
                                            padre.HijoDerecho = aEliminar.HijoDerecho;
                                    }
                                    else //Es la raiz
                                    {
                                        if (EsHijoIzquierdo)
                                            _raiz = aEliminar.HijoDerecho as ArbolBinarioBase<T>;
                                        else
                                            _raiz = aEliminar.HijoDerecho as ArbolBinarioBase<T>;
                                    }
                                    

                                    return aEliminar.Dato;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.HijoDerecho == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                padre = siguiente;
                                EsHijoIzquierdo = false;
                                siguiente = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                            }

                        }
                        else //menor que 0
                        {
                            if (siguiente.HijoIzquierdo == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                padre = siguiente;
                                EsHijoIzquierdo = true;
                                siguiente = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún dato.

            return default(T);
        }

        /// <summary>
        /// Busca si existe un conjunto de datos o dato determinado.
        /// </summary>
        /// <param name="dato">El nodo del arbol a buscar</param>
        /// <returns>Verdadero si encontró dicho nodo.</returns>
        public bool ExisteElemento(T dato)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (Equals(dato, default(T)))
                throw new ArgumentNullException("La llave enviada no es valida");

            if (_raiz == null)
                return false;
            else
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                K llaveBuscar = this.FuncionObtenerLlave(dato);
                bool encontrado = false;

                while (!encontrado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llaveBuscar, llaveSiguiente);

                    if (comparacion == 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.HijoDerecho == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.HijoIzquierdo == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún dato.
            return false;
        }

        /// <summary>
        /// Obtiene o establece la función que servirá para comparar dos llaves
        /// </summary>
        public CompararLlavesDelegate<K> FuncionCompararLlave
        {
            get
            {
                return _fnCompararLave;
            }
            set
            {
                _fnCompararLave = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la función que servirá para obtener la llave primaria de un nodo del arbol
        /// </summary>
        public ObtenerLlaveDelegate<T, K> FuncionObtenerLlave
        {
            get
            {
                return _fnObtenerLlave;
            }
            set
            {
                _fnObtenerLlave = value;
            }
        }

        /// <summary>
        /// Inserta un Dato en su posición especifica cumpliendo con las reglas de los Arboles binarios de búsqueda.
        /// </summary>
        /// <param name="dato">El dato que se desea insertar.</param>
        public void Insertar(T dato)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");
            
            if (dato == null)
                throw new ArgumentNullException("El dato ingresado está vacio");

            if (_raiz == null)
                _raiz = new ArbolBinarioBase<T>(dato);
            else
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                K llaveInsertar = this.FuncionObtenerLlave(dato);
                bool yaInsertado = false;

                while (!yaInsertado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llaveInsertar, llaveSiguiente);

                    if (comparacion == 0)
                    {
                        throw new Exception("El dato ingresado posee una llave que ya existe en la estructura");
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.HijoDerecho == null)
                            {
                                siguiente.HijoDerecho = new ArbolBinarioBase<T>(dato);
                                yaInsertado = true;
                            }
                            else
                            {
                                siguiente = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.HijoIzquierdo == null)
                            {
                                siguiente.HijoIzquierdo = new ArbolBinarioBase<T>(dato);
                                yaInsertado = true;
                            }
                            else
                            {
                                siguiente = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo
            }
        }

        /// <summary>
        /// Verifica si existe un nodo del arbol a través de su llave primaria
        /// </summary>
        /// <param name="llave">La llave primaria del dato buscado.</param>
        /// <returns>Verdadero si lo encontró.</returns>
        public bool ExisteElementoPorLlave(K llave)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (Equals(llave, default(K)))
                throw new ArgumentNullException("La llave enviada no es valida");

            if (_raiz == null)
                return false;
            else
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                bool encontrado = false;

                while (!encontrado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

                    if (comparacion == 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.HijoDerecho == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.HijoDerecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.HijoIzquierdo == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.HijoIzquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún dato.
            return false;
        }

        /// <summary>
        /// Método que recorre el arbol de la forma: izquierda - centro - derecha
        /// </summary>
        /// <param name="fnVisitar">La función encargada de ir recolectando los datos del árbol</param>
        public void RecorrerInOrder(VisitarNodoDelegate<T> fnVisitar)
        {
            miLista.Limpiar();
            _raiz.RecorrerInfijo(VisitarArbol);
            for (int i = 0; i < miLista.Longitud; i++)
            {
                fnVisitar(miLista[i]);
            }
        }

        /// <summary>
        /// Método que recorre el arbol de la forma: izquierda - derecha - centro
        /// </summary>
        /// <param name="fnVisitar">La función encargada de ir recolectando los datos del árbol</param>
        public void RecorrerPostOrder(VisitarNodoDelegate<T> fnVisitar)
        {
            miLista.Limpiar();
            _raiz.RecorrerPosfijo(VisitarArbol);
            for (int i = 0; i < miLista.Longitud; i++)
            {
                fnVisitar(miLista[i]);
            }
        }

        /// <summary>
        /// Método que recorre el arbol de la forma: centro - izquierda - derecha 
        /// </summary>
        /// <param name="fnVisitar">La función encargada de ir recolectando los datos del árbol</param>
        public void RecorrerPreOrder(VisitarNodoDelegate<T> fnVisitar)
        {
            miLista.Limpiar();
            _raiz.RecorrerPrefijo(VisitarArbol);
            for (int i = 0; i < miLista.Longitud; i++)
            {
                fnVisitar(miLista[i]);
            }
        }
        public List<T> ToList()
        {
           
            List<T> nuevaLista = new List<T>();
            for (int i = 0; i < miLista.Longitud; i++)
            {
                nuevaLista.Add(miLista.Elemento(i));
            }
            return nuevaLista;

        }
        #endregion

        #region Miembros Privados
        /// <summary>
        /// Método interno creado para reutilizar el código de los recorridos que fue creado para un Arbol Binario normal, basicamente almacena
        /// el resultado de los recorridos en una lista de arboles.
        /// </summary>
        /// <param name="arbol">Debe iniciar con la raiz y va guardando los arboles.</param>
        internal void VisitarArbol(IArbolBinario<T> arbol)
        {
            miLista.Agregar(arbol.Dato);
        }
        #endregion

    }
}
