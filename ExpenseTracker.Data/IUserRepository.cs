using ExpenseTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public interface IUserRepository
    {
        Task<User> GetUserByID(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<bool> UserExists(int userId);
        Task<IEnumerable<User>> GetUsersByFamilyID(int familyID);
        Task<Family> GetFamilyByID(int familyID);
        Task<Family> AddFamily(Family family);
        Task<Family> UpdateFamily(Family family);
        Task<bool> DeleteFamily(int familyID);
        Task<bool> FamilyExists(int familyID);
    }
}
