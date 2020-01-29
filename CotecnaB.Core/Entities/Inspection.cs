using CotecnaB.Core.Enums;
using System;
using System.Collections.Generic;

namespace CotecnaB.Core.Entities
{
    public class Inspection : BaseEntity
    {
        //public DateTime InspectionDate { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Observations { get; set; }
        public Status Status { get; set; }

        public virtual IEnumerable<InspectionInspector> InspectionInspector { get; set; }

        private void Update(Inspection entity)
        {
            //InspectionDate = entity.InspectionDate;
            Customer = entity.Customer;
            Address = entity.Address;
            Observations = entity.Observations;
        }

        public override void Update<TEntity>(TEntity entity)
        {
            Inspection inspection = entity as Inspection;
            Update(inspection);
        }
    }
}
