using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class StudentCreateViewModel
    {
       
        [Required, MaxLength(10, ErrorMessage = "Name cannot exceed 10 characters")]
        public string name { get; set; }

        [Required]
        public Dept? department { get; set; }
        [Required]

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Display(Name = "Office Email")]
        public string email { get; set; }
        public IFormFile photopath { get; set; }
    }
}
