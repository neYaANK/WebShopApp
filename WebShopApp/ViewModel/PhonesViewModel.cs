using ClassLibraryDB;
using System.Collections.Generic;
using WebShopApp.Models;

namespace WebShopApp.ViewModel
{
    public class PhonesViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
