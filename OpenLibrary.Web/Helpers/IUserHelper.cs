﻿using Microsoft.AspNetCore.Identity;
using OpenLibrary.Common.Enums;
using OpenLibrary.Common.Models;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Models;
using System;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public interface IUserHelper
    {
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);
       
        Task<UserEntity> GetUserAsync(string email);

        Task<UserEntity> AddUserAsync(FacebookProfile model);

        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);

        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType);


        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task RemoveUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
    }
}
