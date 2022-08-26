using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.Services.Interfaces
{
    public interface IPhoneService : ICreatable<PhoneCreateViewModel>
    {
        PhoneViewModel GetById(int phoneId);
    }
}