

namespace SubtitleTranslator.Resources
{
    public class ConstantValues
    {
        public const string SETTINGFILE = "setting.dat";
        public const string SubtitleFileType1 = "srt";
        public const string SubtitleFileType2 = "sbv";
        public static FilePickerFileType SubtitleFileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".srt", ".sbv" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "srt", "sbv" } }, // UTType values
                });
    }
}
