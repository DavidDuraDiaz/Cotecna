using System;
using System.Collections.Generic;

namespace CotecnaB.Core.DTOs
{
    public class InspectorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<InspectionDTO> Inspections { get; set; }
    }
}
