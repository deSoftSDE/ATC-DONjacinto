using dsASPCAtc.DataAccess;
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
        public List<Articulo> Articulos;
        public int NumReg;
        public int NumPags;
        public int NumPag;
        public ProductosViewModel(IConfiguration configuration, CampoBusqueda ba)
        {
            var ad = new AdaptadorAtc(configuration);
            var numPag = ba.numPag;
            if (!numPag.HasValue)
            {
                numPag = 1;
            }
            var bs = new BusquedaArticulos
            {
                valorFuncion = ba.cadena,
                tipoOrden = "ASC",
                orderBy = "Descripcion",
                tamanoPagina = 6,
                pagina = 1,
                idSeccion = null,
                idDelegacion = null,
                idFamilia = null,
                idGenerico = null,
            };
            var res = ad.LeerArticulosVenta(bs);
            Articulos = res.Articulos;
            NumReg = res.NumReg;
            NumPag = (int)numPag;
            NumPags = res.NumPags;
        }

    }
}
