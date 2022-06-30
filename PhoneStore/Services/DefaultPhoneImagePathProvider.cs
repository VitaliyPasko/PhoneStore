using PhoneStore.Services.Abstractions;

namespace PhoneStore.Services
{
    public class DefaultPhoneImagePathProvider : IDefaultPhoneImagePathProvider
    {
        private readonly string _path;

        public DefaultPhoneImagePathProvider(string path)
        {
            _path = path;
        }

        public string GetPathToDefaultImage() => _path;
    }
}