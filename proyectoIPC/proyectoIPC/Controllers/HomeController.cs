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
        public IActionResult Juego(string archivoXML)
        {

            /*Filas,Columnas*/
            /*tablero[2,4]= 3;*/
            Juego jg = new Juego();
            jg.tablero = jg.obtenerTablero(archivoXML);
            return View(jg);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
