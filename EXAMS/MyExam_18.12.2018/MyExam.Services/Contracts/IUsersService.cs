namespace MyExam.Services.Contracts
{
    using System.Collections.Generic;
    using BindingModels;
    using Common.DTOs;

    public interface IUsersService
    {
        UserDto CreateUser(RegisterBindingModel model);

        bool UserExists(string username, string password);

        UserDto GetUserByUsername(string modelRecipient);

        UserDto GetUserByUsernameAndPass(string modelUsername, string hashedPassword);

        IEnumerable<UserDto> GetAllUsers();
    }
}