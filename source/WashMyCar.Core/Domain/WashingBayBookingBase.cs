using System.ComponentModel.DataAnnotations;

namespace WashMyCar.Core.Domain
{
    public class WashingBayBookingBase
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date)]       
        public DateTime Date { get; set; }
        [Required]
        public Color Color { get; set; }
        [Required]
        public string CarMake { get; set; }
        [Required]
        public string CarModel { get; set; }
        [Required]
        public bool IsFullWash { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly TimeSlot { get; set; }
    }
}
