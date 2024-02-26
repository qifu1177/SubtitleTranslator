using App.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Interfaces.Services
{
    public interface ISettingSerivice
    {
        UserSetting ReadUserSetting(string settingPath);
        void SaveUserSetting(UserSetting userSetting, string settingPath);
        
    }
}
