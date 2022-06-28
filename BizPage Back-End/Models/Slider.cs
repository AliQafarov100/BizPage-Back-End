using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BizPage_Back_End.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Image { get; set; }
        
        public string Title { get; set; }
     
        public string SubTilte { get; set; }
    
        public string Order { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
