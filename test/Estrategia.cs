  
using System;
using System.Collections.Generic;
namespace DeepSpace
{
	class Estrategia
	{
		private ArbolGeneral<Planeta> IA,JUG,LCA;
		private Cola<ArbolGeneral<Planeta>>  mejCam;
		private bool mc=false;
		private int idxIA, o=0;
		
		public String Consulta1(ArbolGeneral<Planeta> arbol)
		{
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			int nivel = 0;
			
			c.encolar(arbol);
			c.encolar(null);
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				if(arbolAux == null){		
					if(!c.esVacia()){		
							nivel++;			
							c.encolar(null);
					}
				}
				else{
					if(arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
						break;
					
					foreach(ArbolGeneral<Planeta> hijo in arbolAux.getHijos())
						c.encolar(hijo);
				}
			}
			return ("El planeta del bot (azul) se encuentra a una distancia de "+ nivel+ " planetas del planeta raiz");
		}

		public String Consulta2(ArbolGeneral<Planeta> arbol)
		{
			string textHijo="", textNieto="", textAncestro="";
			if(arbol.getDatoRaiz().EsPlanetaDeLaIA()) { IA=arbol; }
			else
			{
				foreach(ArbolGeneral<Planeta> hijo in arbol.getHijos())
				{
					if(hijo.getDatoRaiz().EsPlanetaDeLaIA()) { IA=hijo; break; }
					foreach(ArbolGeneral<Planeta> nieto in hijo.getHijos())
					{
						if(nieto.getDatoRaiz().EsPlanetaDeLaIA()) { IA=nieto; break; }
						if(nieto.getHijos()[0].getDatoRaiz().EsPlanetaDeLaIA()) { IA=nieto.getHijos()[0]; break; }
					}
				}
			}
			if(IA.altura()==0)
				return "El bot no tiene hijos";
			else
			{
				if(IA.altura()==1)
					return ("El bot tiene solo un hijo: "+IA.getHijos()[0].getDatoRaiz().Poblacion());
				else
				{
					if(IA.altura()==2)
					{
						textHijo="El arbol tiene 6 desendientes: hijos:";
						textNieto=", nietos:";
						foreach(ArbolGeneral<Planeta> hijo in IA.getHijos())
						{
							textHijo+=" "+hijo.getDatoRaiz().Poblacion() ;
							textNieto+=" "+hijo.getHijos()[0].getDatoRaiz().Poblacion();
						}
					}
					else
					{
						if(IA.altura()==3)
						{
							textHijo="El arbol tiene 35 desendientes. hijos: ";
							textNieto="\nNietos: ";
							textAncestro="\nAncestros: ";
							foreach(ArbolGeneral<Planeta> hijo in IA.getHijos())
							{
								textHijo+=" "+hijo.getDatoRaiz().Poblacion();
								foreach(ArbolGeneral<Planeta> nieto in hijo.getHijos())
								{
									textNieto+=" "+nieto.getDatoRaiz().Poblacion();
									textAncestro+=" "+nieto.getHijos()[0].getDatoRaiz().Poblacion();
								}
							}
						}
					}
				}
			}
			return (textHijo+textNieto+textAncestro);
		}
		
		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			/*
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			
			int nivel=0, total=0, count=0;
			string resultado=null;
			
			c.encolar(arbol);
			c.encolar(null);
			while (!c.esVacia()) 
			{
				arbolAux = c.desencolar();
				
				if (arbolAux== null) 
				{
					resultado+= "En el nivel "+nivel+" la problacion total es "+total+ " con un promedio de poblacion de "+(float) total/count+". \n";
					if (!c.esVacia()) 
					{
						c.encolar(null);
						nivel++;
						total=0;
						count=0;
					}
				}
				else
				{
					if (arbolAux!=null) 
					{
					total+=arbolAux.getDatoRaiz().Poblacion();
					count++;
					}
					
					foreach (var hijo in arbolAux.getHijos())
						c.encolar(hijo);
				}
			}
			return ("\n\n"+resultado);
			*/
			
			
			Cola<ArbolGeneral<Planeta>> caminoBOT = new Cola<ArbolGeneral<Planeta>>();
			Cola<ArbolGeneral<Planeta>> caminoJugador = new Cola<ArbolGeneral<Planeta>>();
			Cola<ArbolGeneral<Planeta>> mejorCamino = new Cola<ArbolGeneral<Planeta>>();
			//ArbolGeneral<Planeta> LCA=IA;
			int idxRef=1, x=0, idxBOT,idxJUG, i1=0;
			string y="", w="";
			
			
			//busqueda camino de la raiz hasta la IA
			if(arbol.getDatoRaiz().EsPlanetaDeLaIA()){ caminoBOT.encolar(arbol);}
			else
			{
				foreach(ArbolGeneral<Planeta> hijo in arbol.getHijos())
				{
					if(hijo.getDatoRaiz().EsPlanetaDeLaIA()){ caminoBOT.encolar(arbol); caminoBOT.encolar(hijo);  break;}
					else
					{
						foreach(ArbolGeneral<Planeta> nieto in hijo.getHijos())
						{
							if(nieto.getDatoRaiz().EsPlanetaDeLaIA()){ caminoBOT.encolar(arbol); caminoBOT.encolar(hijo); caminoBOT.encolar(nieto); break;}
							else
								if(nieto.getHijos()[0].getDatoRaiz().EsPlanetaDeLaIA()){ caminoBOT.encolar(arbol); caminoBOT.encolar(hijo); caminoBOT.encolar(nieto); caminoBOT.encolar(nieto.getHijos()[0]); break;}
						}
					}
				}
			}
			
			//busqueda camino de la raiz hasta el jugador
			
			if(arbol.getDatoRaiz().EsPlanetaDelJugador()){ JUG=arbol; caminoJugador.encolar(arbol);}
			else
			{
				foreach(ArbolGeneral<Planeta> hijo in arbol.getHijos())
				{
					if(hijo.getDatoRaiz().EsPlanetaDelJugador()){ JUG=hijo; caminoJugador.encolar(arbol) ; caminoJugador.encolar(hijo); break;}
					else
					{
						foreach(ArbolGeneral<Planeta> nieto in hijo.getHijos())
						{
							if(nieto.getDatoRaiz().EsPlanetaDelJugador()){ JUG=nieto; caminoJugador.encolar(arbol); caminoJugador.encolar(hijo); caminoJugador.encolar(nieto); break;}
							else
								if(nieto.getHijos()[0].getDatoRaiz().EsPlanetaDelJugador()){ JUG=nieto; caminoJugador.encolar(arbol); caminoJugador.encolar(hijo); caminoJugador.encolar(nieto); caminoJugador.encolar(nieto.getHijos()[0]); break;}
						}
					}
				}
			}
			
			i1=1;
			
			while(i1<caminoBOT.total())
			{
				if(caminoBOT.getDato(i1).getDatoRaiz()==caminoJugador.getDato(i1).getDatoRaiz())
				{
					LCA=caminoBOT.getDato(i1);
					i1++;
				}
				else
					break;
			}
			
			y+="lda: "+ i1 + "valor lda"+ LCA.getDatoRaiz().Poblacion() + "\n";
			
			
			
			
			
			
			while(caminoBOT.tope()==LCA)
				caminoBOT.desencolar();
			
			while(caminoJugador.tope()==LCA)
				caminoJugador.desencolar();
			
			
			
			while(!caminoBOT.esVacia())
			{
				mejorCamino.encolar(caminoBOT.desapilar());
			}
			
			mejorCamino.encolar(LCA);
			
			while(!caminoJugador.esVacia())
			{
				mejorCamino.encolar(caminoJugador.desencolar());
			}
			
			for(int i=1; i<=mejorCamino.total(); i++)
				y+=" "+ mejorCamino.getDato(i).getDatoRaiz().Poblacion();
			
			
			return y;
			
			
			
			
			
			
			
		}
			
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			
			Cola<ArbolGeneral<Planeta>> caminoBOT = new Cola<ArbolGeneral<Planeta>>();
			Cola<ArbolGeneral<Planeta>> caminoJugador = new Cola<ArbolGeneral<Planeta>>();
			Cola<ArbolGeneral<Planeta>> mejorCamino = new Cola<ArbolGeneral<Planeta>>();
			int idxRef=1, x=0, idxBOT,idxJUG, i1=1, i2=1;
			
			if(mc==false){
				//busqueda camino de la raiz hasta la IA
				if(arbol.getDatoRaiz().EsPlanetaDeLaIA()){ IA=arbol; caminoBOT.encolar(arbol);}
				else
				{
					foreach(ArbolGeneral<Planeta> hijo in arbol.getHijos())
					{
						if(hijo.getDatoRaiz().EsPlanetaDeLaIA()){ IA=hijo; caminoBOT.encolar(arbol); caminoBOT.encolar(hijo); break;}
						else
						{
							foreach(ArbolGeneral<Planeta> nieto in hijo.getHijos())
							{
								if(nieto.getDatoRaiz().EsPlanetaDeLaIA()){ IA=nieto; caminoBOT.encolar(arbol); caminoBOT.encolar(hijo); caminoBOT.encolar(nieto); break;}
								else
									if(nieto.getHijos()[0].getDatoRaiz().EsPlanetaDeLaIA()){ IA=nieto.getHijos()[0]; caminoBOT.encolar(arbol); caminoBOT.encolar(hijo); caminoBOT.encolar(nieto); caminoBOT.encolar(nieto.getHijos()[0]); break;}
							}
						}
					}
				}
				
				//busqueda camino de la raiz hasta el jugador
				
				if(arbol.getDatoRaiz().EsPlanetaDelJugador()){ caminoJugador.encolar(arbol);}
				else
				{
					foreach(ArbolGeneral<Planeta> hijo in arbol.getHijos())
					{
						if(hijo.getDatoRaiz().EsPlanetaDelJugador()){ caminoJugador.encolar(arbol) ; caminoJugador.encolar(hijo); break;}
						else
						{
							foreach(ArbolGeneral<Planeta> nieto in hijo.getHijos())
							{
								if(nieto.getDatoRaiz().EsPlanetaDelJugador()){ caminoJugador.encolar(arbol); caminoJugador.encolar(hijo); caminoJugador.encolar(nieto); break;}
								else
									if(nieto.getHijos()[0].getDatoRaiz().EsPlanetaDelJugador()){ caminoJugador.encolar(arbol); caminoJugador.encolar(hijo); caminoJugador.encolar(nieto); caminoJugador.encolar(nieto.getHijos()[0]); break;}
							}
						}
					}
				}
				
				
				while(i1<caminoBOT.total())
				{
					if(caminoBOT.getDato(i1).getDatoRaiz()==caminoJugador.getDato(i1).getDatoRaiz())
					{
						LCA=caminoBOT.getDato(i1);
						i1++;
					}
					else
						break;
				}
				
				while(caminoBOT.tope()==LCA)
					caminoBOT.desencolar();
				
				while(caminoJugador.tope()==LCA)
					caminoJugador.desencolar();
				
				
				while(!caminoBOT.esVacia())
				{
					mejorCamino.encolar(caminoBOT.desapilar());
				}
				
				mejorCamino.encolar(LCA);
				
				while(!caminoJugador.esVacia())
				{
					mejorCamino.encolar(caminoJugador.desencolar());
				}
			}
			if(mc==false)
			{
				mc=true;
				mejCam=mejorCamino;
			}
			
			for(int i=1; i<=mejCam.total(); i++)
			{
				if(mejCam.getDato(i).getDatoRaiz().EsPlanetaDeLaIA())
					idxIA=i;
			}
			
			for(int i=1; i<idxIA; i++)
			{
				if(mejCam.getDato(i).getDatoRaiz().Poblacion() > x)
				{
					x=mejCam.getDato(i).getDatoRaiz().Poblacion();
					idxRef=i;
				}
			}
			
			
			if( (mejCam.getDato(idxIA).getDatoRaiz().Poblacion()/2)> mejCam.getDato(idxIA+1).getDatoRaiz().Poblacion() ) //ataca  si es mas grande
			{
				return new Movimiento(mejCam.getDato(idxIA).getDatoRaiz(), mejCam.getDato(idxIA+1).getDatoRaiz());
			}
			else
			{
				return new Movimiento(mejCam.getDato(idxRef).getDatoRaiz(), mejCam.getDato(idxRef+1).getDatoRaiz());
			}
			
		}
	}
}
