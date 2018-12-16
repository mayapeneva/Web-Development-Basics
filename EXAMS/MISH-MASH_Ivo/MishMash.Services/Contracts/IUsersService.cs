namespace MishMash.Services.Contracts
{
    using System.Collections.Generic;
    using BindingModels;
    using DataModels;

    public interface IUsersService
    {
        void CreateUser(RegisterBindingModel model);

        bool UserExists(string username, string password);

        User GetUserByUsername(string modelRecipient);

        User GetUserByUsernameAndPass(string modelUsername, string hashedPassword);

        IEnumerable<User> GetAllUsers();
    }
}