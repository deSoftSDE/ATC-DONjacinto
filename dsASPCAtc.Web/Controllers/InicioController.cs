using dsASPCAtc.DataAccess;
using EntidadesAtc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.Controllers
{
    public class InicioController : Controller
    {
        private IConfiguration _configuration;
        private string _defaultPage = "Index";
        private string _defaultController = "Inicio";
        public InicioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public IActionResult Index()
        //{
        //    ViewData["Resultado"] = new ResultadoRegistro();
        //    return View();
        //}
        [HttpPost]
        public IActionResult Index([FromForm] SolicitudRegistro sol)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesProcesarRegistro(sol);
            ViewData["Resultado"] = res;
            return View();
        }
        public IActionResult Index()
        {
            ViewData["Resultado"] = new ResultadoRegistro();
            return View();
        }

        public IActionResult Validacion(Guid id)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesValidarUsuarioWebPorGuid(id);
            ViewData["Resultado"] = res;
            return View();
        }
    }
}
