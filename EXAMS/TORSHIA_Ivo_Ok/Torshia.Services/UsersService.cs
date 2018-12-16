namespace Torshia.Services
{
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;
    using System.Linq;

    public class UsersService : BaseService, IUsersService
    {
        public UsersService(TorshiaDbContext db)
            : base(db)
        {
        }

        public void RegisterUser(string username, string password, string email)
        {
            var role = this.Db.Users.Any() ? Role.User : Role.Admin;
            var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                Role = role
            };

            this.Db.Users.Add(user);
            this.Db.SaveChanges();
        }

        public bool UserExists(string username, string password)
        {
            return this.Db.Users.Any(u => u.Username == username && u.Password == password);
        }

        public User GetUserByUsernameAndPass(string username, string password)
        {
            return this.Db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}