using ChatHub.Models;

namespace ChatHub.Contracts
{
    public interface IChatMessageCache
    {
        void AddMessage(string roomId, ChatMessage message);
        List<ChatMessage> GetMessages(string roomId);
        void CreateRoom(string roomId);
        bool RoomExists(string roomId);
    }
}
