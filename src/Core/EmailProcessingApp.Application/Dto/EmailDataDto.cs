namespace EmailProcessingApp.Application.Dto
{
    public class EmailDataDto
    {
        public string Key { get; set; }
        public string Email { get; set; }
        public List<string> Attributes { get; set; }
    }
}
