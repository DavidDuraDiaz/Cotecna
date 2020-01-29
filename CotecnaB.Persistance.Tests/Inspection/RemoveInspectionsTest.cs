using CotecnaB.Core.Entities;
using CotecnaB.Core.Enums;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RemoveInspectionsTest : BaseTest
    {
        private static Guid testId = Guid.NewGuid();

        [Fact]
        public void ShouldRemoveInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                //Act
                repositori.Delete(testId);
                Inspection result = repositori.Find(testId);

                //Assert
                Assert.False(result.Active);
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
