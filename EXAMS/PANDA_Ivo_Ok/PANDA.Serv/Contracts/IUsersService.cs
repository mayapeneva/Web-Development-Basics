namespace PANDA.Serv.Contracts
{
    using System.Collections.Generic;
    using DataModels;

    public interface IUsersService
    {
        void CreateUser(string username, string password, string email);

        bool UserExists(string username, string password);

        User GetUserByUsernameAndPass(string modelUsername, string hashedPassword);

        IEnumerable<User> GetAllUsers();

        User GetUserByUsername(string modelRecipient);
    }
}