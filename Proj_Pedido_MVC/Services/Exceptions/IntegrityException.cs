using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_Pedido_MVC.Services.Exceptions
{
    public class IntegrityException:ApplicationException
    {
        //Herda de ApplicationException
        //Exceção personalizada de serviço para erros de integridade referencial.

        //Construtor que repassa a chamada para a super classe
        public IntegrityException(string message) :base(message)
        {
        }

    }
}
