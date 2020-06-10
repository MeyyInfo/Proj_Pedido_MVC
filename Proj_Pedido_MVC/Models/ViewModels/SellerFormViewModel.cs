using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_Pedido_MVC.Models.ViewModels
{
    public class SellerFormViewModel
    {

        //Quais são os dados necessários para uma tela de cadastro de vendedor?

        //Precisa ter um vendedor

        public Seller Seller { get; set; }

        //Além disso precisa ter uma lista de departamento

        //Usar o nome da list no plural, ajuda o framwork a reconhecer os dados,
        //na hora de fazer a conversão dos dados Http para objeto ele já sabe fazer automaticamente.
        public ICollection<Department> Departments { get; set; } 




    }
}
