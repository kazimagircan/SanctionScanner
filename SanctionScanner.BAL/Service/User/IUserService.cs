using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.User
{
    public interface IUserService
    {
        bool AddUser(UserDto data);
        bool UpdateUser(UserDto data);
        bool DeleteUser(int userId);
        UserDto CheckUser(string mail,string password);
        UserDto GetUserByMail(string mail);
        UserDto GetUserById(int id);
        List<UserDto> GetUsers();
    }
}
