using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos.User;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        public UserService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<User> AddUser(User model)
        {
            try{
                await context.Users.AddAsync(model);
                await context.SaveChangesAsync();
            }
            catch(Exception e){
                throw new ApiException("user.addError");
            }
            return model;
        }

        public async Task<List<User>> GetUserList()
        {
            var response = await context.Users.ToListAsync();
            return response;

        }

        public async Task<User> Login(LoginRequest model)
        {
            var user =  await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if(user != null){
                return user;
            }
            else{
                throw new ApiException("login.authError");
            }
        }
    }
}