using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class FindInspectionsTest : BaseTest
    {
        public static Guid testId = Guid.NewGuid();

        [Fact]
        public void ShouldFindInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.Find(testId);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotFindInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.Find(Guid.NewGuid());

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldFindEagerInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.FindEager(testId);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotFindEagerInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.FindEager(Guid.NewGuid());

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldFindEagerInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.FindEagerAsync(testId).Result;

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotFindEagerInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.FindEagerAsync(Guid.NewGuid()).Result;

                //Assert
                Assert.Null(result);
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            CotecnaEFContext context = GetDBContext();

            IEnumerable<Inspection> inspections = GetInspectionsWithInspectors();

            inspections.ElementAt(0).Id = testId;

            context.Inspection.AddRange(inspections);
            context.SaveChanges();

            return context;
        }
    }
}
