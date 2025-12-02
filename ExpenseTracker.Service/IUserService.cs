using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Service
{
    public interface IUserService
    {
        Task<UserModel> GetUserByID(int userID);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<bool> UserExists(int userId);
        Task<IEnumerable<UserModel>> GetUsersByFamilyID(int familyID);
        Task<FamilyModel> GetFamilyByID(int familyID);
        Task<Family> AddFamily(Family family);
        Task<Family> UpdateFamily(Family family);
        Task<bool> DeleteFamily(int familyID);
        Task<bool> FamilyExists(int familyID);
    }
}
