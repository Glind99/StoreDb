using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreDb.Models
{
    public class Vacation
    {
        [Key]
        public int ApplicationId { get; set; }
        public string VacationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplicationDate { get; } = DateTime.Now;

        [ForeignKey("Employee")]
        public int Fk_EmployeeId { get; set; }
        public Employee? Employees { get; set; }
        public int NumDays => (EndDate - StartDate).Days;

    }
}
