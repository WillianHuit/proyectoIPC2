using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoIPC.Models;


namespace proyectoIPC.Controllers
{
    public class Usuarios : Controller
    {

        public othelloDataBaseContext _context;
        public Usuarios(othelloDataBaseContext master) {
            this._context = master;
        }
        [HttpPost]
        public IActionResult GetUsuarios(String nombreUsuario, String claveUsuario) {
            var usuario = _context.Usuario.Where(s=> s.Usr == nombreUsuario && s.Pass == claveUsuario);
            if (usuario.Any())
            {
                if (usuario.Where(s=> s.Usr == nombreUsuario && s.Pass == claveUsuario).Any())
                {
                    return Json(new { status = true, message = "Bienvenido" });
                }
                else {
                    return Json(new { status = false, message = "clave incorrecto" });
                }
            }
            else {
                return Json(new { Status = false, message = "clave incorrecto" });
            }
        }
        [HttpPost]
        public IActionResult insertarUsuario(String nombre, String apellido, String usr, String password, String pais, String fecha, String correo) {
            Usuario registro = new Usuario();
            registro.Nombre = nombre;
            registro.Apellido = apellido;
            registro.Usr = usr;
            registro.Pass = password;
            registro.Pais = pais;
            registro.Fecha = fecha;
            registro.Correo = correo;
            _context.Usuario.Add(registro);
            _context.SaveChanges();
            return Json(new { status = true, message = "registrado" });
        }
        public IActionResult setUsuario(String nombre, String apellido, String usuario, String password, String pais, String fecha, String correo) {
           

            return Json(new { Status = false, message = "clave incorrecto" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
