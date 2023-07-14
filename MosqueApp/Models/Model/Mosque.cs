using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MosqueApp.Models.Model
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Mosque")]
    public class Mosque
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Max 100 characters")]
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        public int CityId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Coordinate { get; set; }

        [DisplayName("Hakkında")]
        public string Description { get; set; }

        public string Photos { get; set; }

        public string QrCode { get; set; }
    }

}
