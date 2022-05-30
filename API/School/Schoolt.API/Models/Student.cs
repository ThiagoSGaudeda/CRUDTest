using System.ComponentModel.DataAnnotations;

namespace Schoolt.API.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public int SchoolId { get; set; }

    }
}
