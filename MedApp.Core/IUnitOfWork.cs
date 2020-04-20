using System;
using System.Threading.Tasks;
using MedApp.Core.Repositories;

namespace MedApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICaseReportRepository CaseReports { get; }
        IPatientRepository Patients { get; }
        Task<int> CommitAsync();
    }
}