using System;
using System.Collections.Generic;

namespace DeepSpace
{

	public class Cola<T>
	{

		
		private List<T> datos = new List<T>();
	
		public void encolar(T elem) {
			this.datos.Add(elem);
		}
	
		public T desencolar() {
			T temp = this.datos[0];
			this.datos.RemoveAt(0);
			return temp;
		}
		
		public T tope() {
			return this.datos[0]; 
		}
		
		public bool esVacia() {
			return this.datos.Count == 0;
		}
		
		public void vaciar(){
			datos.Clear();
		}
		
		public int total(){
			return datos.Count;
		}
		
		public T desapilar() {
			T temp = this.datos[datos.Count-1];
			this.datos.RemoveAt(datos.Count-1);
			return temp;
		}
		
		public T getDato(int x){
			return this.datos[x-1];
		}
		
		public T ultimo(){
			return this.datos[datos.Count-1];
		}
		
		
		
		
	}
	
}
