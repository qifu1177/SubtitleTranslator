﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.NetWork.Models.Responses
{
    public class ChatGptErrorResponseDto
    {
        [JsonProperty("error")]
        public ChatGptErrorDetailResponseDto Error { get; set; }
    }
    public class ChatGptErrorDetailResponseDto
    {
        [JsonProperty("message")]
        public string Message { get; set;}
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("param")]
        public string Param { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
