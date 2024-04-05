using App.Infrastructure.Attributes;

namespace App.Infrastructure.Datas
{
    public class Languages
    {
        public Languages()
        {
            Init();
        }
        private void Init()
        {
            List<LanguageItem> list = new List<LanguageItem>();
            var values = Enum.GetValues(typeof(TessDataLanguage));
            var enumType = typeof(TessDataLanguage);
            var attributType = typeof(LanguageAttribute);
            foreach (var item in values)
            {
                list.Add(new LanguageItem((TessDataLanguage)item, enumType, attributType));
            }
            Items = list.ToArray();
        }
        public LanguageItem[] Items { get; set; }
    }
    public class LanguageItem
    {
        public LanguageItem(TessDataLanguage testData, Type enumType, Type attributType)
        {
            TessData = testData;
            var enumMemberAttribute =
                        ((LanguageAttribute[])enumType.GetField(TessData.ToString()).GetCustomAttributes(attributType, true)).Single();
            if (enumMemberAttribute != null)
            {
                ShortName = enumMemberAttribute.ShortName;
                FullName = enumMemberAttribute.FullName;
                WhisperName = enumMemberAttribute.WhisperName;
            }
            Description = FullName;
        }
        public TessDataLanguage TessData { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string WhisperName { get; set; }
    }
}
