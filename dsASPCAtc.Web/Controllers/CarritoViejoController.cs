using dsASPCAtc.DataAccess;
using EntidadesAtc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.Controllers
{
    [Produces("application/json")]
    public class CarritoViejoController : Controller
    {
        private IConfiguration _configuration;
        private Carrito _carrito;

        //public CarritoViejoController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //[HttpPost]
        //public IActionResult AnadirArticulo([FromBody] BuscaArticulo articulo)
        //{
        //    return AnadirCarrito(articulo);
        //}
        //[HttpGet]
        //public IActionResult AnadirArticulo(int IDArticulo)
        //{
        //    var ad = new AdaptadorAtc(_configuration);
        //    _carrito = HttpContext.Session.GetObjectFromJson<Carrito>("Carrito");
        //    if (_carrito.IDUsuario == null)
        //    {
        //        throw new Exception("Carrito no inicializado");
        //    }
        //    //var articulo = ad.ArticulosLeerPorID(IDArticulo, _carrito.IDUsuario);
        //    //return AnadirCarrito(articulo);
        //    return 1;
        //}
        //public ObjectResult AnadirCarrito(BuscaArticulo articulo)
        //{
        //    ObjectResult result;
        //    try
        //    {
        //        HttpContext.Session.SetString("Test", "Hola Mundo");
        //        try
        //        {
        //            _carrito = HttpContext.Session.GetObjectFromJson<Carrito>("Carrito");
        //            if (_carrito.IDUsuario == null)
        //            {
        //                throw new Exception("Carrito no inicializado");
        //            }
        //            _carrito.Articulos.Add(articulo);
        //            HttpContext.Session.SetObjectAsJson("Carrito", _carrito);
        //            result = new ObjectResult(_carrito)
        //            {
        //                StatusCode = (int)HttpStatusCode.OK
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            result = new ObjectResult(ex)
        //            {
        //                StatusCode = (int)HttpStatusCode.Conflict
        //            };
        //            Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ObjectResult(ex)
        //        {
        //            StatusCode = (int)HttpStatusCode.Conflict
        //        };
        //        Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
        //    }
        //    return result;
        //}
        //[HttpGet]
        //public IActionResult ObtenerCarrito(int idUsuario)
        //{
        //    //var ad = new AdaptadorAtc(_configuration);
        //    ObjectResult result;
        //    try
        //    {
        //        try
        //        {
        //            _carrito = HttpContext.Session.GetObjectFromJson<Carrito>("Carrito");
        //            if (_carrito.IDUsuario == null)
        //            {
        //                _carrito.IDUsuario = idUsuario;
        //                _carrito.Articulos = new List<BuscaArticulo>();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _carrito = new Carrito(idUsuario);
        //            HttpContext.Session.SetObjectAsJson("Carrito", _carrito);
        //        }
        //        result = new ObjectResult(_carrito)
        //        {
        //            StatusCode = (int)HttpStatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ObjectResult(ex)
        //        {
        //            StatusCode = (int)HttpStatusCode.Conflict
        //        };
        //        Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
        //    }
        //    return result;
        //}
    }
}
