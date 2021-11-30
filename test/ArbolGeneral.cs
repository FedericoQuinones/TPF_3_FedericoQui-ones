using System;
using System.Collections.Generic;

namespace DeepSpace
{
	class ArbolGeneral<Planeta>
	{
		
		private Planeta dato;
		private List<ArbolGeneral<Planeta>> hijos = new List<ArbolGeneral<Planeta>>();

		public ArbolGeneral(Planeta dato) {
			this.dato = dato;
		}
	
		public Planeta getDatoRaiz() {
			return this.dato;
		}
	
		public List<ArbolGeneral<Planeta>> getHijos() {
			return hijos;
		}
	
		public void agregarHijo(ArbolGeneral<Planeta> hijo) {
			this.getHijos().Add(hijo);
		}
	
		public void eliminarHijo(ArbolGeneral<Planeta> hijo) {
			this.getHijos().Remove(hijo);
		}
	
		public bool esHoja() {
			return this.getHijos().Count == 0;
		}
	
		public int altura() {			
			if(this.esHoja())
				return 0;
			else{
				int maxAltura = 0;
				foreach(var hijo in this.hijos)
					if(hijo.altura() > maxAltura)
						maxAltura = hijo.altura();
				
				return maxAltura + 1;
			}
		}
	
		
		public int nivel(Planeta dato) {
			return 0;
		}
		
		
		/*
		public void preorden(){
			// primero procesamos raiz
			Console.Write(this.dato.Poblacion() + " ");
			
			// luego los hijos recursivamente
			foreach(var hijo in this.hijos)
				hijo.preorden();
		}
		
		*/
	}
}