using System.Text.Json;

namespace Shared.Models
{
    public class Event
    {
        public string Guid { get; private set; }
        public string Type { get; private set; }
        public JsonDocument Content { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string UserGuid { get; private set; }

        public Event(string guid, string eventName, JsonDocument content, string userGuid)
            :this(guid, eventName, content, DateTime.Now, userGuid)
        {

        }

        public Event(string guid, string eventType, JsonDocument content, DateTime createdDate, string userGuid)
        {
            Guid = guid;
            Type = eventType;
            Content = content;
            CreatedDate = createdDate;
            UserGuid = userGuid;
        }
    }
}
