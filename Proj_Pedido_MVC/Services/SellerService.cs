﻿using Proj_Pedido_MVC.Data;
using Proj_Pedido_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_Pedido_MVC.Services
{
    public class SellerService
    {
        private readonly Proj_Pedido_MVCContext _context;

        public SellerService(Proj_Pedido_MVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        //Inserir o objeto obj no banco de dados
        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            //Para confirmar a operação no Banco de Dados
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);

        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            //Faz a alteração do DBSet
            _context.Seller.Remove(obj);
            //Confirma a alteração do BD
            _context.SaveChanges();
        }





    }
}
