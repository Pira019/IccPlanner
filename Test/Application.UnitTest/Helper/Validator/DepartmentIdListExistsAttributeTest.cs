using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;
using Application.Interfaces.Repositories;
using FluentAssertions;
using NSubstitute;

namespace Test.Application.UnitTest.Helper.Validator
{
    public class DepartmentIdListExistsAttributeTest
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ValidationContext _validationContext;

        public DepartmentIdListExistsAttributeTest()
        {
            _departmentRepository = Substitute.For<IDepartmentRepository>();

            _validationContext = new ValidationContext(new object(), null, new Dictionary<object, object>
        {
            {
               typeof(IDepartmentRepository), _departmentRepository
            }
        });
        }

        [Fact]
        public void IsValid_ShouldReturnSuccess_WhenAllDepartmentsExist()
        {
            // Arrange
            var validIds = new List<int?> { 1, 2, 3 };
            _departmentRepository.GetValidDepartmentId(validIds).Returns(validIds);

            var validator = new DepartmentIdListExistsAttribute();
            var value = "1,2,3";

            // Act
            var result = validator.GetValidationResult(value, _validationContext);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
