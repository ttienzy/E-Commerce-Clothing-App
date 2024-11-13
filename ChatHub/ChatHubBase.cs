using ChatHub.Contracts;
using ChatHub.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub
{
    public class ChatHubBase : Hub
    {

        private readonly IChatMessageCache _messageCache;
        private static readonly Dictionary<string, string> UserRooms = new();

        public ChatHubBase(IChatMessageCache messageCache)
        {
            _messageCache = messageCache;
        }

        public async Task JoinRoom(string roomId)
        {
            // Remove from previous room if exists
            if (UserRooms.ContainsKey(Context.ConnectionId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserRooms[Context.ConnectionId]);
                UserRooms.Remove(Context.ConnectionId);
            }

            // Join new room
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            UserRooms[Context.ConnectionId] = roomId;

            // Create room in cache if not exists
            if (!_messageCache.RoomExists(roomId))
            {
                _messageCache.CreateRoom(roomId);
            }

            // Send previous messages
            var messages = _messageCache.GetMessages(roomId);
            await Clients.Caller.SendAsync("LoadPreviousMessages", messages);
        }

        public async Task SendMessage(string message, string userType)
        {
            if (UserRooms.TryGetValue(Context.ConnectionId, out string roomId))
            {
                var chatMessage = new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = message,
                    UserType = userType,
                    Timestamp = DateTime.UtcNow
                };

                _messageCache.AddMessage(roomId, chatMessage);
                await Clients.Group(roomId).SendAsync("ReceiveMessage", chatMessage);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (UserRooms.TryGetValue(Context.ConnectionId, out string roomId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
                UserRooms.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
