using CotecnaB.Abstractions.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace CotecnaB.Abstractions.Interfaces.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IInspectorRepository Inspector { get; }
        IInspectionRepository Inspection { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
