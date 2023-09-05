using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestBackend.Api.Models;
using TestBackend.Api.Models.Data;
using TestBackend.Common.Models;

namespace TestBackend.Api.Controllers.Tests
{
    [TestClass()]
    public class PersonsControllerTests
    {
        [TestMethod()]
        public void CreatePersonTests()
        {
            // Arrange  
            var appContext = GetTestApplicationContext();
            var loggerMock = new Mock<ILogger<PersonsController>>();
            var controller = new PersonsController(appContext, loggerMock.Object);

            var personModel = new PersonModel
            {
                Name = "Test",
                DisplayName = "Test",
                Skills = new List<SkillModel>
                {
                    new SkillModel("Test", 7),
                    new SkillModel("Test", 7)
                }
            };

            // Act  
            var result = controller.CreatePerson(personModel);

            // Assert  
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<OkResult>(result);
            Xunit.Assert.Equal(200, (result as OkResult)?.StatusCode);
        }



        [TestMethod()]
        public void PutPersonTest()
        {
            // Arrange             

            var appContext = GetNewInitAppContext();
            var loggerMock = new Mock<ILogger<PersonsController>>();
            var controller = new PersonsController(appContext, loggerMock.Object);

            var id = appContext.Persons.FirstOrDefault()?.Id ?? 1;
            var personModel = new PersonModel
            {
                Id = id,
                Name = "TestUpdate",
                DisplayName = "TestUpdate",
                Skills = new List<SkillModel>
                {
                    new SkillModel("TestUpdate", 7),
                    new SkillModel("TestUpdate", 7)
                }
            };

            // Act 
            var result = controller.PutPerson(id, personModel);

            // Assert  
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<OkResult>(result);
            Xunit.Assert.Equal(200, (result as OkResult)?.StatusCode);

        }

        [TestMethod()]
        public void DeletePersonTest()
        {
            // Arrange 
            var appContext = GetNewInitAppContext();
            var loggerMock = new Mock<ILogger<PersonsController>>();
            var controller = new PersonsController(appContext, loggerMock.Object);

            GetNewInitAppContext();

            var id = appContext.Persons.FirstOrDefault()?.Id ?? 1;

            // Act 
            var result = controller.DeletePerson(id);

            // Assert  
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<OkResult>(result);
            Xunit.Assert.Equal(200, (result as OkResult)?.StatusCode);

        }

        [TestMethod()]
        public void GetAllPersonsTest()
        {
            // Arrange 
            var appContext = GetNewInitAppContext();
            var loggerMock = new Mock<ILogger<PersonsController>>();
            var controller = new PersonsController(appContext, loggerMock.Object);
            

            // Act 
            var result = controller.GetAllPersons();

            // Assert  
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(200, (result as OkObjectResult)?.StatusCode);
        }

        [TestMethod()]
        public void GetPersonTest()
        {
            // Arrange            
            var appContext = GetTestApplicationContext();
            var loggerMock = new Mock<ILogger<PersonsController>>();
            var controller = new PersonsController(appContext, loggerMock.Object);
            GetNewInitAppContext();

            var id = appContext.Persons.FirstOrDefault()?.Id ?? 1;

            // Act 
            var result = controller.GetPerson(id);

            // Assert  
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(200, (result as OkObjectResult)?.StatusCode);
        }

        private static ApplicationContext GetNewInitAppContext()
        {
            var dbTest = GetTestApplicationContext();

            dbTest.Add(new Person()
            {
                Name = "TestGetAllPersons",
                DisplayName = "TestGetAllPersons",
                Skills = new List<Skill>
                {
                    new Skill("TestGetAllPersons", 7),
                    new Skill("TestGetAllPersons", 7)
                }
            });
            dbTest.SaveChanges();
            return dbTest;
        }

        private static ApplicationContext GetTestApplicationContext()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseInMemoryDatabase("PersonControllerTest")
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;
            return new ApplicationContext(contextOptions);
        }

    }
}