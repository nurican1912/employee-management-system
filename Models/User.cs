using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class User
    {
        [Column("Id")]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Column("PasswordHash")]
        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
