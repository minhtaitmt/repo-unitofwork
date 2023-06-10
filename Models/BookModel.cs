using System.ComponentModel.DataAnnotations;

namespace GenericRepositoryAndUnitofWork.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int CategoryId { get; set; }
    }
}


