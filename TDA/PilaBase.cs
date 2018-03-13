using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDA
{
    public class PilaBase<T> : IPila<T>
    {
        ILista<T> _lista = new ListaBase<T>();

        #region IPila Members

        public void Push(T item)
        {
            _lista.Agregar(item);
        }

        public T Pop()
        {
            if (this.Length <= 0)
            {
                throw new Exception("Stack underflow");
            }

            // return _lista.Remover(_lista.Longitud - 1);
            return _lista.Remover(this.Length - 1);
        }

        public T Peek()
        {
            return _lista[this.Length - 1];
        }

        public int Length
        {
            get { return _lista.Longitud; }
        }

        public bool IsEmpty
        {
            get
            {
                //return _lista.ListaVacia;
                return this.Length <= 0;
            }
        }

        public void Clear()
        {
            _lista.Limpiar();
        }

        #endregion
    }
}
