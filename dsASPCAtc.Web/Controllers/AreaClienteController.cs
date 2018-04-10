using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dsASPCAtc.Web.Models;
using Microsoft.Extensions.Configuration;
using EntidadesAtc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using dsASPCAtc.Web.ViewModels;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using dsASPCAtc.DataAccess;

namespace dsASPCAtc.Web.Controllers
{
    public class AreaClienteController : Controller
    {
        private IConfiguration _configuration;
        private IHttpContextAccessor _accessor;
        private string _defaultPage = "Index";
        private string _defaultController ="Inicio";
        public AreaClienteController(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _configuration = configuration;
            _accessor = accessor;


        }
        public IActionResult Index()
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            } else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            //HttpContext.Session.SetString("Test", "Ben Rules!");

            var vm = new IndexViewModel(_configuration);
            ViewData["Vehiculos"] = vm.Vehiculos;
            ViewData["UnClick"] = vm.UnClick;
            ViewData["Novedades"] = vm.Novedades;
            ViewData["Test"] = HttpContext.Session.GetString("Test");
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] Login login)
        {
            //HttpContext.Session.SetString("Test", "Ben Rules!");
            var ad = new AdaptadorAtc(_configuration);
            var ipaddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var us = ad.UsuariosLogin(login.email, login.password, ipaddress);
            if (us.IdUsuarioWeb > 0)
            {
                us.InfoMenuWeb = ad.InfoMenuWebLeer();
                us.Mensajes = ad.MensajeLeer(us.Cliente.IDCliente, 0);
                HttpContext.Session.SetObjectAsJson("Login", us);
                
                var vm = new IndexViewModel(_configuration);
                ViewData["Vehiculos"] = vm.Vehiculos;
                ViewData["UnClick"] = vm.UnClick;
                ViewData["Novedades"] = vm.Novedades;
                ViewData["Test"] = HttpContext.Session.GetString("Test");
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
                return View();
            } else
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
        }
        public IActionResult Pedido()
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            return View();
        }

        public IActionResult About()
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            ViewData["Message"] = HttpContext.Session.GetString("Test");
            ViewData["Objeto"] = HttpContext.Session.GetObjectFromJson<FormModel>("Objeto");

            return View();
        }
        [HttpPost]
        public IActionResult Procesar([FromForm] ProcesoPedido pr)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var ad = new AdaptadorAtc(_configuration);
            try
            {
                ad.PedidosCrear(pr.usuario, pr.domicilio);
                return View();
            }
            catch (Exception ex)
            {
                var c = new MensajeError();
                c.Contenido = ex.Message;
                return RedirectToAction("ErrorPedido", "AreaCliente", c);
            }
            
        }
        public IActionResult ErrorPedido(MensajeError ex)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            ViewData["Contenido"] = ex.Contenido;
            return View();

        }
        [HttpPost]
        public IActionResult Productos(CampoBusqueda cm)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            ViewData["Data"] = cm;
            return View();
        }
        [HttpGet]
        public IActionResult Productos(string producto)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var res = new CampoBusqueda { cadena = producto };
            ViewData["Data"] = res;

            return View();
        }
        [HttpGet]
        public IActionResult Resultados(int? modelo, int? periodo, int? carroceria, int? vidrio, string euro, int? marca, int? categoria)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            //var ad = new AdaptadorAtc(_configuration);
            var pr = new Parametros
            {
                idFamilia = modelo,
                idVidrio = vidrio,
                idModeloCarroceria = carroceria,
                eurocode = euro,
                idSeccion = marca,
                idCategoria = categoria,
            };
            if (periodo.HasValue)
            {
                if (periodo > 0)
                {
                    pr.ano = periodo;
                }

            }
            var vm = new ResultadosViewModel(_configuration, pr);
            //var desc = vm.desc
            
           
            //ViewData["DescripcionesJS"] = vm.jsinfo;
            //ViewData["Descripciones"] = vm.desc;
            ViewData["Datos"] = vm;
            return View();
        }
        [HttpGet]
        public IActionResult Buscador(int? vehiculo, int? modelo, int? marca)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var Parametros = new Parametros
            {
                idTipoVehiculo = vehiculo,
                idSeccion = marca,
                idFamilia = modelo
            };
            var vm = new VehiculosViewModel(_configuration, null, Parametros);
            ViewData["Vehiculos"] = vm.Vehiculos;
            ViewData["Parametros"] = vm.ParametrosJavascript;
            return View();
        }
        public IActionResult Vehiculos(int id)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var vm = new VehiculosViewModel(_configuration, id, null);
            ViewData["Vehiculos"] = vm.Vehiculos;
            ViewData["IDTipoVehiculo"] = vm.IDTipoVehiculo;
            ViewData["Vehiculo"] = vm.VehiculoSeleccionado;
            return View();
        }

        public IActionResult Articulo(int id)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var vm = new ArticuloViewModel(_configuration, id);
            ViewData["Articulo"] = vm;
            return View();
        }


        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Facturas()
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var usuario = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            return View();
        }
        public IActionResult Pedidos()
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var usuario = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            return View();
        }
        [HttpGet]
        public IActionResult Finanzas(int? p)
        {
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var usuario = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            var ad = new AdaptadorAtc(_configuration);
            ViewData["SituacionCliente"] = ad.SituacionClienteLeer(usuario.Cliente.IDCliente);
            ViewData["tab"] = p;
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.SetObjectAsJson("Login", null);
            return RedirectToAction(_defaultPage, _defaultController);
        }

        [HttpGet]
        public ActionResult GetData(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SampleData.Menu, loadOptions)), "application/json");
        }
        private Boolean ComprobarLogin()
        {
            try
            {
                var login = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
                return login.IdUsuarioWeb > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            //return true;
        }

    }
    
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
