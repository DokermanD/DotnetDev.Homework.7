using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Customer
    {
        public int Id { get; init; }
        
        [Required]
        public string Firstname { get; init; }

        [Required]
        public string Lastname { get; init; }
    }
}