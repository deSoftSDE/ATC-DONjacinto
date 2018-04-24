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
        public ArticuloViewModel(IConfiguration configuration, int id, int? idcliente, int? idUsuarioWeb, EntidadEurocodes entidadEurocodes)
        {
            if (id > 0)
            {
                var ad = new AdaptadorAtc(configuration);
                var res = ad.ArticulosLeerPorID(id, idcliente, idUsuarioWeb);
                articulo = res;
                var le = new LectorEurocode(articulo.Codigo, entidadEurocodes);
                articulo.Eurocode = le.Leer();
                foreach (Categoria ct in articulo.Accesorios)
                {
                    foreach (BuscaArticulo ar in ct.Articulos)
                    {
                        var lo = new LectorEurocode(ar.Codigo, entidadEurocodes);
                        ar.Eurocode = lo.Leer();
                    }
                }
                try
                {
                    var streaming = configuration.GetSection("StreamFiles")["rutaStreaming"];
                    articulo.Modelo.url = streaming + articulo.Modelo.Imagen;
                    if (articulo.Modelo != null)
                    {
                        foreach (ImagenFamilia ifa in articulo.Modelo.Imagenes)
                        {
                            ifa.url = streaming + ifa.Valor;
                        }
                        if (articulo.Modelo.Imagenes.Count > 0)
                        {
                            articulo.Modelo.Imagenes[0].active = true;
                        }
                    }
                    if (articulo.Imagenes.Count > 0)
                    {
                        articulo.Imagenes[0].active = true;
                    }
                }
                catch (Exception ex)
                {

                }
                
            } else
            {
                articulo = new BuscaArticulo();
                articulo.Accesorios = new List<Categoria>
                {
                    new Categoria
                    {
                        Articulos = new List<BuscaArticulo>(),
                    }
                };
            }
            
        }
    }
}
