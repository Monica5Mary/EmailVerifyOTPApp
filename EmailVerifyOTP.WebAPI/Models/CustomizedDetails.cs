using EmailVerifyOTP.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace EmailVerifyOTP.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class CustomizedDetails : ProblemDetails
    {
            /// <summary>
            /// Default constructor
            /// </summary>
            public CustomizedDetails() { }

            /// <summary>
            /// Constructor with parameters
            /// </summary>
            /// <param name="errors"></param>
            public CustomizedDetails(List<InvalidParamException> errors)
            {
                InvalidParams = errors;
            }
            /// <summary>
            /// Trace Id
            /// </summary>
            [JsonPropertyName("trace-id")]
            public string TraceId { get; set; }

            /// <summary>
            /// Invalid Params
            /// </summary>
            [JsonPropertyName("invalid-param")]
            public List<InvalidParamException> InvalidParams { get; set; } = new List<InvalidParamException>();
        }
}
