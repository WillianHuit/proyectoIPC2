using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proyectoIPC.Models;

namespace proyectoIPC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
       
        public IActionResult Register() {
            return View();
        }
        public IActionResult Inicio() {
            return View();
        }
        [HttpPost]
        public IActionResult FormJugador(int id, string userName, int idJugadoB)
        {
            Juego jg = new Juego();
            jg.idJugadorA = id;
            jg.jugadorA = userName;
            jg.idJugadorB = idJugadoB;
            return View(jg);
        }
        [HttpGet]
        public IActionResult Juego()
        {
            int fila = 8;
            int columna = 8;
            /*Filas,Columnas*/
            /*tablero[2,4]= 3;*/
            Juego jg = new Juego();
            jg.tablero = jg.obtenerTablero(fila,columna);
            return View(jg);
        }

          [HttpPost]
        //string archivoXML,string jugadorA, string jugadorB, int idJA, int idJB
        public IActionResult Juego(Microsoft.AspNetCore.Http.IFormCollection collection)
        {
            string[] letras = getLetters();
            int fila = 8;
            int columna = 8;
            string tipo = "normal";
            fila = Int32.Parse(collection["filas"]);
            columna = Int32.Parse(collection["columnas"]);
            Juego jg = new Juego();
            jg.tablero = jg.obtenerTablero(fila, columna);
            jg.tablero[fila / 2-1, columna / 2-1] = 3;
            jg.tablero[fila / 2, columna / 2 - 1] = 3;
            jg.tablero[fila / 2 - 1, columna / 2] = 3;
            jg.tablero[fila / 2, columna / 2] = 3;
            tipo = collection["tipo"];
            jg.tipo = tipo;
            jg.letras = letras;
            jg.fila = fila;
            jg.columna = columna;
            jg.siguiente = collection["turno"];
            jg.colorsA = colorsPlayer(collection,"jugadorA");
            jg.colorsB = colorsPlayer(collection, "jugadorB");
            return View(jg);
           
            // es con el nombre string _nombre = formCollection["Nombre"];

            /*if (archivoXML == "noXML")
            {
                Juego jg = new Juego();
                jg.tablero = jg.obtenerTablero(fila, columna);
                jg.jugadorA = jugadorA;
                jg.jugadorB = jugadorB;
                jg.idJugadorA = idJA;
                jg.idJugadorB = idJB;
                jg.siguiente = "negro";
                if (jugadorB == "Maquina")
                {
                    jg.jugadorIA = "activar";
                    jg.turnoIA = "blanco";
                }
                if (jugadorA == "Maquina")
                {
                    jg.jugadorIA = "activar";
                    jg.turnoIA = "negro";

                }
                jg.letras = letras;
                jg.fila = fila;
                jg.columna = columna;
                jg.tipo = tipo;
                return View(jg);
            }else{ 
                Juego jg = new Juego();
                jg.tablero = jg.obtenerTablero(archivoXML);
                jg.jugadorA = jugadorA;
                jg.jugadorB = jugadorB;
                jg.idJugadorA = idJA;
                jg.idJugadorB = idJB;
                jg.siguiente = jg.obtenerColorTurno(archivoXML);
                if (jugadorB=="Maquina") {
                    jg.jugadorIA = "activar";
                    jg.turnoIA = "blanco";
                }
                if (jugadorA == "Maquina")
                {
                    jg.jugadorIA = "activar";
                    jg.turnoIA = "negro";

                }
                jg.letras = letras;
                jg.fila = fila;
                jg.columna = columna;
                jg.tipo = tipo;
                return View(jg);

            }*/
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private string colorsPlayer(Microsoft.AspNetCore.Http.IFormCollection collection,string jugador) {
            //jugadorA,jugadorB
            string[] colors = new String[5];
            int contador = 0;
            if (collection["FRojo"] !="" && contador<5) {
                if (collection["FRojo"] == jugador) {
                    colors[contador] = "bg-rojo";
                    contador++;
                }
            }
            if (collection["FAmarillo"] != "" && contador < 5)
            {
                if (collection["FAmarillo"] == jugador)
                {
                    colors[contador] = "bg-amarillo";
                    contador++;
                }
            }
            if (collection["FAzul"] != "" && contador < 5)
            {
                if (collection["FAzul"] == jugador)
                {
                    colors[contador] = "bg-azul";
                    contador++;
                }
            }
            if (collection["FNaranja"] != "" && contador < 5)
            {
                if (collection["FNaranja"] == jugador)
                {
                    colors[contador] = "bg-anaranjado";
                    contador++;
                }
            }
            if (collection["FVerde"] != "" && contador < 5)
            {
                if (collection["FVerde"] == jugador)
                {
                    colors[contador] = "bg-success";
                    contador++;
                }
            }
            if (collection["FVioleta"] != "" && contador < 5)
            {
                if (collection["FVioleta"] == jugador)
                {
                    colors[contador] = "bg-violeta";
                    contador++;
                }
            }
            if (collection["FBlanco"] != "" && contador < 5)
            {
                if (collection["FBlanco"] == jugador)
                {
                    colors[contador] = "bg-blanco";
                    contador++;
                }
            }
            if (collection["FNegro"] != "" && contador < 5)
            {
                if (collection["FNegro"] == jugador)
                {
                    colors[contador] = "bg-negro";
                    contador++;
                }
            }
            if (collection["FCeleste"] != "" && contador < 5)
            {
                if (collection["FCeleste"] == jugador)
                {
                    colors[contador] = "bg-celeste";
                    contador++;
                }
            }
            if (collection["FGris"] != "" && contador < 5)
            {
                if (collection["FGris"] == jugador)
                {
                    colors[contador] = "bg-secondary";
                    contador++;
                }
            }
            string realColor="";
            for (int i=0;i<contador;i++) {
                realColor = colors[i]+" "+realColor;
            }
            return realColor;
        }
        private string[] getLetters() {
            string[] letras = new String[20];

            letras[0]= "A";
            letras[1]= "B";
            letras[2]= "C";
            letras[3]= "D";
            letras[4]= "E";
            letras[5]= "F";
            letras[6]= "G";
            letras[7]= "H";
            letras[8]= "I";
            letras[9]= "J";
            letras[10]= "K";
            letras[11]= "L";
            letras[12]= "M";
            letras[13]= "N";
            letras[14]= "O";
            letras[15]= "P";
            letras[16]= "Q";
            letras[17]= "R";
            letras[18]= "S";
            letras[19]= "T";
            return letras;
        }
    }
}
