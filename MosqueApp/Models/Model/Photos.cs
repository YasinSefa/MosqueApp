using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MosqueApp.Models.Model
{
    [Table("Photos")]
    public class Photos
    {
        [Key]
        public int Id { get; set; }

        [Column("Base64")]
        public byte[] Base64 { get; set; }

        [Column("MosqueId")]
        public int MosqueId { get; set; }

        [Column("AdminId")]
        public int AdminId { get; set; }

        [Column("Action")]
        public string Action { get; set; }
        [Column("IpAddress")]
        public string IpAddress { get; set; }
        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("Browser")]
        public string Browser { get; set; }
        [Column("Platform")]
        public string Platform { get; set; }
    }
}

