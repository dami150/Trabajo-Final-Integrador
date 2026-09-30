
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using tp1;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tpfinal
{

	public class Estrategia
	{
        public string GetUrlSeoPorId(ArbolGeneral<ItemCat> arbol, int id)
        {
            Stack<(ArbolGeneral<ItemCat> nodo, string camino)> pila = new Stack<(ArbolGeneral<ItemCat>, string)>();

            pila.Push((arbol, arbol.getDatoRaiz().Nombre));

            while (pila.Count > 0)
            {
                var actual = pila.Pop();

                if (actual.nodo.getDatoRaiz().Id == id)
                {
                    return actual.camino;
                }

                foreach (var hijo in actual.nodo.getHijos())
                {
                    string nuevoCamino = actual.camino + "/" + hijo.getDatoRaiz().Nombre;

                    pila.Push((hijo, nuevoCamino));
                }
            }

            return null;
        }
        

        public List<string> GetURLsSEO(ArbolGeneral<ItemCat> arbol)
        {
            List<string> urls = new List<string>();

            Stack<(ArbolGeneral<ItemCat> nodo, string camino)> pila = new Stack<(ArbolGeneral<ItemCat>, string)>();

            pila.Push((arbol, arbol.getDatoRaiz().Nombre));

            while (pila.Count > 0)
            {
                var actual = pila.Pop();

                if (actual.nodo.esHoja())
                {
                    urls.Add("tienda.com/" + actual.camino);
                }
                else
                {
                    foreach (var hijo in actual.nodo.getHijos())
                    {
                        string nuevoCamino = actual.camino + "/" + hijo.getDatoRaiz().Nombre;

                        pila.Push((hijo, nuevoCamino));
                    }
                }
            }

            return urls;
        }
            

        public List<List<string>> ConsultaNiveles(ArbolGeneral<ItemCat> arbol)
		{
            return [["Implementar"]];
        }


        public List<ItemCat> Todos(ArbolGeneral<ItemCat> arbol)
        {
            List<ItemCat> Arbol = new List<ItemCat>();

            Arbol.Add(arbol.getDatoRaiz());

            foreach (var hijo in arbol.getHijos())
            {
                Arbol.AddRange(Todos(hijo));
            }

            return Arbol;
        }

        public void Agregar(ArbolGeneral<ItemCat> arbol, ItemCat dato, string rutaAlPadre)
        {
            string[] ruta = rutaAlPadre.Split('/');

            ArbolGeneral<ItemCat> actual = arbol;

            ItemCat Datos = dato;

            if (actual.getDatoRaiz().Nombre != ruta[0])
            {
                throw new Exception("La ruta no comienza en la raíz del árbol.");
            }

            for (int i = 1; i < ruta.Length; i++)
            {
                bool encontrado = false;

                foreach (ArbolGeneral<ItemCat> hijo in actual.getHijos())
                {
                    if (hijo.getDatoRaiz().Nombre == ruta[i])
                    {
                        actual = hijo;
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    throw new Exception(
                        "La ruta al padre no existe: " + rutaAlPadre
                    );
                }
            }

            actual.agregarHijo(new ArbolGeneral<ItemCat>(Datos));
        }

        public List<ItemCat> Buscar(ArbolGeneral<ItemCat> arbol, string elementoABuscar)
		{
            List<ItemCat> Resultado = new List<ItemCat>();

            if (arbol.getDatoRaiz().Nombre.Contains(elementoABuscar))
            {
                Resultado.Add(arbol.getDatoRaiz());
            }

            foreach (ArbolGeneral<ItemCat> hijo in arbol.getHijos())
            {
                Resultado.AddRange(Buscar(hijo, elementoABuscar));
            }

			return Resultado;
		}

    }
}