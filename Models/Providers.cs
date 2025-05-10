using System.ComponentModel.DataAnnotations;

namespace SupermarketWEB.Models
{
    public class Providers
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "NumberPhone")]
        public string Phone { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
    
}

