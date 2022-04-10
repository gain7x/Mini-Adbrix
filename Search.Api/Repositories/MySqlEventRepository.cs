using MySql.Data.MySqlClient;
using Shared.Models;
using System.Text.Json;

namespace Search.Api.Repositories
{
    public class MySqlEventRepository : IEventRepository
    {
        private readonly string _connString;

        public MySqlEventRepository(string connString)
        {
            _connString = connString;
        }

        public async Task<List<Event>> findEventByUserAsync(string userId)
        {
            List<Event> events;

            using (var conn = new MySqlConnection(_connString))
            {
                await conn.OpenAsync();

                events = await GetEventsAsync(conn, userId);
            }
            
            return events;
        }

        private async Task<List<Event>> GetEventsAsync(MySqlConnection conn, string userId)
        {
            List<Event> events = new();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT guid, name, content, created_date, user_guid" +
                    " FROM EVENT WHERE user_guid = @user_id";
                cmd.Parameters.AddWithValue("@user_id", userId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        events.Add(
                            new Event(
                                reader.GetString(0),
                                reader.GetString(1),
                                JsonSerializer.SerializeToDocument(reader.GetString(2)),
                                reader.GetDateTime(3),
                                reader.GetString(4)
                            ));
                    }
                }
            }

            return events;
        }
    }
}
