

namespace SubtitleTranslator.Datas
{
    public class WhisperModels
    {
        private Dictionary<string, string> _models;
        public WhisperModels()
        {
            _models = new Dictionary<string, string>();
            _models.Add("tiny", "https://github.com/qifu1177/MyTranslator/raw/main/models/tiny.pt");
            _models.Add("base", "https://openaipublic.azureedge.net/main/whisper/models/ed3a0b6b1c0edf879ad9b11b1af5a0e6ab5db9205f891f668f8b0e6c6326e34e/base.pt");
            _models.Add("small", "https://openaipublic.azureedge.net/main/whisper/models/9ecf779972d90ba49c06d968637d720dd632c55bbf19d441fb42bf17a411e794/small.pt");
            _models.Add("medium", "https://openaipublic.azureedge.net/main/whisper/models/345ae4da62f9b3d59415adc60127b97c714f32e89e936602e85993674d08dcb1/medium.pt");
            _models.Add("large-v3", "https://openaipublic.azureedge.net/main/whisper/models/e5b1a55b89c1367dacf97e3e19bfd829a01529dbfdeefa8caeb59b3f1b81dadb/large-v3.pt");
            _models.Add("tiny.en", "https://github.com/qifu1177/MyTranslator/raw/main/models/tiny.en.pt");
            _models.Add("base.en", "https://openaipublic.azureedge.net/main/whisper/models/25a8566e1d0c1e2231d1c762132cd20e0f96a85d16145c3a00adf5d1ac670ead/base.en.pt");
            _models.Add("small.en", "https://openaipublic.azureedge.net/main/whisper/models/f953ad0fd29cacd07d5a9eda5624af0f6bcf2258be67c92b79389873d91e0872/small.en.pt");
            _models.Add("medium.en", "https://openaipublic.azureedge.net/main/whisper/models/d7440d1dc186f76616474e0ff0b3b6b879abc9d1a4926b7adfa41db2d497ab4f/medium.en.pt");
        }
        public string this[string key]
        {
            get
            {
                return _models[key];
            }
        }
    }
}
