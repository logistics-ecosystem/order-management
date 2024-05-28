using System.ComponentModel.DataAnnotations;

namespace LogisticTask.Models
{
    public class AcceptedOrder
    {
        [Key]
        public Guid UniqueId { get; set; } // Генерация UUID


        public DateTime DateTimeAccepted { get; set; }

        public Order Orders { get; set; }
        public int OrderId { get; set; }

        //public Car Cars { get; set; }
        public Guid CarId { get; set; }
    }
}
