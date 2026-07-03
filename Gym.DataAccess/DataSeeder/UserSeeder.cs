using Gym.DataAccess.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.DataSeeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync( UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager,
         IConfiguration config)
        {
            await EnsureRoleExists(roleManager, IdentityRoles.SuperAdmin);
            await EnsureRoleExists(roleManager, IdentityRoles.Admin);
            var superAdminEmail = config["IdentitySeed:SuperAdminEmail"]
            ?? throw new InvalidOperationException("SuperAdminEmail is missed.");
            var superAdminPassword = config["IdentitySeed:SuperAdminPassword"]
           ?? throw new InvalidOperationException("SuperAdminPassword is missed.");
            var adminEmail = config["IdentitySeed:AdminEmail"]
            ?? throw new InvalidOperationException("AdminEmail is missed.");

            var adminPassword = config["IdentitySeed:AdminPassword"]
                ?? throw new InvalidOperationException("AdminPassword is missed.");
            var superAdmin = await EnsureUserCreated( userManager, superAdminEmail,superAdminPassword);
            await EnsureUserInRole( userManager,superAdmin, IdentityRoles.SuperAdmin);
            var admin = await EnsureUserCreated( userManager, adminEmail,adminPassword);
            await EnsureUserInRole( userManager,admin,IdentityRoles.Admin);
        }
        private static async Task EnsureRoleExists( RoleManager<ApplicationRole> roleManager,string roleName)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var role = new ApplicationRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create role '{roleName}': {errors}");
                }
            }
        }
        private static async Task<ApplicationUser> EnsureUserCreated(UserManager<ApplicationUser> userManager,
        string email,string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                return user;
            }

            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user '{email}': {errors}");
            }

            return user;
        }
        private static async Task EnsureUserInRole(
    UserManager<ApplicationUser> userManager,
    ApplicationUser user,
    string roleName)
        {
            var isInRole = await userManager.IsInRoleAsync(user, roleName);

            if (!isInRole)
            {
                var result = await userManager.AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to add user '{user.Email}' to role '{roleName}': {errors}");
                }
            }
        }
    }
}
