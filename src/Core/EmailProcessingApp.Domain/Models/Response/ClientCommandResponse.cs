using System.Net;
using System.Text.Json.Serialization;

namespace EmailProcessingApp.Domain.Models
{
    public class ClientCommandResponse : BaseResponse
    {
        public override bool Success => HttpStatusCode == HttpStatusCode.OK;

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public string StatusCode => HttpStatusCode.ToString();
    }
}
