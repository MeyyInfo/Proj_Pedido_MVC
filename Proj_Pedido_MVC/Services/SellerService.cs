using Proj_Pedido_MVC.Data;
using Proj_Pedido_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Proj_Pedido_MVC.Services.Exceptions;
using System.Data;

namespace Proj_Pedido_MVC.Services
{
    public class SellerService
    {
        private readonly Proj_Pedido_MVCContext _context;

        public SellerService(Proj_Pedido_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Inserir o objeto obj no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            //Para confirmar a operação no Banco de Dados
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj=> obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                //Faz a alteração do DBSet
                _context.Seller.Remove(obj);
                //Confirma a alteração do BD
                await _context.SaveChangesAsync();
            }
            //O catch para capturar uma possível DbUpdateException
            catch(DbUpdateException)
            {
                //Quando essa exceção ocorrer será lançada uma nova exceção,
                //nossa exceção de serviço, passando a exceção que veio do Framework.
                throw new IntegrityException("Can't delete seller because he/she has sales");

            }
           
        }

        public async Task UpdateAsync(Seller obj)
        {
            //Any - serve para verificar que existe algum registro no banco de dados na condição passada
            //Se não existir, lançar exceção.

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            //Quando você chama a operação de atualizar no BD, O BD pode retornar uma exceção e conflito
            //de concorrência
            //Criar um try-catch para capturar uma possível exceção de concorrência do BD.

            try
            {
                _context.Update(obj);
                 await _context.SaveChangesAsync();
            }
            /*DbUpdateConcurrencyException - exceção lançada pelo Entity Framework
            No cath será relançada a exceção em nível de serviço criada
            ou seja, o cath intercepta uma exceção do nível de acesso a dados e relança a exceção,
            usando a própria exceção em nível de serviço. Muito importante para segregar as camadas.
            A camada de serviço  não vai propagar uma exceção do nível de acesso a dados, a exceção será 
            lançada na própria camada de serviço. Assim sendo, o controlador, no caso, o SellersController,
            vai ter de lidar somente com exceções da camada de serviço, é uma forma de respeitar a arquitetura
            proposta.*/
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }



    }
}
