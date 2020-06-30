using Proj_Pedido_MVC.Data;
using Proj_Pedido_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proj_Pedido_MVC.Services
{
    public class RegistroDeVendaService
    {

        private readonly Proj_Pedido_MVCContext _context;
        
            
            
        public RegistroDeVendaService(Proj_Pedido_MVCContext context)
        {
                 _context = context;
        }

        //OPERAÇÃO ASSÍNCRONA QUE BUSCA OS REGISTROS DE VENDA POR DATA (OPCIONAIS)

        //Vai retornar um List de SalesRecord
        //DateTime? - argumento opcional
        //Selecionar as vendas pelo intervalo de datas
        //Objeto result composto a partir do Link
        //Outras operações do Link podem ser acrescentadas ao objeto
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Construir a consulta a partir do Dbcontext do SalesRecord.
            var result = from obj in _context.SalesRecord select obj;
            //HasValue - Se for informada uma data mínima
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //Retorna a consulta no formato de lista

            //return result.ToList();

            //Pode agregar outras coisas, por exemplo, fazer um Join coma tabela de 
            //Vendedor e de Departamento, assim como ordenar decrescentemente por data

            // includ - função do Entity Framework - Importar o Microsoft.EntityFrameworkCore

            return await result
                .Include(x => x.Seller) //Faz o Join com a Tabela de Vendedor
                .Include(x=> x.Seller.Department) //Faz o Join com a Tabela de Departamento
                .OrderByDescending(x=>x.Date) //Ordenar por data
                .ToListAsync();

        }
    }
}
