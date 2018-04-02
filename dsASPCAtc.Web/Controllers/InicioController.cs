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
            ViewData["Recuperacion"] = new ResultadoRecuperacionContrasena();
            return View();
        }

        public IActionResult Index()
        {
            ViewData["Resultado"] = new ResultadoRegistro();
            ViewData["Recuperacion"] = new ResultadoRecuperacionContrasena();
            return View();
        }

        public IActionResult Validacion(Guid id)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesValidarUsuarioWebPorGuid(id);
            ViewData["Resultado"] = res;
            return View();
        }

        public IActionResult Recuperacion([FromForm] SolicitudRecuperacion sol)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesRecuperarContrasena(sol.email);
            ViewData["Resultado"] = res;
            return View();
        }
        [HttpGet]
        public IActionResult Recuperar(Guid id)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesValidarRecuperacionPassword(id);
            if (res.Resultado == 1)
            {
                ViewData["Resultado"] = res;
                return View();
            } else
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            
        }
        [HttpPost]
        public IActionResult Recuperar([FromForm] RecuperacionPassword sol)
        {
            if (sol.password == sol.repeatpassword)
            {
                var ad = new ServicioCorreo(_configuration);
                ad.ClientesCambiarContrasena(sol.id, sol.password);
                var res = new ResultadoValidacionGuidRecuperacion();
                res.Usuario = new UsuarioDatosEmail();
                res.Usuario = new UsuarioDatosEmail
                {
                    IdUsuarioWeb = sol.id,
                    NombreCompleto = sol.nombre,
                    Nombre = sol.username,
                    Password = sol.password
                };
                res.Resultado = 2;
                ViewData["Resultado"] = res;
                return View();
            } else
            {
                var res = new ResultadoValidacionGuidRecuperacion();
                res.Usuario = new UsuarioDatosEmail
                {
                    IdUsuarioWeb = sol.id,
                    NombreCompleto = sol.nombre,
                    Nombre = sol.username
                };
                res.Resultado = 1;
                ViewData["Resultado"] = res;
                return View();
            }
        }

    }
}
