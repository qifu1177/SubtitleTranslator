
namespace App.Infrastructure.Datas
{
    public class TextData
    {
        public DateTime Sdt { get; private set; }
        public TextData(DateTime sdt, string text, bool isTemp)
        {
            Sdt = sdt;
            Text = text;
            IsTemp = isTemp;
        }
        public string Text { get; set; }
        public bool IsTemp { get; set; }
        public bool MicrophoneInput { get; set; } = false;
    }
}
