using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RetrieveSingleInspectionsTest : BaseTest
    {
        public static Guid testId = Guid.NewGuid();

        [Fact]
        public void ShouldRetriveSingleInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingle(o => o.Id == testId);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveSingleInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingle(o => o.Id == Guid.NewGuid());

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldRetriveSingleInpectionAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingleAsync(o => o.Id == testId).Result;

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveSingleInpectionAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingleAsync(o => o.Id == Guid.NewGuid()).Result;

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldRetriveSingleEagerInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingleEager(o => o.Id == testId);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveSingleEagerInpections()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingleEager(o => o.Id == Guid.NewGuid());

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldRetriveSingleEagerInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingleEagerAsync(o => o.Id == testId).Result;

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveSingleEagerInpectionsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                Inspection result = repositori.GetSingleEagerAsync(o => o.Id == Guid.NewGuid()).Result;

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
