using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj_Pedido_MVC.Models;

namespace Proj_Pedido_MVC.Data
{
    public class PedidoService
    {
        //Declarar o PedidoService
        private Proj_Pedido_MVCContext _context;

        //Declarar o construtor recebendo o objeto PedidoService para indicar que a 
        //injeção de dependencia deve acontecer
        //Quando o PedidoService for criado, automaticamente ele vai receber uma
        //instancia do Proj_Pedido_MVCContext.

        public PedidoService(Proj_Pedido_MVCContext context)
        {
            _context = context;
        }


        //Operação que será responsável por popular a base de dados
       public void Seed()
       {
            //Se existir algum dado nas tabelas da base de dados vai interromper a operação

            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; //Corta e execução do método, não faz mais nada.
            }

            //Como estamos usando o workflow CODE-FIRST (Cria os objetos e a partir daí
            //cria a base de dados, logo, instanciar os objetos e mandar salvar os objetos
            //no banco de dados.

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            //Instaciar um objeto sem o construtor
            //Department d5 = new Department { Id = 1, Name = "Books" };

            Seller s1 = new Seller(1, "Bob", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller(2, "Maria Geen", "maria@gmail.com", new DateTime(1979, 12, 31), 35000.0, d2);
            Seller s3 = new Seller(3, "Alex Grey", "alex@gmail.com", new DateTime(1979, 12, 31), 35000.0, d1);
            Seller s4 = new Seller(4, "Martha Red", "martha@gmail.com", new DateTime(1979, 12, 31), 35000.0, d4);
            Seller s5 = new Seller(5, "Donald Blue", "donald@gmail.com", new DateTime(1979, 12, 31), 35000.0, d3);
            Seller s6 = new Seller(6, "Alex Pink", "alex@gmail.com", new DateTime(1979, 12, 31), 35000.0, d2);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s2);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s3);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s4);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s5);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s6);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s1);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s2);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s3);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s4);
            SalesRecord r11 = new SalesRecord(11, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s5);
            SalesRecord r12 = new SalesRecord(12, new DateTime(2018, 09, 25), 11000, Models.Enums.SalesStatus.Billed, s6);

            //Depois dos objetos criados, adicionar os objetos no Banco de Dados usando o Entity Framework

            //AddRange - permite adicionar vários objetos de uma única vez
            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);
            _context.SalesRecord.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12);

            //Para efetivar as laterações chamar o SaveChanges, salva e confirma as alterações no DB
            _context.SaveChanges();


        }





    }
}
