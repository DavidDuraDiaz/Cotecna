using CotecnaB.Core.Entities;
using CotecnaB.Core.Enums;
using CotecnaB.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CotecnaB.Persistance.Migrations
{
    public class DataSeeder
    {
        public static void Initialize(CotecnaEFContext context)
        {
            if (!context.Inspection.Any() && !context.Inspector.Any())
            {
                #region Inspector
                Inspector Inspector1 = new Inspector() { Name = "Inspector 1", Created = DateTime.Today };
                Inspector Inspector2 = new Inspector() { Name = "Inspector 2", Created = DateTime.Today };
                Inspector Inspector3 = new Inspector() { Name = "Inspector 3", Created = DateTime.Today };

                context.Inspector.AddRange(
                    Inspector1,
                    Inspector2,
                    Inspector3
                );
                #endregion Inspector

                #region Inspection
                Inspection Inspection1 = new Inspection()
                {
                    Customer = "Customer 1",
                    Address = "Address 1",
                    Observations = "Observation 1",
                    Status = Status.Done,
                    Created = DateTime.Today
                };
                InspectionInspector relation1 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(-1), InspectionId = Inspection1.Id, InspectorId = Inspector1.Id };
                InspectionInspector relation2 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(-1), InspectionId = Inspection1.Id, InspectorId = Inspector2.Id };
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
                InspectionInspector relation3 = new InspectionInspector() { InspectionDate = DateTime.Today, InspectionId = Inspection2.Id, InspectorId = Inspector2.Id };
                InspectionInspector relation4 = new InspectionInspector() { InspectionDate = DateTime.Today, InspectionId = Inspection2.Id, InspectorId = Inspector3.Id };
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
                InspectionInspector relation5 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection3.Id, InspectorId = Inspector1.Id };
                InspectionInspector relation6 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection3.Id, InspectorId = Inspector3.Id };
                Inspection3.InspectionInspector = new List<InspectionInspector>() {
                    relation5,
                    relation6
                };

                Inspection Inspection4 = new Inspection()
                {
                    Customer = "Customer 4",
                    Address = "Address 4",
                    Observations = "Observation 4",
                    Status = Status.New,
                    Created = DateTime.Today
                };
                InspectionInspector relation7 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection4.Id, InspectorId = Inspector2.Id };
                InspectionInspector relation8 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection4.Id, InspectorId = Inspector3.Id };
                Inspection4.InspectionInspector = new List<InspectionInspector>() {
                    relation7,
                    relation8
                };

                Inspection Inspection5 = new Inspection()
                {
                    Customer = "Customer 5",
                    Address = "Address 5",
                    Observations = "Observation 5",
                    Status = Status.New,
                    Created = DateTime.Today
                };
                InspectionInspector relation9 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection5.Id, InspectorId = Inspector2.Id };
                InspectionInspector relation0 = new InspectionInspector() { InspectionDate = DateTime.Today.AddDays(2), InspectionId = Inspection5.Id, InspectorId = Inspector3.Id };
                Inspection5.InspectionInspector = new List<InspectionInspector>() {
                    relation9,
                    relation0
                };

                context.Inspection.AddRange(
                    Inspection1,
                    Inspection2,
                    Inspection3,
                    Inspection4,
                    Inspection5
                );
                #endregion Inspection

                context.SaveChanges();
            }
        }
    }
}
