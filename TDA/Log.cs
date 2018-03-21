using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TDA
{
    public class Log
    {

       
        

        public void Logg(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public void Insercion(StreamWriter w, StreamReader r)
        {
            Logg("Insercion", w);
            DumpLog(r);
        }
        public void Eliminacion(StreamWriter w, StreamReader r)
        {
            Logg("Eliminacion", w);
            DumpLog(r);
        }
        public void Balanceo(StreamWriter w, StreamReader r)
        {
            Logg("Balanceo", w);
            DumpLog(r);
        }
    }
}