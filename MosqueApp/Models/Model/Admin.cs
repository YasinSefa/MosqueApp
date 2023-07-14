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

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column("Email", TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Column("Password", TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Log is required.")]
        [Column("DateTime")]
        public DateTime Log { get; set; }
    }
}
