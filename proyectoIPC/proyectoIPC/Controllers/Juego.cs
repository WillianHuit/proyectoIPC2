using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace proyectoIPC.Controllers
{
    public class Juego
    {
        public int[,] tablero { get; set; }

        public string siguiente { get; set; }
        public string jugadorIA { get; set; }
        public string turnoIA { get; set; }
        public string jugadorA { get; set; }
        public string jugadorB { get; set; }
        public int idJugadorA { get; set; }
        public int idJugadorB { get; set; }

        public int[,] obtenerTablero()
        {
            int[,] tablero = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tablero[i, j] = 0;
                }
            }
            int fila, columna, color = 0;
            
            string cadena = "<tablero><ficha><color>blanco</color><columna>D</columna><fila>4</fila></ficha><ficha><color>negro</color><columna>E</columna><fila>4</fila></ficha><ficha><color>blanco</color><columna>E</columna><fila>5</fila></ficha><ficha><color>negro</color><columna>D</columna><fila>5</fila></ficha><siguienteTiro><color> negro </color></siguienteTiro></tablero>";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(cadena);
            /*xml.LoadXml("C:/Users/Willian Huit/Desktop/carga.xml");*/
            XmlNodeList xnList = xml.SelectNodes("/tablero/ficha");

            foreach (XmlNode xn in xnList)
            {
                fila = obtenerFila(xn["fila"].InnerText);
                columna = obtenerColumna(xn["columna"].InnerText);
                color = obtenerColor(xn["color"].InnerText);
                if (fila<8 && columna<8) {
                    tablero[fila, columna] = color;
                }
            }
            return tablero;
        }
        public int[,] tableroReglas() {
            return null;
        }
        private int obtenerFila(string fila) {
            int retornar = Int32.Parse(fila) - 1;
            return retornar;
        }

        public string obtenerColorTurno(string archivoXML) {
            string turno = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(archivoXML);
            /*xml.LoadXml("C:/Users/Willian Huit/Desktop/carga.xml");*/
            XmlNodeList xnList = xml.SelectNodes("/tablero/siguienteTiro");
            foreach (XmlNode xn in xnList)
            {
                turno = xn["color"].InnerText;
            }
            return turno;
        }
        internal int[,] obtenerTablero(string archivoXML)
        {
            int[,] tablero = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tablero[i, j] = 0;
                }
            }
            int fila, columna, color = 0;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(archivoXML);
            /*xml.LoadXml("C:/Users/Willian Huit/Desktop/carga.xml");*/
            XmlNodeList xnList = xml.SelectNodes("/tablero/ficha");

            foreach (XmlNode xn in xnList)
            {
                fila = obtenerFila(xn["fila"].InnerText);
                columna = obtenerColumna(xn["columna"].InnerText);
                color = obtenerColor(xn["color"].InnerText);
                tablero[fila, columna] = color;
            }
            return tablero;
        }
        
        private int obtenerColumna(string columna)
        {
            int retornar = 0;
            if (columna == "A")
            {
                retornar = 0;
            }
            else if (columna == "B")
            {
                retornar = 1;
            }
            else if (columna == "C")
            {
                retornar = 2;
            }
            else if (columna == "D")
            {
                retornar = 3;
            }
            else if (columna == "E")
            {
                retornar = 4;
            }
            else if (columna == "F")
            {
                retornar = 5;
            }
            else if (columna == "G")
            {
                retornar = 6;
            }
            else if (columna == "H")
            {
                retornar = 7;
            }
            else {
                retornar = 8;
            }
            return retornar;
        }
        private int obtenerColor(string color) {
            int retornar = 0;
            if (color == "negro")
            {
                retornar = 1;
            }
            else if (color == "blanco")
            {
                retornar = 2;
            }
            else {
                retornar = 3;
            }
            return retornar;
        }
    }
    
}
