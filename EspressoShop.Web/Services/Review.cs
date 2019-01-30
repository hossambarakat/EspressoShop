using System;

namespace EspressoShop.Web.Services
{
    public class Review
    {
        public string Id { get; set; }
        public string Headline { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreationDate { get; set; }
        public string ReviewerName { get; set; }
        public int? Stars { get; set; }
    }
}
