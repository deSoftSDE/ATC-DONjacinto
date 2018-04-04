﻿using dsASPCAtc.DataAccess;
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
            if (id > 0)
            {
                var ad = new AdaptadorAtc(configuration);
                var res = ad.ArticulosLeerPorID(id);
                articulo = res;
                var le = new LectorEurocode(articulo.Codigo);
                articulo.Eurocode = le.Leer();
                foreach (Categoria ct in articulo.Accesorios)
                {
                    foreach (BuscaArticulo ar in ct.Articulos)
                    {
                        var lo = new LectorEurocode(ar.Codigo);
                        ar.Eurocode = lo.Leer();
                    }
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
