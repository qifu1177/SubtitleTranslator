using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.NetWork.Models.Responses
{
    public class WhisperResponseDto
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
