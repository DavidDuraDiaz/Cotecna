using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RetrieveSingleInspectorsTest : BaseTest
    {
        public static Guid testId = Guid.NewGuid();

        [Fact]
        public void ShouldRetriveSingleInspector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.GetSingle(o => o.Id == testId);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveSingleInspector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.GetSingle(o => o.Id == Guid.NewGuid());

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void ShouldRetriveSingleInspectorAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.GetSingleAsync(o => o.Id == testId).Result;

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldNotRetriveSingleInspectorAsync()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                Inspector result = repositori.GetSingleAsync(o => o.Id == Guid.NewGuid()).Result;

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
