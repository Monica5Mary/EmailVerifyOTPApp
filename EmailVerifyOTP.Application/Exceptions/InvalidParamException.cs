using System.Text.Json.Serialization;

namespace EmailVerifyOTP.Application.Exceptions
{
        public class InvalidParamException
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("reason")]
            public string Reason { get; set; }
        }
}
