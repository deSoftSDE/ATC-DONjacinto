using dsASPCAppNews.DataAccess;
using dsASPCAtc.DataAccess;
using dsCore.Buscador;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class ProductosViewModel
    {
        public List<object> Articulos;
        public int NumReg;
        public int NumPags;
        public int NumPag;
        public int TamPag = 6;
        public EntidadesAtc.CampoBusqueda cm;
        

        public ProductosViewModel(IConfiguration configuration, EntidadesAtc.CampoBusqueda ba)
        {
            cm = new EntidadesAtc.CampoBusqueda();
            cm.cadena = ba.cadena;
            var v = "";
            var vc = "";
            if (ba.AccionPagina == null)
            {
                ba.AccionPagina = "F";
            }
            switch(ba.AccionPagina)
            {
                case "P":
                    v = ba.FirstValor;
                    vc = ba.FirstIndice.ToString();
                    break;
                case "N":
                    v = ba.LastValor;
                    vc = ba.LastIndice.ToString();
                    break;
            }
            var criterioAuxiliares = new CriterioBusqueda
            {
                IdISOLang = null,
                CampoOrdenacion = "Descripcion",
                TipoOrden = "ASC",
                NumPagina = 1,
                TamanoPagina = 6,
                CamposBusqueda = null,
                Entidad = "BuscaArticulo",
                Paginacion = false,
                Operacion = ba.AccionPagina,
                Valor = v,
                ValorClave = vc,
                CampoClave = "IdArticulo",
                EntidadFuncion = "BuscaArticulo",
                ValorFuncion = "'" + ba.cadena.Replace(" ", "%") + "'",
                EntidadVista = "VBuscaArticulo",
                idAlmacen = 1,
                idDelegacion = 0
            };
            var ls = new LecturasDA(configuration);
            var res = ls.LeerLista(criterioAuxiliares);
            Articulos = res.ListaResultados;
            NumReg = res.ContadorResultados;
            NumPags = res.NumeroPaginas;
            try
            {
                var c = (BuscaArticulo)Articulos[Articulos.Count - 1];
                var d = (BuscaArticulo)Articulos[0];
                cm.LastValor = c.Descripcion;
                cm.LastIndice = c.IdArticulo;
                cm.FirstValor = d.Descripcion;
                cm.FirstIndice = d.IdArticulo;
                cm.AccionPagina = ba.AccionPagina;
            }
            catch (Exception ex)
            {
                var a = 1;
            } 
            
        }

    }
}
