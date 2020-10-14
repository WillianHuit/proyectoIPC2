using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        public IActionResult Juego()
        {
           
            /*Filas,Columnas*/
            /*tablero[2,4]= 3;*/
            Juego jg = new Juego();
            jg.tablero = jg.obtenerTablero();
            return View(jg);
        }
        [HttpPost]
        
        [HttpPost]
        public IActionResult Juego(string archivoXML,string jugadorA, string jugadorB, int idJA, int idJB)
        {

            /*Filas,Columnas*/
            /*tablero[2,4]= 3;*/
            if (archivoXML == "noXML")
            {
                Juego jg = new Juego();
                jg.tablero = jg.obtenerTablero();
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
                return View(jg);

            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
