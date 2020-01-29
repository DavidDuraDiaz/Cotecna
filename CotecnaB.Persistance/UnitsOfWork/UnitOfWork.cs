using CotecnaB.Abstractions.Interfaces.Repositories;
using CotecnaB.Abstractions.Interfaces.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CotecnaB.Persistance.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IInspectorRepository Inspector { get; private set; }
        public IInspectionRepository Inspection { get; private set; }

        public UnitOfWork(DbContext context, IInspectionRepository inspection, IInspectorRepository inspector)
        {
            _context = context;
            Inspection = inspection;
            Inspector = inspector;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
