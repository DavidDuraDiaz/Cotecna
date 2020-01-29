namespace CotecnaB.Core.Interfaces
{
    public interface IBaseEntity
    {
        void Update<TEntity>(TEntity entity) where TEntity : IBaseEntity;
    }
}
