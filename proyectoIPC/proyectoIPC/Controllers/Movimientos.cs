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
	public class Movimientos : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		//public IActionResult validarMovimiento(string tableroOld, int fila, int columna, int x, int y, int ficha, int turno)
		public IActionResult validarMovimiento(string tableroOld, int fila, int columna, int x, int y, int ficha, int turno, string tipo,int tiros)
		{
			
				bool winner = false;
				string mensaje = "";
				bool passTurn = false;
				int turnoSiguiente = -1;
				int contador = 0;
				string[] tableroNew = tableroOld.Split(",");
				int[,] tablero = new int[x, y];
				for (int i = 0; i < x; i++)
				{
					for (int j = 0; j < y; j++)
					{
						tablero[i, j] = Int32.Parse(tableroNew[contador]);
						contador++;
					}
				}
				if (turno == 0)
				{
					turnoSiguiente = 1;
				}
				else {
					turnoSiguiente = 0;
				}
				try { tablero = vertical(tablero, fila, columna, x, y, ficha, turno); } catch { }
				try { tablero = horizontal(tablero, fila, columna, x, y, ficha, turno); } catch { }
				try { tablero = diagonalD(tablero, fila, columna, x, y, ficha, turno); } catch { }
				try { tablero = diagonalI(tablero, fila, columna, x, y, ficha, turno); } catch { }
				try { tablero = validarMovimiento(tablero, x, y, ficha); } catch { }
				string ganador = obtenerGanador(tablero, tipo, x, y);
				if (ganador == "pasarTurno")
				{
					passTurn = true;
					mensaje = "El siguiente jugador ya no dispone de movimientos validos, vuelve a lanzar";
				try { tablero = vertical(tablero, fila, columna, x, y, ficha, turnoSiguiente); } catch { }
				try { tablero = horizontal(tablero, fila, columna, x, y, ficha, turnoSiguiente); } catch { }
				try { tablero = diagonalD(tablero, fila, columna, x, y, ficha, turnoSiguiente); } catch { }
				try { tablero = diagonalI(tablero, fila, columna, x, y, ficha, turnoSiguiente); } catch { }
				try { tablero = validarMovimiento(tablero, x, y, ficha); } catch { }
				string ganadorB = obtenerGanador(tablero, tipo, x, y);
					if (ganadorB != "seguir") {
						winner = true;
						if (ganadorB == "pasarTurno") {
							mensaje = "Los jugadores ya no tienen tiros disponibles se ha seleccionado un ganador";
							ganadorB = escogerGanador(tablero, tipo, x, y);
							ganador = ganadorB;
						}
					}
				}
				else if (ganador == "seguir")
				{
					winner = false;
				}
				else {
					winner = true;
				}
				if (tiros >= 3)
				{//Jugando
					return Json(new { status = true, message = mensaje, tabla = tablero, win = winner, winName = ganador, nextTurn = passTurn, sigT = turnoSiguiente });
				}
				else {//Apertura
					return Json(new { status = false, message = "guardado", tabla = tablero });
				}
		}
		public string escogerGanador(int[,] tablero, string tipo, int x, int y) {
			string ganador = "seguir";
			int blanco = 0;
			int negro = 0;
			int gris = 0;
			int espacio = 0;
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					if (tablero[i, j] == 1)
					{
						negro++;
					}
					else if (tablero[i, j] == 2)
					{
						blanco++;
					}
					else if (tablero[i, j] == 3)
					{
						gris++;
					}
					else
					{
						espacio++;
					}
				}
			}
		
			if (tipo == "Normal")
			{
				if (negro > blanco)
				{
					ganador = "negro";
				}
				else if (negro < blanco)
				{
					ganador = "blanco";
				}
				else if (negro == blanco)
				{
					ganador = "empate";
				}
			}
			else if (tipo == "Inverso")
			{
				if (negro < blanco)
				{
					ganador = "negro";
				}
				else if (negro > blanco)
				{
					ganador = "blanco";
				}
				else if (negro == blanco)
				{
					ganador = "empate";
				}
			}
			
			return ganador;
		}
		public string obtenerGanador(int[,] tablero, string tipo, int x, int y) {
			string ganador = "seguir";
			int blanco = 0;
			int negro = 0;
			int gris = 0;
			int espacio = 0;
			for (int i = 0;i<x;i++) {
				for (int j=0;j<y;j++) {
					if (tablero[i, j] == 1)
					{
						negro++;
					}
					else if (tablero[i, j] == 2)
					{
						blanco++;
					}
					else if (tablero[i, j] == 3)
					{
						gris++;
					}
					else {
						espacio++;
					}
				}
			}
			if (gris == 0 && espacio == 0) {
				if (tipo == "Normal") {
					if (negro > blanco) {
						ganador = "negro";
					} else if (negro < blanco)
					{
						ganador = "blanco";
					} else if (negro == blanco)
					{
						ganador = "empate";
					}
				} else if (tipo == "Inverso") {
					if (negro < blanco)
					{
						ganador = "negro";
					}
					else if (negro > blanco)
					{
						ganador = "blanco";
					}
					else if (negro == blanco)
					{
						ganador = "empate";
					}
				}
			} else if (gris == 0 && espacio > 0) {
				ganador = "pasarTurno";
			}
			return ganador;
		}
		public int[,] vertical(int[,] tablero, int fila, int columna, int inX, int inY, int ficha, int turno)
		{
			// ------------ Hacia abajo -----------------
			int abajo = fila;
			for (int i = fila; i < inX; i++)
			{
				if (tablero[i,columna] == 0)
				{
					break;
				}
				else if (tablero[i,columna] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					abajo = i + 1;
					//console.log("En donde:"+i+" Turno:"+turno)
				}
			}
			for (int i = fila; i < abajo; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[i,columna] = 1;

				}
				else
				{
					//element.className = "boton bg-light"
					tablero[i,columna] = 2;

				}
			}
			//---------------Hacia arriba --------------------
			int arriba = fila;
			for (int i = fila; i > -1; i--)
			{
				if (tablero[i,columna] == 0)
				{
					break;
				}
				else if (tablero[i,columna] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					arriba = i + 1;
					//console.log("En donde:"+i+" Turno:"+turno)
				}
			}
			for (int i = arriba; i < fila; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[i,columna] = 1;

				}
				else
				{
					//element.className = "boton bg-light"
					tablero[i,columna] = 2;

				}
			}
			return tablero;
			//return x * x;
		}

		public int[,] horizontal(int[,] tablero, int fila, int columna, int inX, int inY, int ficha, int turno)
		{
			// ------------------ Hacia la derecha ------------------
			int derecha = columna;
			for (int i = columna; i < inY; i++)
			{
				if (tablero[fila,i] == 0)
				{
					break;
				}
				else if (tablero[fila,i] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					derecha = i + 1;
					//console.log("En donde:"+i+" Turno:"+turno)
				}
			}
			for (int i = columna; i < derecha; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[fila,i] = 1;

				}
				else
				{
					//element.className = "boton bg-light"
					tablero[fila,i] = 2;

				}
			}
			// ------------------ Hacia la izquierda ------------------
			int izquierda = columna;
			for (int i = columna; i > -1; i--)
			{
				if (tablero[fila,i] == 0)
				{
					break;
				}
				else if (tablero[fila,i] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					derecha = i + 1;
					//console.log("En donde:"+i+" Turno:"+turno)
				}
			}
			for (int i = derecha; i < columna; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[fila,i] = 1;

				}
				else
				{
					//element.className = "boton bg-light"
					tablero[fila,i] = 2;

				}
			}
			return tablero;
		}
		public int[,] diagonalD(int[,] tablero, int fila, int columna, int inX, int inY, int ficha, int turno)
		{
			int abajo = fila;
			int contador = 0;
			for (int i = fila; i < 8; i++)
			{
				if (tablero[i,columna - contador] == 0)
				{
					break;
				}
				else if (tablero[i,columna - contador] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					//console.log("Encontro: "+i+""+(columna-contador))
					abajo = i;
					//console.log("En donde:"+i+" Turno:"+turno)
				}
				contador++;
			}
			//console.log("El: "+fila+""+columna+" finaliza: "+abajo)
			contador = 0;
			for (int i = fila; i < abajo; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[i,columna - contador] = 1;

				}
				else
				{
					//element.className = "boton bg-light"
					tablero[i,columna - contador] = 2;

				}
				contador++;
			}
			// ---------------------- Hacia Abajo -------------------
			int arriba = fila;
			int arribaB = columna;
			contador = 0;
			for (int i = fila; i > -1; i--)
			{
				//console.log("Busca: "+i+""+(columna+contador))
				if (tablero[i,columna + contador] == 0)
				{
					break;
				}
				else if (tablero[i,columna + contador] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					//console.log("Encontro: "+i+""+(columna+contador))
					arriba = i;
					arribaB = columna + contador;
				//console.log("En donde:"+i+" Turno:"+turno)
				}
				contador++;
			}
			//console.log("El: "+fila+""+columna+" finaliza: "+arriba)
			contador = 0;
			for (int i = arriba; i < fila; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[i,arribaB - contador] = 1;


		}
				else
				{
					//element.className = "boton bg-light"
					tablero[i,arribaB - contador] = 2;


		}
				contador++;
			}
			return tablero;
		}
		public int[,] diagonalI(int[,] tablero, int fila, int columna, int inX, int inY, int ficha, int turno)
		{
			int abajo = fila;
			int contador = 0;
			for (int i = fila; i < 8; i++)
			{
				if (tablero[i,columna + contador] == 0)
				{
					break;
				}
				else if (tablero[i,columna + contador] == ficha)
				{
					abajo = i;
				}
				contador++;
			}
			contador = 0;
			for (int i = fila; i < abajo; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[i,columna + contador] = 1;


		}
				else
				{
					//element.className = "boton bg-light"
					tablero[i,columna + contador] = 2;


		}
				contador++;
			}
			//-------------------------- Hacia abajo -------------------------
			int arriba = fila;
			int arribaB = columna;
			contador = 0;
			for (int i = fila; i > -1; i--)
			{
				if (tablero[i,columna - contador] == 0)
				{
					break;
				}
				else if (tablero[i,columna - contador] == ficha)
				{
					//console.log("valor:"+tablero[i,columna])
					//console.log("Encontro: "+i+""+(columna-contador))
					arriba = i;
					arribaB = columna - contador;
					//console.log("En donde:"+i+" Turno:"+turno)
				}
				contador++;
			}
			contador = 0;
			for (int i = arriba; i < fila; i++)
			{
				if (turno == 1)
				{
					//element.className = "boton bg-dark"
					tablero[i,arribaB + contador] = 1;

				}
				else
				{
					//element.className = "boton bg-light"
					tablero[i,arribaB + contador] = 2;

				}
				contador++;
			}
			return tablero;
		}
		private int[,] validarMovimiento(int[,] tablero, int x, int y, int ficha)
		{
			
			string[] posiciones = findPosiciones(tablero,x,y,ficha);
			for (var i = 0; i < posiciones.Length; i++)
			{
				tablero = buscarCoincidencias(posiciones[i], tablero,x,y,ficha);
			}
			return tablero;
		}

		private int[,] buscarCoincidencias(string posicionStr, int[,] tablero, int x, int y,int ficha)
		{
			string [] posicion = posicionStr.Split(",");
			int fila = Int32.Parse(posicion[0]);
			int columna = Int32.Parse(posicion[1]);
			try { tablero = verticalV(fila, columna, tablero, x, y, ficha); } catch { }
			try { tablero = horizontalV(fila, columna, tablero, x, y, ficha); } catch { }
			try { tablero = cruzado(fila, columna, tablero, x, y, ficha); } catch { }
			
			
			
			return tablero;
		}


		private int[,] verticalV(int fila, int columna, int[,] tablero,int x, int y, int ficha)
		{
			// ------------ Hacia abajo -----------------
			int abajo = fila;
			for (int i = fila; i < x; i++)
			{
				if (tablero[i,columna] == 0 || tablero[fila,i] == 3)
				{
					break;
				}
				else if (tablero[i,columna] == ficha)
				{
					abajo = i;
				}
			}
			if (abajo < 7 && abajo != fila && tablero[abajo + 1,columna] != 1 && tablero[abajo + 1,columna] != 2)
			{
				if (tablero[abajo + 1,columna] == 0)
				{
					tablero[abajo + 1,columna] = 3;
				}
			}
			//------------ Hacia arriba ------------------
			abajo = fila;
			for (int i = fila; i > 0; i--)
			{
				if (tablero[i,columna] == 0 || tablero[fila,i] == 3)
				{
					break;
				}
				else if (tablero[i,columna] == ficha)
				{
					abajo = i;
				}
			}
			if (abajo > 0 && abajo != fila && tablero[abajo - 1,columna] != 1 && tablero[abajo - 1,columna] != 2)
			{
				if (tablero[abajo - 1,columna] == 0)
				{
					tablero[abajo - 1,columna] = 3;
				}
			}
			return tablero;
		}
		private int[,] horizontalV(int fila, int columna, int[,] tablero, int x, int y, int ficha)
		{
			// -------------------- Derecha ---------------------
			int derecha = columna;
			for (int i = columna; i < y; i++)
			{
				if (tablero[fila,i] == 0 || tablero[fila,i] == 3)
				{
					break;
				}
				else if (tablero[fila,i] == ficha)
				{
					derecha = i;
				}
			}
			if (derecha < 7 && derecha != columna)
			{
				if (tablero[fila,derecha + 1] == 0)
				{
					tablero[fila,derecha + 1] = 3;
				}
			}
			// ------------------- Izquierda ---------------------
			var izquierda = columna;
			for (int i = columna; i > 0; i--)
			{
				if (tablero[fila,i] == 0 || tablero[fila,i] == 3)
				{
					break;
				}
				else if (tablero[fila,i] == ficha)
				{
					izquierda = i;
				}
			}
			if (izquierda > 0 && izquierda != columna)
			{
				if (tablero[fila,izquierda - 1] == 0)
				{
					tablero[fila,izquierda - 1] = 3;
				}
			}

			return tablero;
		}
		private int[,] cruzado(int fila, int columna, int[,] tablero, int x, int y, int ficha)
		{
			// ------------------Diagonal Derecha hacia abajo [\]-------------------
			int abajo = fila;
			int abajoB = fila;
			int contador = 0;
			for (int i = fila; i < x; i++)
			{
				if (tablero[i,columna + contador] == 0 || tablero[i,columna + contador] == 3)
				{
					break;
				}
				else if (tablero[i,columna + contador] == ficha)
				{
					abajo = i;
					abajoB = columna + contador;
				}
				contador++;
			}
			if (abajo < 7 && abajo != fila)
			{
				if (tablero[abajo + 1,abajoB + 1] == 0)
				{
					tablero[abajo + 1,abajoB + 1] = 3;
				}
			}
			// ------------------Diagonal Derecha hacia arriba [\]-------------------
			int arriba = fila;
			int arribaB = fila;
			contador = 0;
			for (int i = fila; i > 0; i--)
			{
				if (tablero[i,columna - contador] == 0 || tablero[i,columna - contador] == 3)
				{
					break;
				}
				else if (tablero[i,columna - contador] == ficha)
				{
					arriba = i;
					arribaB = columna - contador;
				}
				contador++;
			}
			if (arriba > 0 && arriba != fila)
			{
				if (tablero[arriba - 1,arribaB - 1] == 0)
				{
					tablero[arriba - 1,arribaB - 1] = 3;
				}
			}
			// ------------------Diagonal Derecha hacia abajo [/]-------------------
			abajo = fila;
			abajoB = fila;
			contador = 0;
			for (int i = fila; i < x; i++)
			{
				if (tablero[i,columna - contador] == 0 || tablero[i,columna - contador] == 3)
				{
					break;
				}
				else if (tablero[i,columna - contador] == ficha)
				{
					abajo = i;
					abajoB = columna - contador;
				}
				contador++;
			}
			if (abajo < 7 && abajo != fila)
			{
				if (tablero[abajo + 1,abajoB - 1] == 0)
				{
					tablero[abajo + 1,abajoB - 1] = 3;
				}
			}
			// ------------------Diagonal Derecha hacia arriba [/]-------------------
			 arriba = fila;
			 arribaB = fila;
			 contador = 0;
			for (int i = fila; i > 0; i--)
			{
				if (tablero[i,columna + contador] == 0 || tablero[i,columna + contador] == 3)
				{
					break;
				}
				else if (tablero[i,columna + contador] == ficha)
				{
					arriba = i;
					arribaB = columna + contador;
				}
				contador++;
			}
			if (arriba > 0 && arriba != fila)
			{
				if (tablero[arriba - 1,arribaB + 1] == 0)
				{
					tablero[arriba - 1,arribaB + 1] = 3;
				}
			}
			/*var abajo = fila;
			var abajoB = fila;
			var contador = 0;
			for(i = fila;i<8;i++){
				if(tablero[i,columna+contador]==0 || tablero[fila,i]==3){
					break;
				}else if(tablero[i,columna+contador]==ficha){
					abajo = i;
					abajoB = columna+contador;
				}
				contador++;
			}
			if(abajo<7 && abajo!=fila){
				tablero[abajo+1,abajoB+1] = 3;
			}*/

			return tablero;
		}
		private string[] findPosiciones(int[,] tablero, int x, int y,int ficha)
		{
			//ficha
			var nextFicha = 3;
			if (ficha == 1)
			{
				nextFicha = 2;
			}
			else if (ficha == 2)
			{
				nextFicha = 1;
			}
			List<string> posiciones = new List<string>();
			int contador = 0;
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					if (tablero[i,j] == nextFicha)
					{
						posiciones.Add(i + "," + j);
						contador++;
					}
				}
			}

			return posiciones.ToArray();
		}
	}
	}
