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
        private string streaming;
        public IndexViewModel(IConfiguration configuration)
        {
            var ad = new AdaptadorAtc(configuration);
            Vehiculos = ad.TiposVehiculoLeer(null);
            streaming = configuration.GetSection("StreamFiles")["rutaStreaming"]; 
            foreach (TipoVehiculo vh in Vehiculos)
            {
                vh.url = streaming + vh.Imagen;
            };
        }
    }
}
