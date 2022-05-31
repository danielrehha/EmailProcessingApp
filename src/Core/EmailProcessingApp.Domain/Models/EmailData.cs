namespace EmailProcessingApp.Domain.Models
{
    public class EmailData : BaseEntity
    {
        public string Email { get; set; }
        public string Attributes { get; set; }
    }
}
