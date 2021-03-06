﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using dsASPCAtc.DataAccess;

namespace dsASPCAtc.Controllers
{
    [Produces("application/json")]
    public class AtcController : Controller
    {
        private IConfiguration _configuration;

        public AtcController(IConfiguration configuration)
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
        public IActionResult Prueba()
        {
            var ad = new AdaptadorAtc(_configuration);
            ObjectResult result;
            try
            {
                var res = 1;
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