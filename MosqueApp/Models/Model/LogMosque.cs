using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MosqueApp.Models.Model
{
    [Table("LogMosque")]
    public class LogMosque
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int? TownId { get; set; }

        public string Coordinate { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public byte[] QrCode { get; set; }
        public int AdminId { get; set; }
        public string Action { get; set; }
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
    }
}
