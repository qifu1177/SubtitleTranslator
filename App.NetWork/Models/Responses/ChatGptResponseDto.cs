using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.NetWork.Models.Responses
{
    public class ChatGptResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("created")]
        public long Created { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("choices")]
        public ChoiceDto[] Choices { get; set; }
        [JsonProperty("usage")]
        public UsageDto Usage { get; set; }
    }
    public class ChoiceDto
    {
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("message")]
        public MessageDto Message { get; set; }
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }
    public class MessageDto
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
    public class UsageDto
    {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }

}
