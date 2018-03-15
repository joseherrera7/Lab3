using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDALibrary;

namespace TDA
{
    public class ABinBusqueda<T, K> : IArbolBusquedaBinario<T, K>
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
        /// Busca y devuelve un valor del árbol por medio de su llave principal
        /// </summary>
        /// <param name="llave">El valor representativo del conjunto de datos que almacena un nodo en el arbol</param>
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
                K llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);
                bool encontrado = false;

                while (!encontrado)
                {
                    llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

                    if (comparacion == 0)
                    {
                        return siguiente.valor;
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.derecho == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                siguiente = siguiente.derecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.izquierdo == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                siguiente = siguiente.izquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún valor.

            return default(T);
        }

        /// <summary>
        /// Busca, devuelve y elimina un nodo del arbol, teniendo cuidado de que el Arbol siga cumpliendo con las caracteristicas de que sea
        /// arbol binario de búsqueda, el método de eliminación utilizado es reemplazando el menor del mayor
        /// </summary>
        /// <param name="llave">El valor representativo</param>
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
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

                    if (comparacion == 0)
                    {

                        if ((siguiente.derecho == null) && (siguiente.izquierdo == null)) //Si es una hoja
                        {
                            T miDato = siguiente.valor;
                            if ((padre != null))
                            {
                                if (EsHijoIzquierdo)
                                    padre.izquierdo = null;
                                else
                                    padre.derecho = null;
                            }
                            else //Si padre es null entonces es la raiz
                            {
                                _raiz = null;
                            }

                            return miDato;
                        }
                        else
                        {
                            if (siguiente.derecho == null) //Si solo tiene rama izquierda
                            {
                                T miDato = siguiente.valor;
                                if ((padre != null))
                                {
                                    if (EsHijoIzquierdo)
                                        padre.izquierdo = siguiente.izquierdo;
                                    else
                                        padre.derecho = siguiente.derecho;
                                }
                                else
                                {
                                    _raiz = siguiente.izquierdo as ArbolBinarioBase<T>;
                                }

                                return miDato;
                            }
                            else if (siguiente.izquierdo == null)  //Si solo tiene rama derecha
                            {
                                T miDato = siguiente.valor;
                                if ((padre != null))
                                {
                                    if (EsHijoIzquierdo)
                                        padre.izquierdo = siguiente.derecho;
                                    else
                                        padre.derecho = siguiente.derecho;
                                }
                                else
                                {
                                    _raiz = siguiente.derecho as ArbolBinarioBase<T>;
                                }

                                return miDato;
                            }
                            else  //Tiene ambas ramas el que lo sustituirá será el mas izquierdo de los derechos
                            {
                                ArbolBinarioBase<T> aEliminar = siguiente;
                                siguiente = siguiente.derecho as ArbolBinarioBase<T>;
                                int cont = 0;
                                while (siguiente.izquierdo != null)
                                {
                                    padre = siguiente;
                                    siguiente = siguiente.izquierdo as ArbolBinarioBase<T>;
                                    cont++;
                                }

                                if (cont > 0)
                                {
                                    if (padre != null)
                                    {
                                        T miDato = aEliminar.valor;
                                        aEliminar.valor = siguiente.valor;
                                        padre.izquierdo = null;
                                        return miDato;
                                    }

                                }
                                else
                                {
                                    siguiente.izquierdo = aEliminar.izquierdo;

                                    if (padre != null)
                                    {
                                        if (EsHijoIzquierdo)
                                            padre.izquierdo = aEliminar.derecho;
                                        else
                                            padre.derecho = aEliminar.derecho;
                                    }
                                    else //Es la raiz
                                    {
                                        if (EsHijoIzquierdo)
                                            _raiz = aEliminar.derecho as ArbolBinarioBase<T>;
                                        else
                                            _raiz = aEliminar.derecho as ArbolBinarioBase<T>;
                                    }


                                    return aEliminar.valor;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.derecho == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                padre = siguiente;
                                EsHijoIzquierdo = false;
                                siguiente = siguiente.derecho as ArbolBinarioBase<T>;
                            }

                        }
                        else //menor que 0
                        {
                            if (siguiente.izquierdo == null)
                            {
                                return default(T);
                            }
                            else
                            {
                                padre = siguiente;
                                EsHijoIzquierdo = true;
                                siguiente = siguiente.izquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún valor.

            return default(T);
        }

        /// <summary>
        /// Busca si existe un conjunto de datos o valor determinado.
        /// </summary>
        /// <param name="valor">El nodo del arbol a buscar</param>
        /// <returns>Verdadero si encontró dicho nodo.</returns>
        public bool ExisteElemento(T valor)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (Equals(valor, default(T)))
                throw new ArgumentNullException("La llave enviada no es valida");

            if (_raiz == null)
                return false;
            else
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                K llaveBuscar = this.FuncionObtenerLlave(valor);
                bool encontrado = false;

                while (!encontrado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);

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
                            if (siguiente.derecho == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.derecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.izquierdo == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.izquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún valor.
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
        /// Inserta un valor en su posición especifica cumpliendo con las reglas de los Arboles binarios de búsqueda.
        /// </summary>
        /// <param name="valor">El valor que se desea insertar.</param>
        public void Insertar(T valor)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
                throw new Exception("No se han inicializado las funciones para operar la estructura");

            if (valor == null)
                throw new ArgumentNullException("El valor ingresado está vacio");

            if (_raiz == null)
                _raiz = new ArbolBinarioBase<T>(valor);
            else
            {
                ArbolBinarioBase<T> siguiente = _raiz;
                K llaveInsertar = this.FuncionObtenerLlave(valor);
                bool yaInsertado = false;

                while (!yaInsertado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llaveInsertar, llaveSiguiente);

                    if (comparacion == 0)
                    {
                        throw new Exception("El valor ingresado posee una llave que ya existe en la estructura");
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.derecho == null)
                            {
                                siguiente.derecho = new ArbolBinarioBase<T>(valor);
                                yaInsertado = true;
                            }
                            else
                            {
                                siguiente = siguiente.derecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.izquierdo == null)
                            {
                                siguiente.izquierdo = new ArbolBinarioBase<T>(valor);
                                yaInsertado = true;
                            }
                            else
                            {
                                siguiente = siguiente.izquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo
            }
        }

        /// <summary>
        /// Verifica si existe un nodo del arbol a través de su llave primaria
        /// </summary>
        /// <param name="llave">La llave primaria del valor buscado.</param>
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
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);

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
                            if (siguiente.derecho == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.derecho as ArbolBinarioBase<T>;
                            }

                        }
                        else
                        {
                            if (siguiente.izquierdo == null)
                            {
                                return false;
                            }
                            else
                            {
                                siguiente = siguiente.izquierdo as ArbolBinarioBase<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún valor.
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
            miLista.Agregar(arbol.valor);
        }
        #endregion

    }
}
