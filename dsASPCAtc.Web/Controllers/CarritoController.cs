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
    public class CarritoController : Controller
    {
        private IConfiguration _configuration;
        private Carrito _carrito;

        public CarritoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult AnadirArticulo(int IDUsuario, int IDArticulo, int? Cantidad)
        {
            ObjectResult result;
            try
            {
                var ad = new AdaptadorAtc(_configuration);
                _carrito = ad.CarritosUsuariosAnadirArticulo(IDUsuario, IDArticulo, Cantidad);
                result = new ObjectResult(_carrito)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

            } catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        [HttpGet]
        public IActionResult LeerCarrito(int IDUsuario)
        {
            ObjectResult result;
            try
            {
                var ad = new AdaptadorAtc(_configuration);
                _carrito = ad.CarritosUsuariosLeerPorIDUsuario(IDUsuario);
                result = new ObjectResult(_carrito)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
    }
}
