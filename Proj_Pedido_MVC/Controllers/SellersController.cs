using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj_Pedido_MVC.Models;
using Proj_Pedido_MVC.Models.ViewModels;
using Proj_Pedido_MVC.Services;
using Proj_Pedido_MVC.Services.Exceptions;

namespace Proj_Pedido_MVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;


        //_departmentServiceService da classe recebe departmentService do argumento.

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
           
            //Busca no BD todos os departamentos
            var departments = await _departmentService.FindAllAsync();
            //Instanciar o objeto do ViewModel
            //
            var viewModel = new SellerFormViewModel { Departments=departments};
            //Passar o objeto viewModel para a View
            return View(viewModel);
            
        }

        //Recebe o objeto Seller que veio da requisição
        [HttpPost] //Definir a ação como Post
        [ValidateAntiForgeryToken] //Evita ataques maliciosos em que aproveita a autenticação.
        public async Task<IActionResult> Create(Seller seller)
        {

            // ModelState.IsValid - Verifica se o modelo foi validado
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                // Retorna para a View Create repassando o objeto seller, volta e conserta
                return View(viewModel);
            }


            //Ação de inserir o objeto no banco de dados
            await _sellerService.InsertAsync(seller);
            //Redirecionar a requisição para a ação Index, que é a ação que vai mostrar novamente a tela principal do CRUD de vendedores.
            return RedirectToAction(nameof(Index));
            //nameof - se mudar o nome do string da ação Index não precisa mudar na chamada.
        }

        //Esta ação recebe um int opcional que é o Id. A interrogação indica que é opcional.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //A ação Error recebe um argumento que é a mensagem. Para passar o argumento criar um objeto
                //anonimo new ...
                //Dessa forma, redireciona para a página de Error.
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            //Precisa colocar id.Value para pegar o valor dele, porque ele é um nullable (um objeto opcional),
            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                //Redirecionar  para a página de erro com a mensagem da exceção.
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }          
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Precisa colocar id.Value para pegar o valor dele, porque ele é um nullable (um objeto opcional),
            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }


        /*A ação Edit serve para abrir a tela para editar o vendedor
        int (?) - opcional para evitar de acontecer um  erro de execução, na verdade,
        o Id é obrigatório.*/
        public async Task<IActionResult> Edit(int? Id)
        {
            //Se o Id for nulo, significa que a requisição foi feita de forma errada.
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Testar se o Id existe no BD

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            /*Se tudo passar, abrir a tela de edição. Para abrir a tela de edição é preciso
            carregar os Departamentos para povoar a caixa de seleção*/

            List<Department> departments = await _departmentService.FindAllAsync();

            /* Instanciar o ViewModel
               Preencher os dados com o objeto Seller passado
             */

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            //Retornar a View, passando o viewModel como argumento
            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            // ModelState.IsValid - Verifica se o modelo foi validado
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };        
                
                // Retorna para a View Create repassando o objeto seller, volta e conserta
                return View(viewModel);
            }

            //O id do vendedor não pode ser diferente do id da requisição
            if (id != seller.Id)
            {
                //return BadRequest();
                //Id mismatch - O Id não corresponde.
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            /*A chamada o Update pode lançar exceções, colocar a chamada dentro de um Try-Cath   */

            try
            {
                await _sellerService.UpdateAsync(seller);
                //Redirecionar a requisição para a página inicial do CRUD
                return RedirectToAction(nameof(Index));

            }
            //Como estes dois casos são exceções e as exceções carregam mensagem, na frente da declaração da exceção
            //colocar um apelido e, logo, será retornada a mensagem da exceção.
            catch(NotFoundException e){
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            //Ao invés de utilizar as duas exceções (RedirectToAction e DbConcurrencyException) pode utilizar o ApplicationException, 
            // que é um super tipo das duas exceções

            //catch (ApplicationException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
        }

        public IActionResult Error(string message)
        {
            //O atributo Message será a mensagem passada como argumento (message).
            //O RequestId será o Id interno da requisição.
            //Current? é opcional por causa da interrogação. Se ele for nulo, usar o perador de coalecencia nula (??)
            // e dizer que vai querer no lugar, o objeto HttpContext.TraceIdentifier.
            //RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier - Massete do Framework para pegar o Id interno da requisição.
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }

       

    }
}