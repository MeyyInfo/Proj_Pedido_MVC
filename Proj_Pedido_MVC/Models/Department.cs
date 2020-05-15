using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Proj_Pedido_MVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> List_Seller { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Adicionar_List_Seller(Seller item)
        {
            List_Seller.Add(item);
        }

        public double TotalSalesDepartamento(DateTime inicio, DateTime final)
        {
            return List_Seller.Sum(s => s.TotalSales(inicio, final));
        }








    }
}
