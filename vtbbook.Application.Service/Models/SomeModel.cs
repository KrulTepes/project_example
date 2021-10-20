using System;

namespace vtbbook.Application.Service.Models
{
    public class SomeModel
    {
        public Guid Id { get; set; }
        public string SomeText { get; set; }
        public string SomeSender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
