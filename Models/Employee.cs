using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public class Employee
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public String Name {get;set;}

        public String JobTitle{get;set;}
    }
}