

using App.Infrastructure.Datas;

namespace App.Infrastructure.Interfaces.Services
{
    public interface ISetup
    {
        UserSetting GetUserSetting();
        void UpdateResourcen();
        void InitAppLanguage();
    }
}