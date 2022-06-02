namespace EmailProcessingApp.Domain.Models
{
    public class BaseEntity
    {
        public Guid Key { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
