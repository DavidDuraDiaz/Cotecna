using CotecnaB.Abstractions.Interfaces.Repositories;
using CotecnaB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CotecnaB.Persistance.Repositories
{
    public class InspectorRepository : Repository<Inspector>, IInspectorRepository
    {
        public InspectorRepository(DbContext context) : base(context)
        {
        }
    }
}
