using PhoneStore.Models;
using PhoneStore.Repositories.Interfaces.Base;

namespace PhoneStore.Repositories.Interfaces
{
    public interface IFeedbackRepository : IBaseOperations<Feedback>, IByUserIdProvider<Feedback>
    {
        bool CheckFeedbackExists(int userId, int phoneId);
    }
}