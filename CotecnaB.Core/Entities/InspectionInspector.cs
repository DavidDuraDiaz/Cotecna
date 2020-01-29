using System;

namespace CotecnaB.Core.Entities
{
    public class InspectionInspector
    {
        public Guid InspectionId { get; set; }
        public Inspection Inspection { get; set; }
        public Guid InspectorId { get; set; }
        public Inspector Inspector { get; set; }

        public DateTime InspectionDate { get; set; }
    }
}
