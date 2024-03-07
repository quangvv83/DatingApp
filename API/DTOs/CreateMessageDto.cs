using API.Entities;

namespace API.DTOs
{
    public class CreateMessageDto
    {
        
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
    }
}
