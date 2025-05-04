using Application.Constants;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 

namespace Infrastructure.Repositories
{
    /// <summary>
    ///   Permet de faire des actions dans la DB pour un compte
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IccPlannerContext _iccPlannerContext;
        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, IccPlannerContext iccPlannerContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iccPlannerContext = iccPlannerContext;
        }

        public async Task AddUserRole(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public Task<IdentityResult> ConfirmAccountEmailAsync(User user, string token)
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public async Task<List<User>> FilterUsersWithUniquePhoneNumbers(IEnumerable<User> users)
        {
            var phoneNumbersToInsert = new HashSet<string>(users.Select(u => u.PhoneNumber!));

            var existingPhoneNumbers = await _iccPlannerContext.Users
              .Where(u => phoneNumbersToInsert.Contains(u.PhoneNumber!))
              .Select(u => u.PhoneNumber)
              .ToListAsync();

            return users.Where(u => !existingPhoneNumbers.Contains(u.PhoneNumber)).ToList();
        }

        /// <summary>
        /// Trouver un utilisateur par code.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Objet User</returns>
        public Task<User?> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<User?> FindByIdAsync(string id)
        {
            return (_userManager.FindByIdAsync(id));
        }

        public async Task<Domain.Entities.Member?> FindMemberByUserId(string userId)
        {
            return await _iccPlannerContext.Members.FirstOrDefaultAsync(x => x.User != null && x.User.Id == userId);
        }

        public async Task<Member?> GetAuthMember(Guid? userAuthId)
        {
            return await FindMemberByUserId(userAuthId.ToString()!);
        }

        public async Task<IList<string>> GetUserRoles(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsAdminExistsAsync()
        {
            var isAdminExists = await _userManager.GetUsersInRoleAsync(RolesConstants.ADMIN);
            return isAdminExists.Any();
        }

        public Task<bool> IsMemberExist(string memberId)
        {
            return _iccPlannerContext.Members.AnyAsync(x => x.Id == Guid.Parse(memberId));
        }

        public async Task<int> SaveImportedMembersDepartment(IEnumerable<User> users)
        {
            var usersImported = await FilterUsersWithUniquePhoneNumbers(users);
           /* await _iccPlannerContext.Users.BulkInsertAsync(usersImported, options =>
            {
                options.IncludeGraph = true;
            });*/

            return usersImported.Count; 
        }
        public Task<SignInResult> SignIn(string email, string password, bool isPersistent = false)
        {
            return _signInManager.PasswordSignInAsync(email, password, isPersistent, true);
        }

    }
}
