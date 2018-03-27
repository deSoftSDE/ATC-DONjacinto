using dsASPCAtc.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class IndexViewModel
    {
        public List<TipoVehiculo> Vehiculos;
        public List<BuscaArticulo> UnClick;
        public List<BuscaArticulo> Novedades;
        private string streaming;
        public IndexViewModel(IConfiguration configuration)
        {
            var ad = new AdaptadorAtc(configuration);
            Vehiculos = ad.TiposVehiculoLeer(null);
            var res = ad.ArticulosLeerUnClick();
            UnClick = res.UnClick;
            Novedades = res.Novedades;
            streaming = configuration.GetSection("StreamFiles")["rutaStreaming"]; 
            foreach (TipoVehiculo vh in Vehiculos)
            {
                vh.url = streaming + vh.Imagen;
            };
            if (Novedades.Count > 0)
            {
                Novedades[0].active = true;
            }
        }
    }
}
