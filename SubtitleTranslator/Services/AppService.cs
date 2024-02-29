
using App.Infrastructure.Common;
using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.NetWork.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;
using CommunityToolkit.Maui.Views;
using SubtitleTranslator.Resources.Configurations;
using SubtitleTranslator.ViewModels.Abstracts;
using SubtitleTranslator.ViewModels.Items;
using SubtitleTranslator.Views.Abstracts;
using System.Text.RegularExpressions;


namespace SubtitleTranslator.Services
{
    public class AppService
    {
        private const string TimePattern = @"\b\d{1,2}:\d{2}:\d{2}[\.\,]\d{3}\b";
        private static char[] Delimiters = { ',', '.' };
        public void UpdateApiClient(UserSetting userSetting)
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
        }
        public List<SubtitleItemViewModel> GetSubtitleItemsFromFile(string filePath)
        {
            List<SubtitleItemViewModel> list = new List<SubtitleItemViewModel>();
            using (var fileReader = new StreamReader(filePath))
            {
                SubtitleItemViewModel item = null;
                int index = 1;
                while (fileReader.Peek() >= 0)
                {
                    string line = fileReader.ReadLine();
                    MatchCollection matches = Regex.Matches(line, TimePattern);
                    if (matches.Count == 2)
                    {
                        item = new SubtitleItemViewModel();
                        item.Index = index++;
                        item.StartTime = ConvertTimeStringToTimeSpan(matches[0].Value);
                        item.EndTime = ConvertTimeStringToTimeSpan(matches[1].Value);
                    }
                    else if (string.IsNullOrWhiteSpace(line))
                    {
                        if (item != null)
                        {
                            list.Add(item);
                            item = null;
                        }
                    }
                    else if (item != null)
                    {
                        item.Subtitle = line;
                    }
                }
                if (item != null)
                {
                    list.Add(item);
                    item = null;
                }

            }
            return list;
        }
        private TimeSpan ConvertTimeStringToTimeSpan(string timeString)
        {
            string[] parts = timeString.Split(':');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            string[] secondsAndMilliseconds = parts[2].Split(Delimiters);
            int seconds = int.Parse(secondsAndMilliseconds[0]);
            int milliseconds = int.Parse(secondsAndMilliseconds[1]);

            TimeSpan offset = new TimeSpan(0, hours, minutes, seconds, milliseconds);
            return offset;
        }
        public async Task<List<SubtitleItemViewModel>> Translation(List<SubtitleItemViewModel> originals, LanguageItem languageItem)
        {
            List<SubtitleItemViewModel> list=new List<SubtitleItemViewModel>();
            IApiClient client = InstanceMap<IApiClient>.Instance;
            foreach (SubtitleItemViewModel original in originals)
            {
                var (result,errorMessage)= await client.ExecuteTranslation(languageItem,original.Subtitle);
                if(!string.IsNullOrEmpty(errorMessage))
                {
                    throw new Exception(errorMessage);
                }
                else
                {
                    list.Add(new SubtitleItemViewModel
                    {
                        Index = original.Index,
                        StartTime = original.StartTime,
                        EndTime = original.EndTime,
                        Subtitle = result
                    });
                }
            }
            return list;
        }
        public void ShowPopup<T,TViewModel>(Action<TViewModel> init) where TViewModel: PopupViewModelAbstract<object> where T : ModelPopupPage<TViewModel>, new()
        {
            T t = new T();
            init(t.ViewModel);
            App.Current.MainPage.ShowPopup(t);
        }
        public async Task<TResult?> ShowPopupAsync<T, TViewModel,TResult>(Action<TViewModel> init) where TViewModel : PopupViewModelAbstract<TResult> where T : ModelPopupPage<TViewModel>, new()
        {
            T t = new T();
            init(t.ViewModel);
            await App.Current.MainPage.ShowPopupAsync(t);
            return t.ViewModel.Result;
        }
    }
}
