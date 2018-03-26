using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using dsASPCAtc.DataAccess;
using System.Net;
using dsASPCAtc.Web.ViewModels;
using EntidadesAtc;

namespace dsASPCAtc.Web.Controllers
{
    [Produces("application/json")]
    public class DataController : Controller
    {
        private IConfiguration _configuration;

        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult ArticulosLeerPorCadena(string cadena)
        {
            var ad = new AdaptadorAtc(_configuration);
            ObjectResult result;
            try
            {
                var res = ad.ArticulosLeerPorCadena(cadena);
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
        [HttpGet]
        public IActionResult MarcasLeerPorCadena(string cadena)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.MarcasLeerPorCadena(cadena);
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
        [HttpGet]
        public IActionResult MarcasLeer(int? IDTipoVehiculo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.MarcasLeer(IDTipoVehiculo);
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
        [HttpGet]
        public IActionResult ModelosLeer(int? IDTipoVehiculo, int IDSeccion)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.ModelosLeer(IDTipoVehiculo, IDSeccion);
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
        [HttpGet]
        public IActionResult ModelosLeerPorID(int IDFamilia)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                var res = ad.ModelosLeerPorID(IDFamilia);
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
        [HttpGet]
        public IActionResult AnosLeerPor(int idmodelocarroceria, int idfamilia)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                var res = ad.AnosLeerPor(idmodelocarroceria, idfamilia);
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
        [HttpGet]
        public IActionResult CarroceriasLeerEsquema(int idmodelocarroceria, int ano)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                var carr = ad.CarroceriasLeerEsquema(idmodelocarroceria, ano);
                var res = new EsquemaViewModel(carr);
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
        public IActionResult ArticulosBusquedaPaginada([FromBody] CampoBusqueda cadena)
        {
            var ad = new AdaptadorAtc(_configuration);
            ObjectResult result;
            try
            {
                var res = new ProductosViewModel(_configuration, cadena);
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
    }
}