using ExpenseTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public UserRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Family> AddFamily(Family family)
        {
            if(family == null)
            {
                throw new ArgumentNullException(nameof(family), "Family cannot be null.");
            }
            await _context.Families.AddAsync(family);
            await _context.SaveChangesAsync();
            return family;
        }

        public async Task<User> AddUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteFamily(int familyID)
        {
            var family = await _context.Families.FindAsync(familyID);
            if(family == null)
            {
                return false; // Family not found
            }
            _context.Families.Remove(family);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null)
            {
                return false; // User not found
            }
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> FamilyExists(int familyID)
        {
            return await _context.Families.AnyAsync(f => f.FamilyId == familyID);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users;
            try
            {
                users = await _context.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<Family> GetFamilyByID(int familyID)
        {
            var family = await _context.Families.FindAsync(familyID);
            if(family == null)
            {
                throw new KeyNotFoundException($"Family with ID {familyID} not found.");
            }
            return family;
        }

        public async Task<User> GetUserByID(int userId)
        {
            if(userId < 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return user;         
        }

        public async Task<IEnumerable<User>> GetUsersByFamilyID(int familyID)
        {
            IEnumerable<User> usersInFamily;
            try
            {
                var aUsersWithSameFamilyID = _context.Users.Where(u => u.FamilyId == familyID);
                usersInFamily = await aUsersWithSameFamilyID.ToListAsync();
                return usersInFamily;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving users by family ID.", ex);
            }
        }

        public async Task<Family> UpdateFamily(Family family)
        {
            if(family == null)
            {
                throw new ArgumentNullException(nameof(family), "Family cannot be null.");
            }
            _context.Families.Update(family);
            await _context.SaveChangesAsync();
            return family;
        }

        public async Task<User> UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }
    }
}
