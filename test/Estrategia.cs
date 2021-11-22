  
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		
		public String Consulta1(ArbolGeneral<Planeta> arbol)
		{
			
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			
			int nivel = 0;
			
			c.encolar(arbol);
			c.encolar(null);
			
			Console.Write("Nivel " + nivel + ": ");
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();	//aux es igual al ultimo nodo de la cola
				
				
				if(arbolAux == null){			//si el ultimo nodo es null
					if(!c.esVacia()){			//si la cola no esta vacia
							nivel++;			//subir de nivel
							c.encolar(null);	//encola el null
					}
				}
				
				else{									//si ultimo nodo de la cola no es null
					
					
					if(arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
						break;
					
					foreach(ArbolGeneral<Planeta> hijo in arbolAux.getHijos())
					{
						c.encolar(hijo);
					}
				}
			}
			return ("El planeta del bot (azul) se encuentra a una distancia de "+ nivel+ " planetas del planeta raiz");
		}

		
		
		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			
			arbol.getDatoRaiz().Poblacion();
			
			
			return "xd";
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			//Implementar
			
			return null;
		}
		
		
		public void preorden(ArbolGeneral<Planeta> arbol)
		{
			// primero procesamos raiz
			
			Console.Write(arbol.getDatoRaiz().Poblacion() + " ");
			
			// luego los hijos recursivamente
			foreach(ArbolGeneral<Planeta> hijo in arbol.getHijos())
				preorden(hijo);
		}
		
		
	}
}
