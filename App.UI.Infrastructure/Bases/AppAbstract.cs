using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.Bases
{
    public class AppAbstract<T> : Application where T : ISetup
    {
        protected T _setup;
        protected UiSetting _uiSetting;
        protected override void OnStart()
        {
            base.OnStart();
            UserSetting setting = _setup.GetUserSetting();
            _uiSetting = new UiSetting(setting);
            _setup.UpdateResourcen();
            _setup.InitAppLanguage();
        }

    }
}
