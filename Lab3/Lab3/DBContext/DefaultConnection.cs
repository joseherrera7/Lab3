using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;
using TDALibrary;

namespace Lab3.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();
       
        public ArbolAVLBase<Partido, int> Arbolito = new ArbolAVLBase<Partido, int>();
        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }

    }
}