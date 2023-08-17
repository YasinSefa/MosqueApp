using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MosqueApp.Models.Model
{

    [Table("LogPhotos")]
    public class LogPhotos
    {
        public int Id { get; set; }

        [Column("Base64")]
        public byte[] Base64 { get; set; }

        [Column("MosqueId")]
        public int MosqueId { get; set; }

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
