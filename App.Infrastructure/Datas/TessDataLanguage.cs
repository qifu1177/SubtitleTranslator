using App.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Datas
{
    
    public enum TessDataLanguage
    {
        [Language("zh-CN","Chinese - Simplified","zh")]
        chi_sim,
        [Language("zh-TW", "Chinese - Traditional", "zh")]
        chi_tra,
        [Language("de", "German")]
        deu,
        [Language("en", "English")]
        eng,
        [Language("fr", "French")]
        fra,
        [Language("it", "Italian")]
        ita,
        [Language("ja", "Japanese")]
        jpn,
        [Language("ko", "Korean")]
        kor,        
        [Language("ru", "Russian")]
        rus,
        [Language("es", "Spanish")]
        spa,
    }
}
