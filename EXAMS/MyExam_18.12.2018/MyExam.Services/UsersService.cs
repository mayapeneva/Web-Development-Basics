namespace MyExam.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using BindingModels;
    using Common.DTOs;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;

    public class UsersService : BaseService, IUsersService
    {
        private readonly IHashService hashService;

        public UsersService(MyExamDbContext db, IHashService hashService)
            : base(db)
        {
            this.hashService = hashService;
        }

        public UserDto CreateUser(RegisterBindingModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);
            var role = this.Db.Users.Any() ? Role.User : Role.Admin;
            var user = new User
            {
                Username = model.Username,
                Password = hashedPassword,
                Email = model.Email,
                Role = role
            };

            this.Db.Users.Add(user);
            this.Db.SaveChanges();

            return new UserDto
            {
                Id = user.Id,
                Role = user.Role.ToString()
            };
        }

        public bool UserExists(string username, string password)
        {
            var hashedPassword = this.hashService.Hash(password);

            return this.Db.Users.Any(u => u.Username == username &&
                                          u.Password == hashedPassword);
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = this.Db.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Role = user.Role.ToString()
            };
        }

        public UserDto GetUserByUsernameAndPass(string username, string password)
        {
            var hashedPassword = this.hashService.Hash(password);

            var user = this.Db.Users.SingleOrDefault(u => u.Username == username && u.Password == hashedPassword);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Role = user.Role.ToString(),
                Email = user.Email
            };
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return this.Db.Users.ToList().Select(u => new UserDto
            {
                Id = u.Id,
                Role = u.Role.ToString()
            });
        }
    }
}