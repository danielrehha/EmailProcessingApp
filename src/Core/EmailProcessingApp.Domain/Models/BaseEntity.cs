namespace EmailProcessingApp.Domain.Models
{
    public class BaseEntity
    {
        public virtual string Key { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
