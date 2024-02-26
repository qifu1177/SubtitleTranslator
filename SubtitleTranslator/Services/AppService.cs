using App.Infrastructure.Common;
using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.NetWork.Services;
using SubtitleTranslator.Resources.Configurations;


namespace SubtitleTranslator.Services
{
    public class AppService
    {
        public IApiClient GetApiClient(UserSetting userSetting)
        {
            if (userSetting.UseOpenAi && !string.IsNullOrWhiteSpace(userSetting.OpenAiKey))
            {
                InstanceMap<IApiClient>.Instance = Factory<OpenAiClient>.Init((instance) =>
                {
                    instance.SetConfig(Factory<OpenAiConfig>.Init((c) => c.ApiKey = userSetting.OpenAiKey));
                    instance.SetDataService(Factory<OpenAiDataService>.Instance);
                });
            }
            else
            {
                InstanceMap<IApiClient>.Instance = Factory<GoogleClient>.Init((instance) =>
                {
                    instance.SetConfig(Factory<GoogleConfig>.Instance);
                    instance.SetDataService(Factory<GoogleDataService>.Instance);
                });
            }
            return InstanceMap<IApiClient>.Instance;
        }
    }
}
