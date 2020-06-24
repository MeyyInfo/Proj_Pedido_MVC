using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_Pedido_MVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {

        //Construtor basico da classe recebendo uma mensagem e esse construtor vai repassar a mensagem para a classe base.
        //Exceções especificas da camada de serviço
        //Controle maior de como tratar cada tipo de exceção que pode ocorrer
        public NotFoundException(string message) : base(message)
        {

        }






    }
}
