using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TercerProyecto
{
    public class Circulo
    {
        #region Variables
        /*
        private Color myColor;
		private string Nombre;
        private Point Posicion; 
		private int Tamanio;
        #endregion

        #region Miembros

        /// <summary>
        /// Método que dibuja un circulo.
        /// </summary>
        /// <param name="Cadena">El texto que mostrará</param>
        /// <param name="PosX">La posición en el eje X del contenedor</param>
        /// <param name="PosY">La posición en el eje Y del contenedor</param>
        public Circulo(string Cadena, int PosX, int PosY)
        {
			Nombre = Cadena;
            myColor = Color.Navy;
            Posicion = new Point(PosX, PosY);
            Tamanio = 20;
		}

		public void Dibujar(Graphics Canvas)
		{
			Brush brush = new SolidBrush(myColor);
			Brush brushstring = new SolidBrush(Color.White);
			Font fuente = new Font("Arial",10, FontStyle.Bold);
			RectangleF rec = new RectangleF(Posicion.X, Posicion.Y, 40, 20);
			Canvas.FillEllipse(brush, rec);
			Canvas.DrawString(Nombre,fuente,brushstring,Posicion.X + 1, Posicion.Y + 3);
		}//Fin del método dibujar
        */
        #endregion
    }

    public class Linea
	{/*
		static Point PosicionIni = new Point();
		static Point PosicionFin = new Point();
		Color myColor;

        public Linea(int PosXI, int PosYI, int PosXF, int PosYF)
        {
            PosicionIni.X = PosXI;
            PosicionIni.Y = PosYI;
            PosicionFin.X = PosXF;
            PosicionFin.Y = PosYF;
            myColor = Color.DarkBlue;

        }

		public void Dibujar (Graphics Canvas){
			Pen Lapiz = new Pen(myColor);
			Canvas.DrawLine(Lapiz, PosicionIni.X, PosicionIni.Y, PosicionFin.X, PosicionFin.Y);
		}
        */
	}
}
