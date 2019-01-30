namespace EspressoShop.Web.Models
{
    public class ReviewViewModel
    {
        public string Id { get; set; }
        public string Headline { get; set; }
        public string ReviewText { get; set; }
        public string CreationDate { get; set; }
        public string ReviewerName { get; set; }
        public int? Stars { get; set; }
    }
}
