using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dsASPCAtc.Web.Models;
using dsASPCAtc.DataAccess;
using Microsoft.Extensions.Configuration;
using EntidadesAtc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using dsASPCAtc.Web.ViewModels;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using DevExpress.Compatibility.System.Web;

namespace dsASPCAtc.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Test", "Ben Rules!");
            var Objeto = new FormModel();
            Objeto.campo1 = "A";
            Objeto.campo2 = "B";
            Objeto.campo3 = "C";
            ViewData["Companies"] = JsonConvert.SerializeObject(SampleData.Companies);
            HttpContext.Session.SetObjectAsJson("Objeto", Objeto);
            var vm = new IndexViewModel(_configuration);
            ViewData["Vehiculos"] = vm.Vehiculos;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Test");
            ViewData["Objeto"] = HttpContext.Session.GetObjectFromJson<FormModel>("Objeto");

            return View();
        }
        [HttpPost]
        public IActionResult Productos(CampoBusqueda cm)
        {
            ViewData["Data"] = cm;
            return View();
        }
        [HttpGet]
        public IActionResult Productos(string producto)
        {
            var res = new CampoBusqueda { cadena = producto };
            ViewData["Data"] = res;

            return View();
        }
        [HttpGet]
        public IActionResult Resultados(int? modelo, int? periodo, int? carroceria, int? vidrio)
        {
            //var ad = new AdaptadorAtc(_configuration);
            var pr = new Parametros
            {
                idFamilia = modelo,
                idVidrio = vidrio,
                idModeloCarroceria = carroceria
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
            var vm = new VehiculosViewModel(_configuration, id, null);
            ViewData["Vehiculos"] = vm.Vehiculos;
            ViewData["IDTipoVehiculo"] = vm.IDTipoVehiculo;
            ViewData["Vehiculo"] = vm.VehiculoSeleccionado;
            return View();
        }

        public IActionResult Articulo(int id)
        {
            var vm = new ArticuloViewModel(_configuration, id);
            ViewData["Articulo"] = vm;
            return View();
        }
        
        public void AnadirACarrito(int id)
        {
            var a = 1;
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Mia()
        {
            ViewData["Message"] = "Mi paginilla.";
            //var ad = new AdaptadorAtc(_configuration);
            //var result = ad.CargaLeer();
            //ViewData["Contenido"] = result;
            var modelo = new FormModel();
            modelo.campo3 = "Campo Oculto";
            modelo.campo2 = "Campo Rellenado";
            return View(modelo);
        }
        [HttpPost]
        public IActionResult Form(FormModel form)
        {
            var b = form.campo4.OpenReadStream();
            ViewData["Message"] = form.campo1 + " " + form.campo2 + " " + form.campo3;
            
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult GetData(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SampleData.Menu, loadOptions)), "application/json");
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
