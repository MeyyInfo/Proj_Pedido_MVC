using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        //await - para fazer uma chamada assincrona dentro do método usar a palavra await 

        //A expressão link não é executada ela só prepara a consulta.
        //O ToList() que executa a consulta e transforma o resultado para list
        //ToList() é uma operação sincrona, porém existe outra versão do ToList()
        //ToListAsync - não é do List é o Entity Framework.


    }
}
