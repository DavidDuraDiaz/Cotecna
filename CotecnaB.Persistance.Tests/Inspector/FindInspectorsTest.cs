using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class FindInspectorsTest : BaseTest
    {
        public static Guid testId = Guid.NewGuid();

        [Fact]
        public void ShouldFindInspector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.Find(testId);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotFindInspector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.Find(Guid.NewGuid());

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldFindInspectorsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.FindAsync(testId).Result;

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotFindInspectorsAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.FindAsync(Guid.NewGuid()).Result;

                //Assert
                Assert.Null(result);
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            CotecnaEFContext context = GetDBContext();

            IEnumerable<Inspector> inspectors = GetInspectors();

            inspectors.ElementAt(0).Id = testId;

            context.Inspector.AddRange(inspectors);
            context.SaveChanges();

            return context;
        }
    }
}
