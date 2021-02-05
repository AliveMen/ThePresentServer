using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePresentServer.Data.Models
{
    public class Present
    {
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }

        public string LinkToProduct { get; set; }
        public string LinkToPicture { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

    }
}