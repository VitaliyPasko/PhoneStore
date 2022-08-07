using System;
using PhoneStore.Models;

namespace PhoneStore.ViewModels
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhoneId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public User User { get; set; }
        public Phone Phone { get; set; }
    }
}