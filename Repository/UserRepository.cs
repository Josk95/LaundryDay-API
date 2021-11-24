using System;
using System.Linq;
using LaundryWebApi.Data;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;

namespace LaundryWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;


        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public User Create(User user)
        {
            _userContext.users.Add(user);
            _userContext.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            return _userContext.users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _userContext.users.FirstOrDefault(u => u.Id == id);
        }


        
        public User AuthenticateUser(LoginDto dto)
        {
            var existingUser = GetByEmail(dto.Email);
            if (existingUser == null)
                return null;


            if (!BCrypt.Net.BCrypt.Verify(dto.Password, existingUser.Password))
                return null;
            

            return existingUser;
          
        }

        public User Update(User user)
        {
            User currentUser = GetById(user.Id);
            _userContext.users.Update(currentUser);
            _userContext.SaveChanges();

            return currentUser;
        }
    }
}
