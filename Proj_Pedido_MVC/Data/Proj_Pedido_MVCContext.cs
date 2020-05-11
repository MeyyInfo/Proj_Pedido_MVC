using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proj_Pedido_MVC.Models;

namespace Proj_Pedido_MVC.Data
{
    public class Proj_Pedido_MVCContext : DbContext
    {
        public Proj_Pedido_MVCContext (DbContextOptions<Proj_Pedido_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Proj_Pedido_MVC.Models.Department> Department { get; set; }
    }
}
