using System.Net;
using System.Text.Json.Serialization;

namespace EmailProcessingApp.Domain.Models
{
    public class BaseResponse
    {
        public virtual bool Success => string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; }
    }
}
