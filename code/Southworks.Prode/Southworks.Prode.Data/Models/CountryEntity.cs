using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("Countries")]
    public class CountryEntity : IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(10)]
        public string Code { get; set; }
    }
}
