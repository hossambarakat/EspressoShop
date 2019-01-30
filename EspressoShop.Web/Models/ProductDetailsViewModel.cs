using System.Collections.Generic;

namespace EspressoShop.Web.Models
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}