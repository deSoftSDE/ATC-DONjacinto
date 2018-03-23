using dsASPCAtc.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class VehiculosViewModel
    {
        public List<TipoVehiculo> Vehiculos;
        private string streaming;
        public TipoVehiculo VehiculoSeleccionado;
        public int? IDTipoVehiculo;
        public VehiculosViewModel(IConfiguration configuration, int? id)
        {
            var ad = new AdaptadorAtc(configuration);
            Vehiculos = ad.TiposVehiculoLeer(id);
            streaming = configuration.GetSection("StreamFiles")["rutaStreaming"];
            foreach (TipoVehiculo vh in Vehiculos)
            {
                vh.url = streaming + vh.Imagen;
                if (vh.IDTipoVehiculo == id)
                {
                    VehiculoSeleccionado = vh;
                }
            };
            IDTipoVehiculo = id;
        }
    }
}
