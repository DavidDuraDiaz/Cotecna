using CotecnaB.Core.DTOs;
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
    public class CreateInspectionsTest : BaseTest
    {
        [Fact]
        public void ShouldCreateInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                Inspection Inspection1 = new Inspection()
                {
                    Customer = "Customer 1",
                    Address = "Address 1",
                    Observations = "Observation 1",
                    Status = Status.Done,
                    Created = DateTime.Today
                };

                //Act
                repositori.Create(Inspection1);
                Inspection result = repositori.Find(Inspection1.Id);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void ShouldCreateWithInspectorInpection()
        {
            using (var context = InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

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
                Inspection1.InspectionInspector = new List<InspectionInspector>() {
                    relation1
                };

                //Act
                repositori.Create(Inspection1);
                Inspection result = repositori.Find(Inspection1.Id);

                //Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void TestDuplicateInspectionInspectorLogic()
        {
            using (var context= InitAndGetDbContext())
            {
                //Arrange
                var repositori = new InspectionRepository(context);

                Inspector Inspector1 = new Inspector() { Id = Guid.NewGuid(), Name = "Inspector 1", Created = DateTime.Today };
                Inspection Inspection1 = new Inspection()
                {
                    Id = Guid.NewGuid(),
                    Customer = "Customer 1",
                    Address = "Address 1",
                    Observations = "Observation 1",
                    Status = Status.Done,
                    Created = DateTime.Today
                };
                InspectionInspector relation1 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(-1), InspectionId = Inspection1.Id, InspectorId = Inspector1.Id };
                Inspection1.InspectionInspector = new List<InspectionInspector>() {relation1};

                repositori.Create(Inspection1);
                context.SaveChanges();

                InspectorDTO inspectorDTO = new InspectorDTO() { Id = Inspector1.Id, Name = "Inspector 1" };
                InspectionDTO InspectionDTO = new InspectionDTO()
                {
                    Id = Inspection1.Id,
                    Customer = "Customer 1",
                    Address = "Address 1",
                    Observations = "Observation 1",
                    status = Status.Done,
                    InspectionDate = relation1.InspectionDate,
                    Inspectors = new List<InspectorDTO>() { inspectorDTO }
                };


                //Act
                Inspection found = repositori.GetSingleEagerAsync(o => o.Id == InspectionDTO.Id
                                        && o.InspectionInspector.Any(ii => ii.InspectionDate == InspectionDTO.InspectionDate)
                                        && o.InspectionInspector.Any(ii => InspectionDTO.Inspectors.Any(i => i.Id == ii.InspectorId))).Result;

                //Assert
                Assert.NotNull(found);
            }
        }

        private CotecnaEFContext InitAndGetDbContext()
        {
            return GetDBContext();
        }
    }
}
