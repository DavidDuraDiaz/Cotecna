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
    public class CreateInspectorsTest : BaseTest
    {
        [Fact]
        public void ShouldCreateInpector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                Inspector Inspector1 = new Inspector()
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer 1",
                    Created = DateTime.Today
                };

                //Act
                repositori.Create(Inspector1);
                Inspector result = repositori.Find(Inspector1.Id);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldCreateWithInspectorInspector()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectorRepository(context);

                Inspector Inspector1 = new Inspector() { Name = "Inspector 1", Created = DateTime.Today };
                Inspection Inspection1 = new Inspection()
                {
                    Customer = "Customer 1",
                    Address = "Address 1",
                    Observations = "Observation 1",
                    Status = Status.Done,
                    Created = DateTime.Today
                };
                InspectionInspector relation1 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(-1), InspectionId = Inspection1.Id, InspectorId = Inspector1.Id };
                Inspector1.InspectionInspector = new List<InspectionInspector>() {
                    relation1
                };

                //Act
                repositori.Create(Inspector1);
                Inspector result = repositori.Find(Inspector1.Id);

                //Assert
                Assert.NotNull(result);
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            return GetDBContext();
        }
    }
}
