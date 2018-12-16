namespace PANDA.Serv
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;

    public class UsersService : BaseService, IUsersService
    {
        public UsersService(PandaDbContext db)
            : base(db)
        {
        }

        public void CreateUser(string username, string password, string email)
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

        public IEnumerable<User> GetAllUsers()
        {
            return this.Db.Users;
        }

        public User GetUserByUsername(string username)
        {
            return this.Db.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}