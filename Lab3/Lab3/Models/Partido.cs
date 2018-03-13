using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class Partido
    {
        int NoPartido { get; set; }
        DateTime FechaPartido { get; set; }
        string Grupo { get; set; }
        string Pais1 { get; set; }
        string Pais2 { get; set; }
        string Estadio { get; set; }
    }
}