using LibrariesEmpleado;
using Microsoft.AspNetCore.Mvc;
using MvcCoreConsumoApiEmpleados2022.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreConsumoApiEmpleados2022.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceApiEmpleados service;

        public EmpleadosController(ServiceApiEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<string> oficios = await this.service.GetOficiosAsync();
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string oficio)
        {
            List<string> oficios = await this.service.GetOficiosAsync();
            List<Empleado> empleados =
                await this.service.GetEmpleadosOficioAsync(oficio);
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }
    }
}
