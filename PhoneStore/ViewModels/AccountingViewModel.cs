namespace PhoneStore.ViewModels
{
    public class AccountingViewModel
    {
        public UserAccountingViewModel User { get; set; }
        public FeedbackAccountingViewModel Feedback { get; set; }
    }

    public class AccountingBase
    {
        public string EntityName { get; set; }
        public int Count { get; set; }

        public AccountingBase(string entityName, int count)
        {
            EntityName = entityName;
            Count = count;
        }
    }

    public class UserAccountingViewModel : AccountingBase
    {
        public UserAccountingViewModel(string entityName, int count) : base(entityName, count)
        {
        }
    }
    public class FeedbackAccountingViewModel : AccountingBase
    {
        public FeedbackAccountingViewModel(string entityName, int count) : base(entityName, count)
        {
        }
    }
}