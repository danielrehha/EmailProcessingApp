namespace EmailProcessingApp.Domain.Models
{
    public class ResponseEmail : BaseEntity
    {
        public ResponseEmail()
        {
            IsSent = false;
        }

        public string Email { get; set; }
        public byte[] EmailBody { get; set; }
        public bool IsSent { get; set; }
    }
}
