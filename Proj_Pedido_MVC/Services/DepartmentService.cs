using Proj_Pedido_MVC.Data;
using Proj_Pedido_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_Pedido_MVC.Services
{
    public class DepartmentService
    {
        private readonly Proj_Pedido_MVCContext _context;

        public DepartmentService(Proj_Pedido_MVCContext context)
        {
            _context = context;
        }

        //Método para retornar todos os departamentos ordenados por nome

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }




    }
}
