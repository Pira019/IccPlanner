using Application.Constants;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Security
{
    /// <summary>
    /// Cette classe permet de définir les autorisations
    /// </summary>
    public static class AuthorizationPolicies
    {
        /// <summary>
        /// Permet de définir les accès
        /// </summary>
        /// <param name="options"> Fait reference <see cref="AuthorizationOptions"/></param>
        public static void AddPolicies(AuthorizationOptions options)
        {
            //Accès Roles

            //CAN_CREATE_ROLE
            options.AddPolicy(PolicyConstants.CAN_CREATE_ROLE, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) || 
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_CREATE_ROLE)
                    ));
            
            //CAN_READ_ROLE
            options.AddPolicy(PolicyConstants.CAN_READ_ROLE, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) || 
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_READ_ROLE)
                    )); 
            //Fin Accès Roles
        }
    }
}
