using CotecnaB.Core.Enums;
using System;
using System.Collections.Generic;

namespace CotecnaB.Core.DTOs
{
    public class InspectionDTO
    {
        public Guid Id { get; set; }
        public DateTime InspectionDate { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Observations { get; set; }
        public Status status { get; set; }

        public IEnumerable<InspectorDTO> Inspectors { get; set; }
    }
}
