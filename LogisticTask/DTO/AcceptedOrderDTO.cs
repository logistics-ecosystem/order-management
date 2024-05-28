namespace LogisticTask.DTO
{
    public class AcceptedOrderDTO
    {
        public Guid UniqueId { get; set; }
        public DateTime DateTimeAccepted { get; set; }
        public int OrderId { get; set; }

        public Guid CarId { get; set; }
    }
}
