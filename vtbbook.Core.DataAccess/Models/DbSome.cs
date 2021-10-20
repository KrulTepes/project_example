using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vtbbook.Core.DataAccess.Models
{
    public class DbSome : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string SomeText { get; set; }

        [Required]
        public string SomeSender { get; set; }
    }
}
