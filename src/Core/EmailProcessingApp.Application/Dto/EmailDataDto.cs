namespace EmailProcessingApp.Application.Dto
{
    public class EmailDataDto : BaseDto
    {
        public Guid Key { get; set; }
        public string Email { get; set; }
        public List<string> Attributes { get; set; }
    }
}
