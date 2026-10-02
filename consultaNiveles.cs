/*
 * Creado por SharpDevelop.
 * Usuario: MEL
 * Fecha: 1/10/2026
 * Hora: 22:02
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace claseNiveles.cs
{
	public List<List<string>> ConsultaNiveles(ArbolGeneral<ItemCat> arbol)
	{
		List<List<string>> resultado = new List<List<string>>();
		if (arbol == null || arbol.Vacio()) 
			return resultado;
		
		Queue<ArbolGeneral<ItemCat>> cola = new Queue<ArbolGeneral<ItemCat>>();
		cola.Enqueue(arbol);
		
		while (cola.Count > 0)
		{
			int nivelCantidad = cola.Count;
			List<string> nivelActual = new List<string>();
			
			for (int i = 0; i < nivelCantidad; i++)
			{
				var nodo = cola.Dequeue();
				nivelActual.Add(nodo.getDato().Nombre);
				
				foreach (var hijo in nodo.getHijos())
				{
					if (hijo != null && !hijo.Vacio())
					{
						cola.Enqueue(hijo);
					}
				}
			}
			resultado.Add(nivelActual);
		}
		return resultado;
	}
		
		}
	
