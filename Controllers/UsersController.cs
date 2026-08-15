using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation("Retrieving all users.");

            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "User ID must be greater than zero."
                });
            }

            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = $"User with ID {id} was not found."
                });
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _logger.LogInformation(
                "Creating a new user with email {Email}.",
                user.Email);

            // Check for duplicate email.
            var existingUser = _userService
                .GetAllUsers()
                .FirstOrDefault(u =>
                    u.Email.Equals(
                        user.Email,
                        StringComparison.OrdinalIgnoreCase));

            if (existingUser != null)
            {
                return Conflict(new
                {
                    message = "A user with this email already exists."
                });
            }

            var createdUser = _userService.AddUser(user);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = createdUser.Id },
                createdUser);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "User ID must be greater than zero."
                });
            }

            var existingUser = _userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound(new
                {
                    message = $"User with ID {id} was not found."
                });
            }

            // Prevent another user from using the same email.
            var duplicateEmail = _userService
                .GetAllUsers()
                .Any(u =>
                    u.Id != id &&
                    u.Email.Equals(
                        user.Email,
                        StringComparison.OrdinalIgnoreCase));

            if (duplicateEmail)
            {
                return Conflict(new
                {
                    message = "Another user already uses this email address."
                });
            }

            _userService.UpdateUser(id, user);

            _logger.LogInformation(
                "User with ID {UserId} was updated.",
                id);

            return Ok(new
            {
                message = "User updated successfully.",
                user = _userService.GetUserById(id)
            });
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "User ID must be greater than zero."
                });
            }

            var deleted = _userService.DeleteUser(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = $"User with ID {id} was not found."
                });
            }

            _logger.LogInformation(
                "User with ID {UserId} was deleted.",
                id);

            return Ok(new
            {
                message = "User deleted successfully."
            });
        }
    }
}
