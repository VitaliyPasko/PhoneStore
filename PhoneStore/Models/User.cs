using System.ComponentModel;

namespace PhoneStore.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Возраст")]
        public int Age { get; set; }
    }
}