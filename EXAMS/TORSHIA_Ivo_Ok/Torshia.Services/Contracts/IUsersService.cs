namespace Torshia.Services.Contracts
{
    using Models;

    public interface IUsersService
    {
        void RegisterUser(string username, string password, string email);

        bool UserExists(string username, string password);

        User GetUserByUsernameAndPass(string modelUsername, string hashedPassword);
    }
}