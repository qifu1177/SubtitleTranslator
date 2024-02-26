using App.Infrastructure.Common;
using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.Infrastructure.Interfaces.Services.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Services
{
    public class SettingSerivice : AbstractService, ISettingSerivice
    {
        public UserSetting ReadUserSetting(string settingPath)
        {

            return FuncWithExceptionHandler(() =>
            {
                System.IO.FileInfo settingInfo = new System.IO.FileInfo(settingPath);
                if (settingInfo.Exists)
                {
                    string str = System.IO.File.ReadAllText(settingInfo.FullName);
                    UserSetting userSetting = JsonConvert.DeserializeObject<UserSetting>(str);
                    return userSetting;
                }
                return new UserSetting();
            });

        }
        public void SaveUserSetting(UserSetting userSetting, string settingPath)
        {
            ActionWithExceptionHandler(() =>
            {                
                string userSettingStr = JsonConvert.SerializeObject(userSetting);
                System.IO.File.WriteAllText(settingPath, userSettingStr);
            });

        }
        
    }
}
