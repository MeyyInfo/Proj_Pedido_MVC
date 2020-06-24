using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_Pedido_MVC.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {

        public DbConcurrencyException(string message) : base(message)
        {
        }

    }
}
