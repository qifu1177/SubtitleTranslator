using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Interfaces.Datas
{
    public interface IApiConfig
    {
        string ApiKey { get; }
        string ApiUrl { get;}
        string SpeechRecognizeUrl { get; }
    }
}
