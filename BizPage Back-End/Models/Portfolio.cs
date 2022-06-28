using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizPage_Back_End.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
