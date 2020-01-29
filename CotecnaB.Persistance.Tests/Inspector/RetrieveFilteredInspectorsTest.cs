using CotecnaB.Core.Entities;
using CotecnaB.Core.Enums;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RetrieveFilteredInspectorsTest : BaseTest
    {
        private string testName = "test";

        [Fact]
        public void ShouldRetriveFilteredInspectorsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                IEnumerable<Inspector> result = repositori.GetFiltered(o => o.Name == testName);

                //Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveFilteredInspectorsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                IEnumerable<Inspector> result = repositori.GetFiltered(o => o.Name == "anotherName");

                //Assert
                Assert.Empty(result);
            }
        }

        [Fact]
        public void ShouldRetriveFilteredEagerInspectorsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                IEnumerable<Inspector> result = repositori.GetFilteredAsync(o => o.Name == testName).Result;

                //Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveFilteredEagerInspectors()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                IEnumerable<Inspector> result = repositori.GetFilteredAsync(o => o.Name == "anotherName").Result;

                //Assert
                Assert.Empty(result);
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            CotecnaEFContext context = GetDBContext();

            List<Inspector> inspectors = GetInspectors().ToList();

            inspectors.ForEach(o => o.Name = "Wrong Name" );
            inspectors.ElementAt(0).Name = testName;

            context.Inspector.AddRange(inspectors);
            context.SaveChanges();

            return context;
        }
    }
}
