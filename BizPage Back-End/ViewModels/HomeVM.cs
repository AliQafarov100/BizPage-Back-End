using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizPage_Back_End.Models;

namespace BizPage_Back_End.ViewModels
{
    public class HomeVM
    {
        public List<Card> Cards { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Client> Clients { get; set; }
        public List<Portfolio> Portfolios { get; set; }
        public List<Category> Categories { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
