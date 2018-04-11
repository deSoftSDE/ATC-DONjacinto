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
        public IActionResult FacturasLeer(int idCliente, int pagina, int bloque, string nFactura, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.FacturasLeer(idCliente, pagina, bloque, nFactura, fechaDesde, fechaHasta);
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
        public IActionResult PedidosLeer(int idCliente, int pagina, int bloque, string nFactura, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.PedidosLeer(idCliente, pagina, bloque, nFactura, fechaDesde, fechaHasta);
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
        public IActionResult MensajeMarcarLeido(int idMensaje, int idCliente)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                 ad.MensajeMarcarLeido(idMensaje, idCliente);
                var us = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
                us.Mensajes = ad.MensajeLeer(idCliente, 0);
                HttpContext.Session.SetObjectAsJson("Login", us);
                result = new ObjectResult(1)
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
        public IActionResult MarcasYModelosLeerPorCadena(string cadena)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.MarcasYModelosLeerPorCadena(cadena);
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
        public IActionResult FinanzasDebitosPendientesLeer(int idcliente, int bloque, int pagina)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.FinanzasDebitosPendientesLeer(idcliente, bloque, pagina);
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
        public IActionResult FacturasMensualesLeer(int idcliente)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.EsquemasMensualesLeer(idcliente);
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
        public IActionResult PedidosMensualesLeer(int idcliente)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.EsquemasMensualesLeer(idcliente, "Pedidos");
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
        public IActionResult FinanzasExtractosLeer(int iddeudor, int bloque, int pagina)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.FinanzasExtractosLeer(iddeudor, bloque, pagina);
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
        public IActionResult FinanzasEfectosCursoLeer(int iddeudor, int bloque, int pagina)
        {
            ObjectResult result;
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.FinanzasEfectosCursoLeer(iddeudor, bloque, pagina);
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
        [HttpPost]
        public IActionResult BajoPedidoEnviar([FromBody] FormularioBajoPedido form)
        {
            var ad = new ServicioCorreo(_configuration);
            ObjectResult result;
            try
            {
                var res = ad.CorreoBajoPedidoEnviar(form);
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
        public IActionResult PedidosCrear(int idUsuarioWeb, int idDomiEnt)
        {
            var ad = new AdaptadorAtc(_configuration);
            ObjectResult result;
            try
            {
                ad.PedidosCrear(idUsuarioWeb, idDomiEnt);
                result = new ObjectResult(1)
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