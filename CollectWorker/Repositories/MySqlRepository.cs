using MySql.Data.MySqlClient;
using Shared.Models;

namespace CollectWorker.Services
{
    public class MySqlRepository : IRepository
    {
        private readonly MySqlConnection _conn;

        public MySqlRepository(string connString)
        {
            try
            {
                _conn = new MySqlConnection(connString);
                _conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get DB connection: {ex.Message}");
            }
        }

        /// <summary>
        /// DB에 User를 저장합니다.
        /// </summary>
        /// <exception cref="Exception">수집 실패</exception>
        public async Task SaveAsync(User user)
        {
            using (var cmd = _conn.CreateCommand())
            {
                cmd.CommandText = "INSERT IGNORE `USER`(`guid`, `created_date`)" +
                    " VALUES(@guid, DEFAULT)";

                cmd.Parameters.AddWithValue("@guid", user.Guid);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// DB에 Event를 저장합니다.
        /// </summary>
        /// <exception cref="Exception">수집 실패</exception>
        public async Task SaveAsync(Event eventItem)
        {
            using (var cmd = _conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO `EVENT`" +
                    "(`guid`, `name`, `content`, `created_date`, `user_guid`)" +
                    " VALUES(@guid, @name, @content, DEFAULT, @user_guid)";
                cmd.Parameters.AddWithValue("@guid", eventItem.Guid);
                cmd.Parameters.AddWithValue("@name", eventItem.Type);
                cmd.Parameters.AddWithValue("@content", eventItem.Content.RootElement.ToString());
                cmd.Parameters.AddWithValue("@user_guid", eventItem.UserGuid);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
