using CotecnaB.Core.Entities;
using CotecnaB.Core.Enums;
using CotecnaB.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CotecnaB.Persistance.Tests
{
    public class BaseTest
    {
        public CotecnaEFContext GetDBContext()
        {
            var builder = new DbContextOptionsBuilder<CotecnaEFContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new CotecnaEFContext(builder.Options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public IEnumerable<Inspector> GetInspectors()
        {
            Inspector Inspector1 = new Inspector() { Name = "Inspector 1", Created = DateTime.Today };
            Inspector Inspector2 = new Inspector() { Name = "Inspector 2", Created = DateTime.Today };
            Inspector Inspector3 = new Inspector() { Name = "Inspector 3", Created = DateTime.Today };

            return new List<Inspector>() { Inspector1, Inspector2, Inspector3 };
        }

        public IEnumerable<Inspection> GetInspectionsWithInspectors()
        {
            IEnumerable<Inspector> inspectors = GetInspectors();

            Inspection Inspection1 = new Inspection()
            {
                Customer = "Customer 1",
                Address = "Address 1",
                Observations = "Observation 1",
                Status = Status.Done,
                Created = DateTime.Today
            };
            InspectionInspector relation1 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(-1), InspectionId = Inspection1.Id, InspectorId = inspectors.ElementAt(0).Id };
            InspectionInspector relation2 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(-1), InspectionId = Inspection1.Id, InspectorId = inspectors.ElementAt(1).Id };
            Inspection1.InspectionInspector = new List<InspectionInspector>() {
                    relation1,
                    relation2
                };

            Inspection Inspection2 = new Inspection()
            {
                Customer = "Customer 2",
                Address = "Address 2",
                Observations = "Observation 2",
                Status = Status.InProgress,
                Created = DateTime.Today
            };
            InspectionInspector relation3 = new InspectionInspector() { InspectionDate = DateTime.Today, InspectionId = Inspection2.Id, InspectorId = inspectors.ElementAt(1).Id };
            InspectionInspector relation4 = new InspectionInspector() { InspectionDate = DateTime.Today, InspectionId = Inspection2.Id, InspectorId = inspectors.ElementAt(2).Id };
            Inspection2.InspectionInspector = new List<InspectionInspector>() {
                    relation3,
                    relation4
                };

            Inspection Inspection3 = new Inspection()
            {
                Customer = "Customer 3",
                Address = "Address 3",
                Observations = "Observation 3",
                Status = Status.New,
                Created = DateTime.Today
            };
            InspectionInspector relation5 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection3.Id, InspectorId = inspectors.ElementAt(0).Id };
            InspectionInspector relation6 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection3.Id, InspectorId = inspectors.ElementAt(2).Id };
            Inspection3.InspectionInspector = new List<InspectionInspector>() {
                    relation5,
                    relation6
                };


            return new List<Inspection>() { Inspection1, Inspection2, Inspection3 };
        }

        public IEnumerable<Inspection> GetInspectionsWithoutInspectors()
        {
            Inspection Inspection1 = new Inspection()
            {
                Customer = "Customer 1",
                Address = "Address 1",
                Observations = "Observation 1",
                Status = Status.Done,
                Created = DateTime.Today
            };

            Inspection Inspection2 = new Inspection()
            {
                Customer = "Customer 2",
                Address = "Address 2",
                Observations = "Observation 2",
                Status = Status.InProgress,
                Created = DateTime.Today
            };

            Inspection Inspection3 = new Inspection()
            {
                Customer = "Customer 3",
                Address = "Address 3",
                Observations = "Observation 3",
                Status = Status.New,
                Created = DateTime.Today
            };

            return new List<Inspection>() { Inspection1, Inspection2, Inspection3 };
        }
    }
}
