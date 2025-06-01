using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///   Cette interface gère les action d'un compte
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Créer un compte
        /// </summary>
        /// <param name="user"><see cref="User"/>  </param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <returns>Objet IdentityResult</returns>
        public Task<IdentityResult> CreateAsync(User user, string password);
        public Task<User?> FindByEmailAsync(string email);

        /// <summary>
        /// Trouver un compte(User) par l'Id
        /// </summary>
        /// <param name="id">Id de l'utilisateur</param>
        /// <returns></returns>
        public Task<User?> FindByIdAsync(string id);

        /// <summary>
        /// Confirmer l'adresse email d'un compte
        /// </summary>
        /// <param name="user">L'utilisateur a valider</param>
        /// <param name="token">le token de verification</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> ConfirmAccountEmailAsync(User user, string token);

        /// <summary>
        /// Permet d'authentifier un utilisateur
        /// </summary>
        /// <param name="email">Email ou le user name d'un utilisateur </param>
        /// <param name="password">Le mot de passe de l'utilisateur</param>
        /// <param name="isPersistent">Flag indiquant si le cookie de connexion doit persister après la fermeture du navigateur.</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="SignInResult"/> de l'opération </returns>
        public Task<SignInResult> SignIn(string email, string password, bool isPersistent = false);

        /// <summary>
        /// Permet d’indiquer s'il existe un utilisateur avec le role ADMIN/>
        /// </summary>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant une valeur bool </returns>
        public Task<bool> IsAdminExistsAsync();

        /// <summary>
        /// Ajouter un role a un utilisateur
        /// </summary>
        /// <param name="user">Utilisateur</param>
        /// <param name="roleName">Nom de role</param>
        public Task AddUserRole(User user, string roleName);

        /// <summary>
        /// Récupérer les roles d'un User <see cref="User"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant la liste de roles </returns>
        public Task<IList<string>> GetUserRoles(User user);

        /// <summary>
        /// Permet de trouver un membre par ID
        /// </summary>
        /// <param name="memberId">L'Id du membre</param>
        /// <returns></returns>
        public Task<bool> IsMemberExist(string memberId);
        public Task<Member?> FindMemberByUserId(string userId);

        /// <summary>
        /// Récupérer Member qui est(en cours) authentifier 
        /// </summary>
        /// <param name="userAuthId">L'Id du membre</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="Member"/> de l'opération </returns>
        public Task<Member?> GetAuthMemberAsync(Guid? userAuthId);


        /// <summary>
        /// Permet enregistrement les comptes de membres importer
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Task<int> SaveImportedMembersDepartment(IEnumerable<User> users);

        /// <summary>
        /// Permet de  filter par numéro de contact pour évité les doublure 
        /// </summary>
        /// <param name="users">Liste des utilisateurs</param>
        /// <returns> les users filtres </returns>
        public Task<List<User>> FilterUsersWithUniquePhoneNumbers(IEnumerable<User> users);  
    }
}
