using EntidadesAtc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class EsquemaViewModel
    {
        public List<Fila> Filas { get; set; }
        public EsquemaViewModel(Carroceria car)
        {
            Filas = new List<Fila>
            {
                new Fila(),
                new Fila(),
                new Fila(),
                new Fila(),
                new Fila(),
                new Fila(),
                new Fila(),
                new Fila(),
                new Fila(),
            };
            foreach(Vidrio vid in car.Vidrios)
            {
                Filas[vid.PosVer].Celdas[vid.PosHor].Vidrio = vid;
                Filas[vid.PosVer].Celdas[vid.PosHor].ExtensionVer = vid.SpanVer;
                Filas[vid.PosVer].Celdas[vid.PosHor].ExtensionHor = vid.SpanHor;
                if (vid.SpanVer > 1)
                {
                    var Eliminado = false;
                    var celda = vid.PosHor;
                    var fila = vid.PosVer + 1;
                    var cantidad = vid.SpanVer - 1;
                    var eliminadas = 0;
                    while (!Eliminado)
                    {
                        if (Filas[fila].Celdas[celda].Eliminada)
                        {
                            fila++;
                        }
                        else
                        {
                            Filas[fila].Celdas[celda].Eliminada = true;
                            eliminadas++;
                            if (eliminadas == cantidad)
                            {
                                Eliminado = true;
                            }

                        }
                    };
                }

                if (vid.SpanHor > 1)
                {
                    var Eliminado = false;
                    var celda = vid.PosHor +1;
                    var fila = vid.PosVer;
                    var cantidad = vid.SpanHor - 1;
                    var eliminadas = 0;
                    while (!Eliminado)
                    {
                        if (Filas[fila].Celdas[celda].Eliminada)
                        {
                            celda++;
                        }
                        else
                        {
                            Filas[fila].Celdas[celda].Eliminada = true;
                            eliminadas++;
                            if (eliminadas == cantidad)
                            {
                                Eliminado = true;
                            }

                        }
                    };
                }

            }
            foreach(Fila fila in Filas)
            {
                fila.Celdas.RemoveAll(c => c.Eliminada);
            }
        }
    }
    public class Fila
    {
        public Fila()
        {
            Celdas = new List<Celda>
            {
                new Celda(),
                new Celda(),
                new Celda(),
                new Celda(),
                new Celda(),
                new Celda(),
                new Celda(),
                new Celda(),
                new Celda(),
            };
        }
        public List<Celda> Celdas { get; set; }
    }
    public class Celda
    {
        public Celda()
        {
            ExtensionVer = 1;
            ExtensionHor = 1;
        }

        public int ExtensionVer { get; set; }
        public int ExtensionHor { get; set; }
        public Vidrio Vidrio { get; set; }
        public Boolean Eliminada { get; set; }
    }
}
