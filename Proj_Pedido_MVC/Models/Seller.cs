using System;
using System.Collections.Generic;
using System.Linq;

namespace Proj_Pedido_MVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> List_SalesRecord { get; set; } = new List<SalesRecord>();


        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void Adicionar_List_SalesRecord(SalesRecord item)
        {
            List_SalesRecord.Add(item);

        }

        public void Remover_List_SalesRecord(SalesRecord item)
        {
            List_SalesRecord.Remove(item);

        }

        public double TotalSales(DateTime inicio, DateTime final)
        {
            return List_SalesRecord.Where(sr => sr.Date >= inicio && sr.Date <= final).Sum(sr => sr.Amount);
        }





    }
}
