using CotecnaB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CotecnaB.Abstractions.Interfaces.Repositories
{
    public interface IInspectionRepository : IRepository<Inspection>
    {
        Inspection FindEager(Guid Id);
        Inspection GetSingleEager(Expression<Func<Inspection, bool>> predicate);
        IEnumerable<Inspection> GetFilteredEager(Expression<Func<Inspection, bool>> predicate);
        IEnumerable<Inspection> GetAllEager();

        Task<Inspection> FindEagerAsync(Guid Id);
        Task<Inspection> GetSingleEagerAsync(Expression<Func<Inspection, bool>> predicate);
        Task<IEnumerable<Inspection>> GetFilteredEagerAsync(Expression<Func<Inspection, bool>> predicate);
        Task<IEnumerable<Inspection>> GetAllEagerAsync();
    }
}
