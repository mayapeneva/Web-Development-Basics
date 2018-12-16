namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;
    using DataModels;
    using ViewModels.BindingModels;

    public interface IUsersService
    {
        void CreateUser(RegisterBindingModel model);

        bool UserExists(string username, string password);

        User GetUserByUsername(string modelRecipient);

        User GetUserByUsernameAndPass(string modelUsername, string hashedPassword);

        IEnumerable<User> GetAllUsers();
    }
}