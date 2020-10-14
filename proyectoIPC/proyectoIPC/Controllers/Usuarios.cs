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
                    var idUsuario = _context.Usuario.Where(u => u.Usr == nombreUsuario && u.Pass == claveUsuario).Select(u => u.Id);

                    return Json(new { status = true, message = "Bienvenido", idUsr = idUsuario});
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
        [HttpPost]
        public IActionResult generarXML(string cadena, int turno)
        {
            string[] lista = cadena.Split(",");
            //turno=1 es negro, turno = 2 es blanco
            //0 nada, 1 negro, 2 blanco, 3 gris(no se guarda)
            int turnoTemp = 1;
            int contador = 0;
            int[,] tableroPrueba = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tableroPrueba[i, j] = Int32.Parse(lista[contador]);
                    contador++;
                }
            }
            //Comentar lo de arriba una vez terminada la prueba -------------------------------------
            string archivoXML = "<tablero>";

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //Cambiar tableroPrueba -> tablero
                    if (tableroPrueba[i, j] != 0)
                    {
                        archivoXML = archivoXML + "<ficha>";
                        archivoXML = archivoXML + "  <color>" + obtenerColorStr(tableroPrueba[i, j]) + "</color>\n\r";
                        archivoXML = archivoXML + "  <columna>" + obtenerColumnaStr(j) + "</columna>\n\r";
                        archivoXML = archivoXML + "  <fila>" + obtenerFilaStr(i) + "</fila>\n\r";
                        archivoXML = archivoXML + "</ficha>";
                    }
                }
            }
            archivoXML = archivoXML + "</tablero>";
            return Json(new { textoXML = archivoXML });
        }

        private int obtenerFilaStr(int i)
        {
            return i + 1;
        }

        private string obtenerColumnaStr(int v)
        {
            string columna = "";
            switch (v)
            {
                case 0:
                    columna = "A";
                    break;
                case 1:
                    columna = "B";
                    break;
                case 2:
                    columna = "C";
                    break;
                case 3:
                    columna = "D";
                    break;
                case 4:
                    columna = "E";
                    break;
                case 5:
                    columna = "F";
                    break;
                case 6:
                    columna = "G";
                    break;
                case 7:
                    columna = "H";
                    break;

            }
            return columna;
        }

        private string obtenerColorStr(int v)
        {
            string color = "";
            if (v == 1)
            {
                color = "negro";
            }
            else if (v == 2)
            {
                color = "blanco";
            }

            return color;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
