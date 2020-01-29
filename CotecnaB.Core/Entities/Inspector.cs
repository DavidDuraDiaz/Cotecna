using System.Collections.Generic;

namespace CotecnaB.Core.Entities
{
    public class Inspector : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<InspectionInspector> InspectionInspector { get; set; }

        private void Update(Inspector entity)
        {
            Name = entity.Name;
        }

        public override void Update<TEntity>(TEntity entity)
        {
            Inspector inspection = entity as Inspector;
            Update(inspection);
        }
    }
}
