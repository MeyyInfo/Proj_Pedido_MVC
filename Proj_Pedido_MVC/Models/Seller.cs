using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Proj_Pedido_MVC.Models
{
    public class Seller
    {
        public int Id { get; set; }



        /*StringLegth - Estabelecer limítes de mínimo e máximo para o tamano do campo, assim como emitir 
           mensagem de erro. O Framework tem uma mensagem padrão, caso a mesma não seja colocada.
           Também pode ser parametrizado:

           "{0} size should be between {2} and {1}"

            {0} - Nome do atributo
            {2} - Tamanho mínimo
            {3} - Tamanho máximo         
         */
        [Required(ErrorMessage ="{0} required")]   //Campo obrigatório        
        [StringLength(60, MinimumLength =3, ErrorMessage ="Name size should be between 3 and 60")]
        public string Name { get; set; }


        
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")] //Verifica se o e-mail é valido
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0,ErrorMessage ="{0} must be from {1} to {2}")] //Validação de faixa de valores para números
        [Display(Name = "Base Salary")]
        //O zero indica o valor do atributo, o F2 indica a formatação de duas casas decimais
        [DisplayFormat(DataFormatString ="{0:F2}")]
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
