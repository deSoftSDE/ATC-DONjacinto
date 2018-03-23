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
        public IActionResult Buscador()
        {
            var vm = new VehiculosViewModel(_configuration, null);
            ViewData["Vehiculos"] = vm.Vehiculos;
            return View();
        }
        public IActionResult Vehiculos(int id)
        {
            var vm = new VehiculosViewModel (_configuration, id);
            ViewData["Vehiculos"] = vm.Vehiculos;
            ViewData["IDTipoVehiculo"] = vm.IDTipoVehiculo;
            ViewData["Vehiculo"] = vm.VehiculoSeleccionado;
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
