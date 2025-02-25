using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;
using Application.Interfaces.Repositories;
using Domain.Abstractions;
using FluentAssertions;
using Infrastructure.Repositories;
using NSubstitute;

namespace Test.Application.UnitTest.Helper.Validator
{
    public class DepartmentExistsAttributeTest
    {
        public readonly IDepartmentRepository _departmentRepository;
        public readonly DepartmentExistsAttribute _departmentExistsAttribute;
        public DepartmentExistsAttributeTest() 
        { 
            _departmentRepository = Substitute.For<IDepartmentRepository>();
            _departmentExistsAttribute = new DepartmentExistsAttribute();
        }  

        [Fact]
        public void ValidationResult_WhenDepartmentNotExist()
        {
            //Arrange
            var departmenId = 1;
            var validationContext = new ValidationContext(new object(), null, null)
            {
                Items = { { typeof(IDepartmentRepository), _departmentRepository } }
            };


            _departmentRepository.IsDepartmentIdExists(departmenId).Returns(Task.FromResult(false));

            //Act
            var validationResult = _departmentExistsAttribute.GetValidationResult(departmenId, validationContext);

            //Asset 
            validationResult!.ErrorMessage.Should().Be(DepartmentError.DEPARTMENT_NOT_EXISTS.Message);

        }
        
        [Fact]
        public void ValidationResult_WhenDepartmentExist_ShouldReturnSuccess()
        {
            //Arrange
            var departmenId = 1;
            var validationContext = new ValidationContext(new object(), null, null)
            {
                Items = { { typeof(IDepartmentRepository), _departmentRepository } }
            };


            _departmentRepository.IsDepartmentIdExists(departmenId).Returns(Task.FromResult(true));

            //Act
            var validationResult = _departmentExistsAttribute.GetValidationResult(departmenId, validationContext);

            //Asset 
            validationResult.Should().Be(ValidationResult.Success);

        }
    }
}
