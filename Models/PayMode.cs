using System.ComponentModel.DataAnnotations;

namespace SupermarketWEB.Models
{
    public class PayMode
    {
        public int Id { get; set; }

        [Display(Name = "NameTarjet")]
        public string NameTarjet { get; set; }

        [Display(Name = "NumberTarjet")]
        public string NumberTarjet { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
