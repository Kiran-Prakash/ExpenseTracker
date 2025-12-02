using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Family> AddFamily(Family family)
        {
            if (family == null)
            {
                throw new ArgumentNullException(nameof(family), "Family cannot be null.");
            }

            return await _userRepository.AddFamily(family);
        }

        public async Task<User> AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            return await _userRepository.AddUser(user);
        }

        public async Task<bool> DeleteFamily(int familyID)
        {
            if (familyID <= 0)
            {
                throw new ArgumentException("Family ID must be greater than zero.", nameof(familyID));
            }

            return await _userRepository.DeleteFamily(familyID);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }

            return await _userRepository.DeleteUser(userId);
        }

        public async Task<bool> FamilyExists(int familyID)
        {
            if (familyID <= 0)
            {
                throw new ArgumentException("Family ID must be greater than zero.", nameof(familyID));
            }

            return await _userRepository.FamilyExists(familyID);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            IEnumerable<User> users = await _userRepository.GetAllUsers();
            var userProfiles = new List<UserModel>();
            foreach(var user in users)
            {
                var userModel = GetUserModelFromUser(user);
                userProfiles.Add(userModel);
            }
            return userProfiles;
        }

        public async Task<FamilyModel> GetFamilyByID(int familyID)
        {
            var family = await _userRepository.GetFamilyByID(familyID);
            return await GetFamilyModelFromFamily(family);
        }

        public async Task<UserModel> GetUserByID(int userID)
        {
            if (userID < 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userID));
            }

            var user = await _userRepository.GetUserByID(userID);
            return GetUserModelFromUser(user);

        }

        public UserModel GetUserModelFromUser(User user)
        {
            var userModel = new UserModel();
            if (user != null)
            {
                userModel.UserName = user.UserName;
                userModel.FamilyId = user.FamilyId;
                userModel.AdobjId = (int)user.AdobjId;
                userModel.DateOfBirth = user.DateOfBirth;
                userModel.DisplayName = user.DisplayName;
                userModel.EmailId = user.EmailId;
            }
            return userModel;
        }

        public async Task<FamilyModel> GetFamilyModelFromFamily(Family family)
        {
            var familyModel = new FamilyModel();
            if (family != null)
            {
                familyModel.FamilyName = family.FamilyName;
                var userModels = (List<UserModel>)await GetUsersByFamilyID(family.FamilyId);
                familyModel.FamilyMembers = userModels;
            }
            return familyModel;
        }

        public async Task<IEnumerable<UserModel>> GetUsersByFamilyID(int familyID)
        {
            if (familyID < 0)
            {
                throw new ArgumentException("Family ID must be greater than zero.", nameof(familyID));
            }

            IEnumerable<User> users = await _userRepository.GetUsersByFamilyID(familyID);
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in users)
            {
                userModels.Add(GetUserModelFromUser(user));
            }
            return userModels;
        }

        public async Task<Family> UpdateFamily(Family family)
        {
            await _userRepository.UpdateFamily(family);
            return family;
        }

        public async Task<User> UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
            return user;
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _userRepository.UserExists(userId);
        }
    }
}
