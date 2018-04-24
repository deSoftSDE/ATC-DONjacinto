using DevExpress.Compatibility.System.Web;
using dsASPCAtc.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class ResultadosViewModel
    {
        public Parametros desc;

        public string jsinfo;

        public List<BuscaArticulo> Articulos;

        public List<Categoria> Accesorios;

        public List<TipoVidrio> TiposVidrio;

        public string minHeightVidrio;
        public string minHeightAccesorio;


        public ResultadosViewModel(IConfiguration _configuration, Parametros parametros, EntidadEurocodes entidadEurocodes)
        {
            var ad = new AdaptadorAtc(_configuration);
            var res = ad.ArticulosLeerBusqueda(parametros);
            desc = res.Parametros;
            Articulos = res.Articulos;
            Accesorios = res.Accesorios;
            TiposVidrio = res.TiposVidrio;
            JavaScriptSerializer js = new JavaScriptSerializer();
            jsinfo = js.Serialize(desc);
            foreach (BuscaArticulo ar in Articulos)
            {
                var le = new LectorEurocode(ar.Codigo, entidadEurocodes);
                ar.Eurocode = le.Leer();
            }
            var streaming = _configuration.GetSection("StreamFiles")["rutaStreaming"];
            minHeightVidrio = (TiposVidrio.Count * 103).ToString() + "px";
            minHeightAccesorio = (Accesorios.Count * 64).ToString() + "px";
            foreach (TipoVidrio tiv in TiposVidrio)
            {
                tiv.url = streaming +  tiv.Imagen;
            }
            foreach (Categoria cat in Accesorios)
            {
                foreach (BuscaArticulo ar in cat.Articulos)
                {
                    var le = new LectorEurocode(ar.Codigo, entidadEurocodes);
                    ar.Eurocode = le.Leer();
                }
            }

        }
    }
}
