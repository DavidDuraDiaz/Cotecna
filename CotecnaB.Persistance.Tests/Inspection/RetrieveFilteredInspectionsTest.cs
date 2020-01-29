using CotecnaB.Core.Entities;
using CotecnaB.Core.Enums;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RetrieveFilteredInspectionsTest : BaseTest
    {
        private Status testStatus = Status.New;

        [Fact]
        public void ShouldRetriveFilteredInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetFiltered(o => o.Status == testStatus);

                //Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void ShouldRetriveFilteredInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetFilteredAsync(o => o.Status == testStatus).Result;

                //Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void ShouldRetriveFilteredEagerInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetFilteredEager(o => o.Status == testStatus);

                //Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void ShouldRetriveFilteredEagerInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                IEnumerable<Inspection> result = repositori.GetFilteredEagerAsync(o => o.Status == testStatus).Result;

                //Assert
                Assert.Single(result);
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            CotecnaEFContext context = GetDBContext();

            List<Inspection> inspections = GetInspectionsWithInspectors().ToList();

            inspections.ForEach(o => o.Status = Status.Done );
            inspections.ElementAt(0).Status = testStatus;

            context.Inspection.AddRange(inspections);
            context.SaveChanges();

            return context;
        }
    }
}
