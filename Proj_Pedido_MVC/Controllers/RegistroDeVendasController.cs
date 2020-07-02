using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj_Pedido_MVC.Services;

namespace Proj_Pedido_MVC.Controllers
{
    public class RegistroDeVendasController : Controller
    {

        private readonly RegistroDeVendaService _registroDeVendaService;

        public RegistroDeVendasController(RegistroDeVendaService registroDeVendaService)
        {
            _registroDeVendaService = registroDeVendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //Verificar se a data tem um valor, caso não tenha, atribuir um valor

            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            //Os dados serão passados para View utilizando o dicionário ViewData
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _registroDeVendaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }


        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            //Verificar se a data tem um valor, caso não tenha, atribuir um valor

            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            //Os dados serão passados para View utilizando o dicionário ViewData
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _registroDeVendaService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}
