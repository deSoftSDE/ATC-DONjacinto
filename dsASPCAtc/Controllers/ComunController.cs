using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using dsCore.Buscador;
using dsCore.Tipos;
using dsASPCAtc.DataAccess;
using dsASPCAppNews.DataAccess;

namespace dsASPCAtc.Controllers
{
    [Produces("application/json")]
    public class ComunController : Controller
    {
        private IConfiguration _configuration;
        public ComunController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult LeerLista([FromBody] CriterioBusqueda criterio)
        {
            ObjectResult result;
            try
            {
                var ls = new LecturasDA(_configuration);
                var res = ls.LeerLista(criterio);
                result = new ObjectResult(res)
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

        [HttpPost]
        public IActionResult EjecutarProcedimiento([FromBody]Procedimiento proc)
        {
            ObjectResult result;
            try
            {
                var ad = new GenericoDA(_configuration);
                var res = ad.EjecutarProcedimientoAlmacenado(proc);
                result = new ObjectResult(res)
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


        public IActionResult LeerP(int id)
        {
            CriterioBusqueda criterio = new CriterioBusqueda();
            ObjectResult result;
            try
            {
                //var ls = new LecturasDA(_configuration);
                //var res = ls.LeerLista(criterio);
                var url = Url.Action("LeerP");
                //var url = Url.RouteUrl("default"); // Generates /custom/url/to/destination
                return Content($"See {id} {url}, it's really great.");
                //result = new ObjectResult(Id)
                //{
                //    StatusCode = (int)HttpStatusCode.OK
                //};
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