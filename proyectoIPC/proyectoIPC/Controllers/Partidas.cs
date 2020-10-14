using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoIPC.Models;

namespace proyectoIPC.Controllers
{
    public class Partidas : Controller
    {
        public othelloDataBaseContext _context;
        public Partidas(othelloDataBaseContext master)
        {
            this._context = master;
        }
        [HttpPost]
        public IActionResult guardarPartida(int Anfitrion, int JugadorA, int JugadorB, int Ganador)
        {
            try
            {
                 Partida registro = new Partida();
                 registro.JugadorA = JugadorA;
                 registro.JugadorB = JugadorB;
                 registro.Ganador = Ganador;
                 _context.Partida.Add(registro);
                 _context.SaveChanges();
                return Json(new { status = true, message = "guardado" });
            }
            catch {
                return Json(new { status = false, message = "guardado" });
            }
        }
    }
}
