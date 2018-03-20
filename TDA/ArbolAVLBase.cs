using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TDALibrary;

namespace TDA
{
    public class ArbolAVLBase<T, K> : ABinBusqueda<T, K>, IArbolAVL<T, K>
    {
        Log nuevoLog = new Log();
        StreamWriter w = File.AppendText("log.txt");
        StreamReader r = File.OpenText("log.txt");
        /// <summary>
        /// Properti que devuelve la Raíz de un árbol, es de solo lectura
        /// </summary>
        public ArbolBinarioBase<T> Raiz
        {
            get
            {
                return this._raiz;
            }
        }

        /// <summary>
        /// Busca, devuelve y elimina un nodo del arbol, teniendo cuidado de que el Arbol siga cumpliendo con las caracteristicas de que sea
        /// arbol AVL, el método de eliminación utilizado es reemplazando el menor del mayor
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
                ArbolBinarioBase<T> siguiente = _raiz; //Empiezo a verificar desde la raiz.
                ArbolBinarioBase<T> padre = null; //El padre de la raiz es nulo
                bool EsHijoIzquierdo = false; //La raiz no es ni izquierda ni derecha
                bool encontrado = false; //Asumo que no lo he encontrado

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
                                {
                                    padre.izquierdo = null;
                                }
                                else
                                {
                                    padre.derecho = null;
                                }
                                //EsHijoIzquierdo, se refiere al hijo que elimine
                                Equilibrar(padre, EsHijoIzquierdo, false); //Le paso el padre, es hijo izquierdo falso o verdadero, es nuevo = false

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
                                    {
                                        padre.izquierdo = siguiente.izquierdo;
                                        siguiente.izquierdo.Padre = padre;
                                    }
                                    else
                                    {
                                        padre.derecho = siguiente.izquierdo;
                                        siguiente.izquierdo.Padre = padre;
                                    }

                                    Equilibrar(padre, EsHijoIzquierdo, false);
                                }
                                else
                                {
                                    siguiente.izquierdo.Padre = null;
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
                                    {
                                        padre.izquierdo = siguiente.derecho;
                                        siguiente.derecho.Padre = padre;
                                    }
                                    else
                                    {
                                        padre.derecho = siguiente.derecho;
                                        siguiente.derecho.Padre = padre;
                                    }
                                    Equilibrar(padre, EsHijoIzquierdo, false);
                                }
                                else
                                {
                                    siguiente.derecho.Padre = null;
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
                                        if (siguiente.derecho == null)
                                        {
                                            padre.izquierdo = null;
                                            Equilibrar(padre, true, false);
                                        }
                                        else
                                        {
                                            padre.izquierdo = siguiente.derecho;
                                            siguiente.derecho.Padre = padre;
                                            Equilibrar(padre, true, false);
                                        }


                                        return miDato;
                                    }

                                }
                                else
                                {
                                    //Le estoy asignando un nuevo hijo a Siguiente
                                    siguiente.izquierdo = aEliminar.izquierdo;
                                    aEliminar.izquierdo.Padre = siguiente;
                                    siguiente.FactorBalance = aEliminar.FactorBalance;
                                    siguiente.Padre = aEliminar.Padre;

                                    if (padre != null)
                                    {
                                        if (EsHijoIzquierdo)
                                        {
                                            padre.izquierdo = aEliminar.derecho;
                                            Equilibrar(siguiente, false, false);
                                        }
                                        else
                                        {
                                            padre.derecho = aEliminar.derecho;
                                            Equilibrar(siguiente, false, false);
                                        }
                                    }
                                    else //Es la raiz
                                    {
                                        _raiz = aEliminar.derecho as ArbolBinarioBase<T>;
                                        Equilibrar(_raiz, false, false);
                                    }


                                    return aEliminar.valor;
                                }

                            }
                        }
                    }
                    else //La comparación dio un valor diferente de 0
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
                    }//Fin del if comparación

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún valor.
            nuevoLog.Eliminacion(w);
            return default(T);
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
                                siguiente.derecho.Padre = siguiente;
                                Equilibrar(siguiente, false, true); //es Izquierdo = false, es nuevo = true;
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
                                siguiente.izquierdo.Padre = siguiente;
                                Equilibrar(siguiente, true, true); //Es izquierdo = true, es nuevo true;
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
            nuevoLog.Insercion(w);
        }

        /// <summary>
        /// Procedimiento que sirva para equilibrar un arbol AVL despues de una insersión o una eliminación.
        /// </summary>
        /// <param name="nodo">El padre del nodo que se insertó</param>
        /// <param name="esIzquierdo">Si el nodo que se insertó es hijo izquierdo del padre entonces es verdadero de lo
        /// contrario es falso.</param>
        /// <param name="esNuevo">Si es una inserción entonces es verdadero, si el método es llamado después de una 
        /// eliminación entonces es falso.</param>
        internal void Equilibrar(IArbolBinario<T> nodo, bool esIzquierdo, bool esNuevo)
        {
            bool salir = false; //al terminar de recorrer una rama si no es necesario rotar entonces se convierte en verdadero.


            // Recorrer camino inverso actualizando valores de FE:
            while ((nodo != null) && !salir)
            {
                bool hizoRotacion = false; //Al inicio digo que no se rotó

                if (esNuevo)
                {
                    if (esIzquierdo)
                        nodo.FactorBalance--; // Estamos añidiendo
                    else
                        nodo.FactorBalance++;
                }
                else
                {
                    if (nodo.FactorBalance == 0)
                        salir = true;

                    if (esIzquierdo)
                        nodo.FactorBalance++; // Borrando un nodo
                    else
                        nodo.FactorBalance--;
                }

                if (nodo.FactorBalance == 0)
                    salir = true; // La altura de el arbol que empieza en nodo no ha variado, salir de Equilibrar

                //Si existe algún desbalance en esta porción de código se realizan las rotaciones
                else if (nodo.FactorBalance == -2)
                { // Rotación a la derecha doble o simple según sea el caso y salir.
                    if (nodo.izquierdo.FactorBalance == 1)
                    {
                        RDD(nodo); // Rotación doble
                        hizoRotacion = true;
                    }
                    else
                    {
                        RSD(nodo); // Rotación simple
                        hizoRotacion = true;
                    }
                    salir = true;
                }
                else if (nodo.FactorBalance == 2)
                {  // Rotar a la izquierda doble o simple según sea el caso y salir.
                    if (nodo.derecho.FactorBalance == -1)
                    {
                        RDI(nodo); // Rotación doble
                        hizoRotacion = true;
                    }
                    else
                    {
                        RSI(nodo); // Rotación simple
                        hizoRotacion = true;
                    }
                    salir = true;
                }

                if ((hizoRotacion) && (nodo.Padre != null) && (!esNuevo))
                    nodo = nodo.Padre;

                if (nodo.Padre != null)
                {
                    if (nodo.Padre.derecho == nodo)
                        esIzquierdo = false;
                    else
                        esIzquierdo = true;

                    if ((!esNuevo) && (nodo.FactorBalance == 0))
                        salir = false;

                }

                nodo = nodo.Padre; // Calcular Factor de balance, siguiente nodo del camino ossea el padre.
            }
            nuevoLog.Balanceo(w);
        }

        /// <summary>
        /// Rotación doble a la izquierda, se usa cuando la FE del nodo que recibe como parámetro es -2
        /// (Eso significa que su arbol izquierdo es mas grande que el derecho) y
        /// la raiz del subarbol iquierdo tenga FE 1
        /// </summary>
        /// <param name="nodo">El nodo con el desbalance</param>
        internal void RDD(IArbolBinario<T> nodo)
        {
            //Punteros necesarios para realizar la rotación
            IArbolBinario<T> Padre = nodo.Padre;
            IArbolBinario<T> P = nodo;
            IArbolBinario<T> Q = P.izquierdo;
            IArbolBinario<T> R = Q.derecho;
            IArbolBinario<T> B = R.izquierdo;
            IArbolBinario<T> C = R.derecho;

            //Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
            //o el izquierdo, si el padre es null, significa que es la raiz.
            if (Padre != null)
            {
                if (Padre.derecho == P)
                    Padre.derecho = R;
                else
                    Padre.izquierdo = R;
            }
            else
            {
                _raiz = R as ArbolBinarioBase<T>;
                _raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
            }

            // Reconstruir árbol:
            Q.derecho = B;
            P.izquierdo = C;
            R.izquierdo = Q;
            R.derecho = P;

            // Reasignar padres:
            R.Padre = Padre;
            P.Padre = Q.Padre = R;
            if (B != null)
                B.Padre = Q;
            if (C != null)
                C.Padre = P;


            // Ajustar valores de FE:
            switch (R.FactorBalance)
            {
                case -1:
                    {
                        Q.FactorBalance = 0;
                        P.FactorBalance = 1;
                    }
                    break;

                case 0:
                    {
                        Q.FactorBalance = 0;
                        P.FactorBalance = 0;
                    }
                    break;

                case 1:
                    {
                        Q.FactorBalance = -1;
                        P.FactorBalance = 0;
                    }
                    break;
            }
            R.FactorBalance = 0;
        }

        /// <summary>
        /// Rotación doble a la izquierda, se usa cuando la FE del nodo que recibe como parámetro es 2 y
        /// el subarbol derecho de este tiene una Fe de -1
        /// </summary>
        /// <param name="nodo">El nodo con el desbalance</param>
        internal void RDI(IArbolBinario<T> nodo)
        {
            //Punteros necesarios para realizar la rotación
            IArbolBinario<T> Padre = nodo.Padre;
            IArbolBinario<T> P = nodo;
            IArbolBinario<T> Q = nodo.derecho;
            IArbolBinario<T> R = Q.izquierdo;
            IArbolBinario<T> B = R.izquierdo;
            IArbolBinario<T> C = R.derecho;

            //Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
            //o el izquierdo, si el padre es null, significa que es la raiz.
            if (Padre != null)
            {
                if (Padre.derecho == P)
                    Padre.derecho = R;
                else
                    Padre.izquierdo = R;
            }
            else
            {
                _raiz = R as ArbolBinarioBase<T>;
                _raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
            }

            // Recontrucción del árbol 
            P.derecho = B;
            Q.izquierdo = C;
            R.izquierdo = P;
            R.derecho = Q;

            //Actualizando a los padres
            R.Padre = Padre;
            P.Padre = Q.Padre = R;
            if (B != null)
                B.Padre = P;
            if (C != null)
                C.Padre = Q;


            // Ajustar valores de Factores de Balance
            switch (R.FactorBalance)
            {
                case -1:
                    {
                        P.FactorBalance = 0;
                        Q.FactorBalance = 1;
                    }
                    break;

                case 0:
                    {
                        P.FactorBalance = 0;
                        Q.FactorBalance = 0;
                    }
                    break;

                case 1:
                    {
                        P.FactorBalance = -1;
                        Q.FactorBalance = 0;
                    }
                    break;
            }
            R.FactorBalance = 0;
        }

        /// <summary>
        /// Rotación simple a derecha, se usa cuando el FE de un arbol es -2
        /// </summary>
        /// <param name="nodo">El nodo padre que presenta el des-equilibrio</param>
        internal void RSD(IArbolBinario<T> nodo)
        {
            //Punteros necesarios para realizar la rotación.
            IArbolBinario<T> Padre = nodo.Padre; //Parde el árbol desequilibrado.
            IArbolBinario<T> P = nodo; //Nodo desequilibrado
            IArbolBinario<T> Q = P.izquierdo;
            IArbolBinario<T> B = Q.derecho; //Nodo con altura igual al hijo derecho de P

            //Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
            //o el izquierdo, si el padre es null, significa que es la raiz.
            if (Padre != null)
            {
                if (Padre.derecho == P)
                    Padre.derecho = Q;
                else
                    Padre.izquierdo = Q;
            }
            else
            {
                _raiz = Q as ArbolBinarioBase<T>;
                _raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
            }

            //Reconstruyo el arbol
            P.izquierdo = B;
            Q.derecho = P;

            //Actualizando los nuevos padres
            P.Padre = Q;
            if (B != null)
                B.Padre = P;
            Q.Padre = Padre;


            //Ajuste de valores del factor de balance
            if (Q.FactorBalance == 0)
            {
                P.FactorBalance = -1;
                Q.FactorBalance = 1;

            }
            else
            {
                P.FactorBalance = 0;
                Q.FactorBalance = 0;
            }

        }

        /// <summary>
        /// Rotación simple a Izquierda, se usa cuando el FE de un arbol es 2
        /// </summary>
        /// <param name="nodo">El nodo padre que presenta el des-equilibrio</param>
        internal void RSI(IArbolBinario<T> nodo)
        {
            //Punteros necesarios para realizar la rotación.
            IArbolBinario<T> Padre = nodo.Padre; //Padre del arbol desbalanceado
            IArbolBinario<T> P = nodo; //Arbol desbalanceado con FE 2
            IArbolBinario<T> Q = P.derecho;
            IArbolBinario<T> B = Q.izquierdo; //Nodo con altura igual a el hijo izquierdo de P

            //Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
            //o el izquierdo, si el padre es null, significa que es la raiz.
            if (Padre != null)
            {
                if (Padre.derecho == P)
                    Padre.derecho = Q;
                else
                    Padre.izquierdo = Q;
            }
            else
            {
                _raiz = Q as ArbolBinarioBase<T>;
                _raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
            }

            //Reconstruyo el padre, 
            P.derecho = B;
            Q.izquierdo = P;

            //Asignando nuevos padres
            P.Padre = Q;
            if (B != null)
                B.Padre = P;
            Q.Padre = Padre;

            //Ajusto valores del Factor de Balance
            if (Q.FactorBalance == 0)
            {
                P.FactorBalance = 1;
                Q.FactorBalance = -1;
            }
            else
            {
                P.FactorBalance = 0;
                Q.FactorBalance = 0;
            }
        }
    }
}
