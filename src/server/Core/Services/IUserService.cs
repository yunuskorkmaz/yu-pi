using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos.User;
using Core.Entities;

namespace Core.Services
{
    public interface IUserService
    {
        Task<User> AddUser(User model);
        Task<List<User>> GetUserList();
        Task<User> Login(LoginRequest model);
    }
}