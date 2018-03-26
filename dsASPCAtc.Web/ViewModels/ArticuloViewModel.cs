using dsASPCAtc.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class ArticuloViewModel
    {
        public BuscaArticulo articulo;
        public ArticuloViewModel(IConfiguration configuration, int id)
        {
            var ad = new AdaptadorAtc(configuration);
            var res = ad.ArticulosLeerPorID(id);
            articulo = res;
        }
    }
}
