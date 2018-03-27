﻿using DevExpress.Compatibility.System.Web;
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

        public ResultadosViewModel(IConfiguration _configuration, Parametros parametros)
        {
            var ad = new AdaptadorAtc(_configuration);
            var res = ad.ArticulosLeerBusqueda(parametros);
            desc = res.Parametros;
            Articulos = res.Articulos;
            Accesorios = res.Accesorios;
            JavaScriptSerializer js = new JavaScriptSerializer();
            jsinfo = js.Serialize(desc);

        }
    }
}