using Application.Constants;
using Infrastructure.Security;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Security
{
    public class AuthorizationPoliciesTest
    {
        private readonly AuthorizationOptions _options;

        public AuthorizationPoliciesTest()
        {
            _options = Substitute.For<AuthorizationOptions>();
        }

        [Fact]
        public void AddPolicies_WhenCAN_READ_ROLE()
        {
            //Arrange
            var expectedCanCreateRolePolicy = new AuthorizationPolicyBuilder()
               .RequireAssertion(context =>
                   context.User.IsInRole(RolesConstants.ADMIN) ||
                   context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_CREATE_ROLE))
               .Build();

            var expectedCanReadRolePolicy = new AuthorizationPolicyBuilder()
                .RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) ||
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_READ_ROLE))
                .Build();

            //Act
            AuthorizationPolicies.AddPolicies(_options);

            //Assert
            _options.Received().AddPolicy(PolicyConstants.CAN_READ_ROLE, expectedCanReadRolePolicy);
            _options.Received().AddPolicy(PolicyConstants.CAN_CREATE_ROLE, expectedCanCreateRolePolicy);
        }  
    }
}
