
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		
		
		public String Consulta1( ArbolGeneral<Planeta> arbol)
		{
			
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			
			int nivel = 0;
			
			c.encolar(arbol);
			c.encolar(null);
			
			Console.Write("Nivel " + nivel + ": ");
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();	//aux es igual al ultimo nodo de la cola
				
				if(arbolAux == null){		//si el arbol auxiliar es null
					if(!c.esVacia()){		//si la cola no esta vacia
						nivel++;			//subir de nivel
						//Console.Write("\nNivel " + nivel + ": ");	//imprimir el nivel
						c.encolar(null);	//encola el null
					}						
				}
				else{									//si ultimo nodo de la cola no es null
					//Console.Write(arbolAux.dato + " "); //lo imprime
				
					foreach(ArbolGeneral<Planeta> hijo in arbolAux)
					{
						c.encolar(hijo);
					}
				}
			}
		}


		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
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
	}
}
