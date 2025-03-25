using Domain.Entities;
using FluentAssertions;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Configurations
{
    public class TokenProviderTest
    {
        private IOptions<AppSetting> _options;
        private TokenProvider _tokenProvider;

        public TokenProviderTest()
        {
            _options = Substitute.For<IOptions<AppSetting>>();

            var mockAppSetting = new AppSetting
            {
                AppName = "app",
                JwtSetting = new JwtSetting
                {
                    Secret = "my_super_secret_key_1234567890_ABCDEFmy_super_secret_key_1234567890_ABCDEFmy_super_secret_key_1234567890_ABCDEF",  // Assurez-vous que la clé a une longueur suffisante
                    Audiance = "Audience",
                    ExpirationInMinutes = 60,
                    Issuer = "issuer"
                }
            };
            _options.Value.Returns(mockAppSetting);
            _tokenProvider = new TokenProvider(_options);
        }

        [Fact]
        public void Create_GivenUserAndRoles_ShouldReturnValidToken()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
            };
            var userRolesName = new List<string> { "Admin", "User" };

            // Act
            var token = _tokenProvider.Create(user, userRolesName);

            // Assert
            token.Should().NotBeNull();
            //  token.Should().Contain(user.Id.ToString());
        }

    }
}
