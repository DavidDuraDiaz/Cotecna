using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RetrieveAllInspectionsTest : BaseTest
    {
        [Fact]
        public void ShouldRetriveAllInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetAll();

                //Assert
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void ShouldRetriveAllInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetAllAsync().Result;

                //Assert
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void ShouldRetriveAllEagerInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetAllEager();

                //Assert
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void ShouldRetriveAllEagerInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetAllEagerAsync().Result;

                //Assert
                Assert.Equal(3, result.Count());
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            CotecnaEFContext context = GetDBContext();

            IEnumerable<Inspection> inspections = GetInspectionsWithInspectors();

            context.Inspection.AddRange(inspections);
            context.SaveChanges();

            return context;
        }
    }
}
