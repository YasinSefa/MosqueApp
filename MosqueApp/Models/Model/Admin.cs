using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MosqueApp.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column("Username", TypeName = "varchar(50)")]
        public string Username { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column("Password", TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Column("DateTime")]
        public DateTime Log { get; set; }
    }
}
