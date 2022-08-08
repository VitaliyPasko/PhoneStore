using PhoneStore.ViewModels;
using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.Services.Interfaces
{
    public interface IPhoneService : ICreatable<PhoneCreateViewModel>
    {
        PhoneViewModel GetPhoneById(int phoneId);
    }
}