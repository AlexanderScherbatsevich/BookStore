using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Cart { get; set; } = string.Empty;

        [ForeignKey("ShippingDetails")]
        public int ShippingDetailsId { get; set; }

    }
}
