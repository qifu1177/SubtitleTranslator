using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.NetWork.Models.Requests
{
    public class ChatGptDto
    {
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("messages")]
        public MessageItemDto[] Messages { get; set; }
    }
    public class MessageItemDto
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
