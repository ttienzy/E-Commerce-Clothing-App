using ChatHub.Contracts;
using ChatHub.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ChatHub.Repository
{
    public class ChatMessageCache : IChatMessageCache
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public ChatMessageCache(IMemoryCache cache)
        {
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }

        public void CreateRoom(string roomId)
        {
            if (!RoomExists(roomId))
            {
                _cache.Set(roomId, new List<ChatMessage>(), _cacheOptions);
            }
        }

        public bool RoomExists(string roomId)
        {
            return _cache.TryGetValue(roomId, out _);
        }

        public void AddMessage(string roomId, ChatMessage message)
        {
            if (_cache.TryGetValue(roomId, out List<ChatMessage> messages))
            {
                messages.Add(message);
                _cache.Set(roomId, messages, _cacheOptions);
            }
        }

        public List<ChatMessage> GetMessages(string roomId)
        {
            if (_cache.TryGetValue(roomId, out List<ChatMessage> messages))
            {
                return messages;
            }
            return new List<ChatMessage>();
        }
    }
}
