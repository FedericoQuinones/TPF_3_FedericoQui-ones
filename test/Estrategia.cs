  
using System;
using System.Collections.Generic;
namespace DeepSpace
{
	
	class Estrategia
	{
		private ArbolGeneral<Planeta> IA,JUG,nPrev=null;
		private Cola<ArbolGeneral<Planeta>> mejorCaminoAUX;
		private Cola<Movimiento> MovRefuer;
		private int idxIA=1;
		
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
		}
			
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			Cola<ArbolGeneral<Planeta>> caminoBOT = new Cola<ArbolGeneral<Planeta>>();
			Cola<ArbolGeneral<Planeta>> caminoJugador = new Cola<ArbolGeneral<Planeta>>();
			Cola<ArbolGeneral<Planeta>> mejorCamino = new Cola<ArbolGeneral<Planeta>>();
			
			Movimiento m;
			
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
			if(caminoBOT.total()>=2 && caminoJugador.total()>=2)
			{
				while(caminoBOT.dos()==caminoJugador.menosdos())
				{
					caminoBOT.desencolar();
					caminoJugador.desencolar();
					
					if(caminoBOT.total()<=2 && caminoJugador.total()<=2) //fix bug si solo hay IA y JUG y no neutral (se encola solo [2 (IA), 5 (JUG)])
						break;
				}
			}
			
			if(caminoBOT.tope() == caminoJugador.tope())
				caminoBOT.desencolar();
				
			while(!caminoBOT.esVacia())
				mejorCamino.encolar(caminoBOT.desapilar());
			
			while(!caminoJugador.esVacia())
				mejorCamino.encolar(caminoJugador.desencolar());
			
			
			m=new Movimiento(arbol.getDatoRaiz(), arbol.getDatoRaiz());
			
			/*
	refu:
			if(Refuerza==true)
			{
				iGlobal++;
				while(iGlobal <= idxRefuerzo)
				{
					return new Movimiento(mejorCamino.getDato(iGlobal).getDatoRaiz(), mejorCamino.getDato(iGlobal+1).getDatoRaiz());
				}
				
			}
			
			
			if(mejorCamino.getDato(idxIA+1).getDatoRaiz().EsPlanetaDeLaIA()==false)
			{
				if( mejorCamino.getDato(idxIA+1).getDatoRaiz().Poblacion()>mejorCamino.getDato(idxIA).getDatoRaiz().Poblacion()/2 )
				{
					//refuerza
					Refuerza=true;
					idxRefuerzo=idxIA;
					goto refu;
					
				}
				else //ataca  si es mas grande
				{
					return new Movimiento(mejorCamino.getDato(idxIA).getDatoRaiz(), mejorCamino.getDato(idxIA+1).getDatoRaiz());
				}
			}
			else
			{
				iGlobal=0;
				idxRefuerzo=0;
				idxIA++;
			}
			*/
			
			if(mejorCamino.getDato(idxIA+1).getDatoRaiz().EsPlanetaDeLaIA()==false)
			{
				if( mejorCamino.getDato(idxIA+1).getDatoRaiz().Poblacion()> mejorCamino.getDato(idxIA).getDatoRaiz().Poblacion()/2 )
				{
					//refuerza
					for(int i=1; i>=idxIA-1; i++)
					{
						m=new Movimiento(mejorCamino.getDato(i).getDatoRaiz(), mejorCamino.getDato(i++).getDatoRaiz());
						MovRefuer.encolar(m);
					}
				}
				else //ataca  si es mas grande
				{
					return new Movimiento(mejorCamino.getDato(idxIA).getDatoRaiz(), mejorCamino.getDato(idxIA+1).getDatoRaiz());
				}
			}
			else
			{
				idxIA++;
			}
			
			while(!MovRefuer.esVacia())
			{
				m=MovRefuer.desencolar();
				return m;
			}
				
			return (m);
		}
	}
}
