using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj_Pedido_MVC.Models;
using Proj_Pedido_MVC.Services;

namespace Proj_Pedido_MVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {

            return View();
            
        }

        //Recebe o objeto Seller que veio da requisição
        [HttpPost] //Definir a ação como Post
        [ValidateAntiForgeryToken] //Evita ataques maliciosos em que aproveita a autenticação.
        public IActionResult Create(Seller seller)
        {
            //Ação de inserir o objeto no banco de dados
            _sellerService.Insert(seller);
            //Redirecionar a requisição para a ação Index, que é a ação que vai mostrar novamente a tela principal do CRUD de vendedores.
            return RedirectToAction(nameof(Index));
            //nameof - se mudar o nome do string da ação Index não precisa mudar na chamada.
        }




    }
}