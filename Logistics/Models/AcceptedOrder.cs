using System.ComponentModel.DataAnnotations;

namespace Logistics.Models
{
    public class AcceptedOrder
    {
        [Key]
        public Guid UniqueId { get; set; }
        public DateTime DateTimeAccepted { get; set; }
        public Order Orders { get; set; } = new Order();
        public Guid OrderId { get; set; }

        //public Car Cars { get; set; }
        public Guid CarId { get; set; }
    }
}
