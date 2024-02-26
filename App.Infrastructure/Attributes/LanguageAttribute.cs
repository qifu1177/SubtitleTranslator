using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Attributes
{
    public class LanguageAttribute : Attribute
    {
        private string _shortName;
        private string _fullName;
        private string _whisperName;
        public LanguageAttribute(string shortName, string fullName, string whisperName = null)
        {
            _shortName = shortName;
            _fullName = fullName;
            if (string.IsNullOrEmpty(whisperName))
                _whisperName = shortName;
            else
                _whisperName = whisperName;
        }
        public string ShortName => _shortName;
        public string FullName => _fullName;
        public string WhisperName => _whisperName;
    }
}
