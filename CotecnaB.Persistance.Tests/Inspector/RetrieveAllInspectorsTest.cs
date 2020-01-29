using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RetrieveAllInspectorsTest : BaseTest
    {
        [Fact]
        public void ShouldRetriveAllInspectors()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                IEnumerable<Inspector> result = repositori.GetAll();

                //Assert
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void ShouldRetriveAllInspectorsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                IEnumerable<Inspector> result = repositori.GetAllAsync().Result;

                //Assert
                Assert.Equal(3, result.Count());
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            CotecnaEFContext context = GetDBContext();

            IEnumerable<Inspector> inspectors = GetInspectors();

            context.Inspector.AddRange(inspectors);
            context.SaveChanges();

            return context;
        }
    }
}
