namespace PhoneStore.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhoneId { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Phone Phone { get; set; }
    }
}