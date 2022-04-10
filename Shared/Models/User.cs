namespace Shared.Models
{
    public class User
    {
        public string Guid { get; private set; }

        public User(string guid)
        {
            Guid = guid;
        }
    }
}
