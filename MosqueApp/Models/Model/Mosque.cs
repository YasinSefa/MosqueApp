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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }

        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adres boş bırakılamaz")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Required(ErrorMessage = "İlçe boş bırakılamaz")]
        [ForeignKey("Town")]
        public int? TownId { get; set; }
        public Town Town { get; set; }

        [Required(ErrorMessage = "Kordinat boş bırakılamaz")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Coordinate { get; set; }

        [Required(ErrorMessage = "Hakkında boş bırakılamaz")]
        [DisplayName("Hakkında")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Başlık boş bırakılamaz")]
        [DisplayName("Baslık")]
        public string Title { get; set; }

        public ICollection<Photos> Photos { get; set; }

        [Required(ErrorMessage = "Qr kod boş bırakılamaz")]
        public byte[] QrCode { get; set; }
       
        [Column("AdminId")]
        public int AdminId { get; set; }

        [Column("Action")]
        public string Action { get; set; }

        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
    }

}
