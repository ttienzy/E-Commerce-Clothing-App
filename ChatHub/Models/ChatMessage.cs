namespace ChatHub.Models
{
    public class ChatMessage
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
