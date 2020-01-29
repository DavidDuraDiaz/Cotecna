using CotecnaB.Abstractions.Interfaces.Repositories;
using CotecnaB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CotecnaB.Persistance.Repositories
{
    public class InspectionRepository : Repository<Inspection>, IInspectionRepository
    {
        public InspectionRepository(DbContext context) : base(context)
        {
        }

        public Inspection FindEager(Guid Id)
        {
            return _entities.Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).FirstOrDefault(o => o.Id == Id);
        }
        public Inspection GetSingleEager(Expression<Func<Inspection, bool>> predicate)
        {
            return _entities.Where(predicate)
                            .Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).FirstOrDefault();
        }
        public IEnumerable<Inspection> GetFilteredEager(Expression<Func<Inspection, bool>> predicate)
        {
            return _entities.Where(predicate)
                            .Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).ToList();
        }
        public IEnumerable<Inspection> GetAllEager()
        {
            return _entities.Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).ToList();
        }

        public async Task<Inspection> FindEagerAsync(Guid Id)
        {
            return await _entities.Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).FirstOrDefaultAsync(o => o.Id == Id);
        }
        public async  Task<Inspection> GetSingleEagerAsync(Expression<Func<Inspection, bool>> predicate)
        {
            return await _entities.Where(predicate)
                            .Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).FirstOrDefaultAsync();
        }
        public async  Task<IEnumerable<Inspection>> GetFilteredEagerAsync(Expression<Func<Inspection, bool>> predicate)
        {
            return await _entities.Where(predicate)
                            .Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).ToListAsync();
        }
        public async  Task<IEnumerable<Inspection>> GetAllEagerAsync()
        {
            return await _entities.Include(i => i.InspectionInspector)
                                .ThenInclude(ii => ii.Inspector).ToListAsync();
        }
    }
}
