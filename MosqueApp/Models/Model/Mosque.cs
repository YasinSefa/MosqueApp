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

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CityId is required.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [DisplayName("Hakkında")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Photos is required.")]
        public string Photos { get; set; }

        [Required(ErrorMessage = "QrCode is required.")]
        public string QrCode { get; set; }
    }

}
