using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class ShippingDetails
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the first address line")]
        [MaxLength(255)]
        public string Line1 { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Line2 { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Line3 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a city name")]
        [MaxLength(255)]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a state name")]
        [MaxLength(255)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Zip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a country name")]
        [MaxLength(255)]
        public string Country { get; set; } = string.Empty;

        //[ForeignKey("ApplicationUser")]
        //public Guid ApplicationUserId { get; set; }

        //public ApplicationUser? ApplicationUser { get; set; }

    }
}
