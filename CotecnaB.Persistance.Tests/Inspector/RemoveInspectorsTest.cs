using CotecnaB.Core.Entities;
using CotecnaB.Persistance.Contexts;
using CotecnaB.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class RemoveInspectorsTest : BaseTest
    {
        private static Guid testId = Guid.NewGuid();

        [Fact]
        public void ShouldRemoveInspector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                //Act
                repositori.Delete(testId);
                Inspector result = repositori.Find(testId);

                //Assert
                Assert.False(result.Active);
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
