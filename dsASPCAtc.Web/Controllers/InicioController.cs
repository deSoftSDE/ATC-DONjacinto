using dsASPCAtc.DataAccess;
using dsASPCAtc.Web.ViewModels;
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
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            var ad2 = new AdaptadorAtc(_configuration);
            ViewData["Cabeceras"] = ad2.ImagenesCabWebLeer();
            return View();
        }
        private EmpresaWeb ObtenerDatosEmpresa()
        {
            try
            {
                var res = HttpContext.Session.GetObjectFromJson<EmpresaWeb>("EmpresaWeb");
                if (res == null)
                {
                    return ConsultarDatosEmpresa();
                } else
                {
                    return res;
                }
            }
            catch
            {
                return ConsultarDatosEmpresa();
            }
        }
        private EmpresaWeb ConsultarDatosEmpresa()
        {
            var ad = new AdaptadorAtc(_configuration);
            var dempr = ad.DatosEmpresaLeer();
            HttpContext.Session.SetObjectAsJson("EmpresaWeb", dempr);
            return dempr;
        }
        public IActionResult Index()
        {
            ViewData["Resultado"] = new ResultadoRegistro();
            ViewData["Recuperacion"] = new ResultadoRecuperacionContrasena();
            var lc = new LectorEurocode("6548RGSH5FD");
            var c = lc.Leer();
            var ld = new LectorEurocode("2722ACL1B");
            var d = ld.Leer();
            //LUNETA
            var le = new LectorEurocode("6548BGPEAOW1J");
            var e = le.Leer();
            //ACCESORIO
            var lf = new LectorEurocode("2715ASMH");
            var f = lf.Leer();
            var lg = new LectorEurocode("2711AGNGN");
            var g = lg.Leer();
            var lh = new LectorEurocode("3739AGNBLV1P");
            var h = lh.Leer();
            var li = new LectorEurocode("3587RGNM5FDKW");
            var i = li.Leer();
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            var ad2 = new AdaptadorAtc(_configuration);
            ViewData["Cabeceras"] = ad2.ImagenesCabWebLeer();
            return View();
        }

        public IActionResult Calidad()
        {
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }
        public IActionResult Historia()
        {
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }
        public IActionResult Formacion()
        {
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }

        public IActionResult Productos()
        {
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }
        public IActionResult Contacto()
        {
            ViewData["Mensaje"] = "";
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }
        [HttpPost]
        public IActionResult Contacto([FromForm] FormularioContacto form)
        {
            ViewData["Mensaje"] = "Formulario enviado con éxito";
            var ad = new ServicioCorreo(_configuration);
            var st = ad.CorreoContactoEnviar(form);
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }


        public IActionResult Validacion(Guid id)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesValidarUsuarioWebPorGuid(id);
            ViewData["Resultado"] = res;
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }

        public IActionResult Recuperacion([FromForm] SolicitudRecuperacion sol)
        {
            var ad = new ServicioCorreo(_configuration);
            var res = ad.ClientesRecuperarContrasena(sol.email);
            ViewData["Resultado1"] = res;
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
            return View();
        }
        [HttpGet]
        public IActionResult Recuperar(Guid id)
        {
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
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
            ViewData["DatosEmpresa"] = ObtenerDatosEmpresa();
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
