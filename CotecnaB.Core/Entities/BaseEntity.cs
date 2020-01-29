using CotecnaB.Core.Interfaces;
using System;

namespace CotecnaB.Core.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public bool Active { get; set; }

        public abstract void Update<TEntity>(TEntity entity) where TEntity : IBaseEntity;

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
