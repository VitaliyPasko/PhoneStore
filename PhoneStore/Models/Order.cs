namespace PhoneStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}