using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MosqueApp.Models.Model
{
    public class Town
    {
        [Required(ErrorMessage = "İlçe boş bırakılamaz")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column("TownId")]
        public int TownId { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
