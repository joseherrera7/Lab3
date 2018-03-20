using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;
using TDALibrary;

namespace Lab3.DBContext
{
    public class DefaultConnection<T>
    {
        private static volatile DefaultConnection<T> Instance;
        private static object syncRoot = new Object();
        public ArbolAVLBase<Partido, T> Arbolito = new ArbolAVLBase<Partido, T>();
        
        
        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection<T> getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection<T>();
                        }
                    }
                }
                return Instance;
            }
        }

    }
}