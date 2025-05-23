﻿using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Test.Infrastructure.UnitTest.Repositories
{
    public class BaseRepositoryTest
    {
        private readonly IccPlannerContext _iccPlannerContext;
        private readonly BaseRepository<User> _baseRepository;
        public BaseRepositoryTest()
        {
            var option = new DbContextOptionsBuilder<IccPlannerContext>()
                   .UseInMemoryDatabase("fakeDb")
                   .Options;
            _iccPlannerContext = new IccPlannerContext(option);

            _baseRepository = new BaseRepository<User>(_iccPlannerContext);
            //EntityFrameworkManager.ContextFactory = context => new IccPlannerContext(option);

        }

        [Fact]
        public async Task Insert_ShouldInsertEntity()
        {
            // Arrange 
            var entity = new User
            {
                Email = "Test@gmail.com"
            };

            await _iccPlannerContext.Set<User>().AddAsync(entity); 

            // Act
            var result = await _baseRepository.Insert(entity);

            // Assert 
            result.Should().Be(entity); 
        } 

        [Fact]
        public async Task BulkInsertOptimizedAsync_ShouldReturnTask()
        {
            // Arrange 
            var dataTest = new List<User> 
            {
                new User
                {
                     Email = "Test@gmail.com"
                }
            };  
             
            // Assert
            await _baseRepository.InsertAllAsync(dataTest);
        }

        [Fact]
        public async Task IsExist_ShouldReturnTrue()
        {
            // Arrange 
            var objetId = Guid.NewGuid().ToString();
            var dataTest = new List<User>
            {
                new User
                {
                     Email = "Test@gmail01.com",
                     Id = objetId.ToString(),
                }
            };
            await _baseRepository.InsertAllAsync(dataTest);

            //Act
            var result = await _baseRepository.IsExist(objetId);
            
            // Assert
            result.Should().BeTrue();  
        }

        [Fact]
        public async Task IsExist_ShouldReturnFalse()
        {             
            //Act
            var result = await _baseRepository.IsExist(Arg.Any<string>());

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task IsExist_WhenInt_ShouldReturnFalse()
        {
            //Act
            var result = await _baseRepository.IsExist(Arg.Any<int>());

            // Assert
            result.Should().BeFalse();
        }
    }
}
