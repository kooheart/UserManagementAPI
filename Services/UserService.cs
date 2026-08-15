using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Age = 25
            },
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Age = 30
            }
        };

        private int _nextId = 3;

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }

        public User AddUser(User user)
        {
            user.Id = _nextId++;

            _users.Add(user);

            return user;
        }

        public bool UpdateUser(int id, User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Age = user.Age;

            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            _users.Remove(user);

            return true;
        }
    }
}
