namespace Chushka.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enum;
    using ViewModels.BindingModels;

    public class UsersService : BaseService, IUsersService
    {
        private readonly IHashService hashService;

        public UsersService(ChushkaDbContext db, IHashService hashService)
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
                FullName = model.FullName,
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