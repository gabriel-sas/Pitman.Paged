using System.ComponentModel.DataAnnotations;

namespace Pitman.Paged.Tests
{
    public class MyEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}