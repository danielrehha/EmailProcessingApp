using System.Net;
using System.Text.Json.Serialization;

namespace EmailProcessingApp.Domain.Models
{
    public class BaseResponse
    {
        public bool Success => HttpStatusCode == HttpStatusCode.OK;

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public string StatusCode => HttpStatusCode.ToString();

        public string ErrorMessage { get; set; }
    }
}
