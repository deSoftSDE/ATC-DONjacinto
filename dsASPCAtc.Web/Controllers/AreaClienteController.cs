﻿using System;
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
using System.IO;
using OfficeOpenXml;
using System.Text;
using Microsoft.Extensions.Options;

namespace dsASPCAtc.Web.Controllers
{
    public class AreaClienteController : Controller
    {
        private IConfiguration _configuration;
        private IHttpContextAccessor _accessor;
        private EntidadEurocodes _entidadEurocodes;
        private string _defaultPage = "Index";
        private string _defaultController ="Inicio";
        private string _endPoint;
        public AreaClienteController(IConfiguration configuration, IHttpContextAccessor accessor, IOptions<EntidadEurocodes> EntidadEurocodes)
        {
            _configuration = configuration;
            _accessor = accessor;
            _entidadEurocodes = EntidadEurocodes.Value;
            _endPoint = _configuration.GetSection("JSApi")["api"];

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
            ViewData["Api"] = _endPoint;
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] Login login)
        {
            //HttpContext.Session.SetString("Test", "Ben Rules!");
            ViewData["Api"] = _endPoint;
            var ad = new AdaptadorAtc(_configuration);
            var ipaddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var us = ad.UsuariosLogin(login.email, login.password, ipaddress);
            if (us.IdUsuarioWeb > 0)
            {
                us.InfoMenuWeb = ad.InfoMenuWebLeer();
                us.Mensajes = ad.MensajeLeer(us.Cliente.IDCliente, 0);
                us.DatosEmpresa = ad.DatosEmpresaLeer(us.IdUsuarioWeb);
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
                //var msj = new MensajeError();
                return RedirectToAction("Incorrecta", _defaultController);
            }
        }
        public IActionResult Pedido()
        {
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
        [HttpPost]
        public IActionResult SubirExcel([FromForm] FormularioExcel a)
        {
            ViewData["Api"] = _endPoint;
            var us = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            var msj = new List<MensajeError>();
            var res = new List<ArticuloBasico>();
            if (a.files != null)
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {

                        a.files.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data

                        using (ExcelPackage package = new ExcelPackage(ms))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            int rowCount = worksheet.Dimension.Rows;
                            int ColCount = worksheet.Dimension.Columns;
                            bool bHeaderRow = true;
                            for (int row = 1; row <= rowCount; row++)
                            {
                                if (bHeaderRow)
                                {
                                    bHeaderRow = false;
                                }
                                else
                                {
                                    var ar = new ArticuloBasico();
                                    //bool eurocode = true;
                                    if (worksheet.Cells[row, 1].Value != null)
                                    {
                                        var r = worksheet.Cells["C1"].Start;
                                        ar.Descripcion = worksheet.Cells[row, 1].Value.ToString();
                                        if (worksheet.Cells[row, 2].Value != null)
                                        {
                                            ar.Cantidad = worksheet.Cells[row, 2].Value.ToString();
                                        }
                                        else
                                        {
                                            ar.Cantidad = "1";
                                        }
                                        res.Add(ar);

                                    }

                                    /*for (int col = 1; col <= ColCount; col++)
                                    {
                                        if (eurocode)
                                        {
                                            ar.Descripcion = worksheet.Cells[row, col].Value.ToString();
                                            eurocode = false;
                                        }
                                        else
                                        {
                                            ar.Cantidad = worksheet.Cells[row, col].Value.ToString();
                                            //sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                                        }
                                    }*/

                                }

                            }
                            var i = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    msj.Add(new MensajeError { Descripcion = "Archivo no válido", Estado = 2 });
                }
                
                if (res.Count > 0)
                {
                    var ad = new AdaptadorAtc(_configuration);
                    msj = ad.CarritosUsuariosAnadirMasivamente(res, a.vaciar, us.IdUsuarioWeb);
                }
                else
                {
                    msj.Add(new MensajeError { Descripcion = "Excel vacío", Estado = 2 });
                }

            } else
            {
                msj.Add(new MensajeError { Descripcion = "Archivo no válido", Estado = 2 });
            }
            

            
            ViewData["Mensajes"] = msj;
            return View();
        }
        public IActionResult ErrorPedido(MensajeError ex)
        {
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
        public IActionResult Cuenta()
        {
            ViewData["Api"] = _endPoint;
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            ViewData["Mensaje"] = new MensajeError();
            return View();
        }
        [HttpPost]
        public IActionResult Cuenta([FromForm] FormularioCambioPassword form)
        {
            ViewData["Api"] = _endPoint;
            var me = new MensajeError();
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                ViewData["Usuario"] = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
            }
            if (form.newn == form.newnew)
            {
                var ad = new AdaptadorAtc(_configuration);
                var a = ad.UsuariosWebModificarClave(form);
                if (a == -1)
                {
                    me.Contenido = "La contraseña anterior es incorrecta";
                } else
                {
                    me.Contenido = "Contraseña modificada exitosamente";
                }
            } else
            {
                me.Contenido = "Las nuevas contraseñas no coinciden";
            }
            ViewData["Mensaje"] = me;
            return View();
        }
        [HttpGet]
        public IActionResult Productos(string producto)
        {
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
            var cl = new UsuarioWeb();
            cl.Cliente = new Cliente();
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                cl = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
                ViewData["Usuario"] = cl;
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
                idCliente = cl.Cliente.IDCliente,
            };
            if (periodo.HasValue)
            {
                if (periodo > 0)
                {
                    pr.ano = periodo;
                }

            }
            var vm = new ResultadosViewModel(_configuration, pr, _entidadEurocodes);
            //var desc = vm.desc
            
           
            //ViewData["DescripcionesJS"] = vm.jsinfo;
            //ViewData["Descripciones"] = vm.desc;
            ViewData["Datos"] = vm;
            return View();
        }
        [HttpGet]
        public IActionResult Buscador(int? vehiculo, int? modelo, int? marca)
        {
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
            var cl = new UsuarioWeb();
            cl.Cliente = new Cliente();
            if (!ComprobarLogin())
            {
                return RedirectToAction(_defaultPage, _defaultController);
            }
            else
            {
                cl = HttpContext.Session.GetObjectFromJson<UsuarioWeb>("Login");
                ViewData["Usuario"] = cl;
            }
            var vm = new ArticuloViewModel(_configuration, id, cl.Cliente.IDCliente, cl.IdUsuarioWeb, _entidadEurocodes);
            ViewData["Articulo"] = vm;
            return View();
        }


        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Facturas()
        {
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
            ViewData["Api"] = _endPoint;
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
