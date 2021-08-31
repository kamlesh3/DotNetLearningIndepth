using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class HomeEditsViewModel: StudentCreateViewModel
    {
        public int id { get; set; }
        public string existingphoto { get; set; }
    }
}
