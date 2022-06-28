using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizPage_Back_End.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Portfolio> Portfolios { get; set; }
    }
}
