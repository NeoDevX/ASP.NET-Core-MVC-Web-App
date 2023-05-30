using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Address
    {
        [Key] public int Id { get; set; }
        public string? State { get; init; }
        public string? Street { get; init; }
        public string? City { get; init; }
        public int? ZipCode { get; set; }
    }
}