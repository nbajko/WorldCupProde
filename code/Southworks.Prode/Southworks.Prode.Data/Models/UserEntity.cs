using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("Users")]
    public class UserEntity : IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        [Index]
        public string Email { get; set; }

        public UserAccessLevel AccessLevel { get; set; }
    }

    public enum UserAccessLevel
    {
        Admin,
        Player
    }
}
