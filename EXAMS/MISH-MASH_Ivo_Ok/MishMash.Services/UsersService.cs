namespace MishMash.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using BindingModels;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;

    public class UsersService : BaseService, IUsersService
    {
        private readonly IHashService hashService;

        public UsersService(MishMashDbContext db, IHashService hashService)
            : base(db)
        {
            this.hashService = hashService;
        }

        public void CreateUser(RegisterBindingModel model)
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
        }

        public bool UserExists(string username, string password)
        {
            var hashedPassword = this.hashService.Hash(password);

            return this.Db.Users.Any(u => u.Username == username &&
                                          u.Password == hashedPassword);
        }

        public User GetUserByUsername(string username)
        {
            return this.Db.Users.SingleOrDefault(u => u.Username == username);
        }

        public User GetUserByUsernameAndPass(string username, string password)
        {
            var hashedPassword = this.hashService.Hash(password);

            return this.Db.Users.SingleOrDefault(u => u.Username == username && u.Password == hashedPassword);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.Db.Users;
        }
    }
}