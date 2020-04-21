using System.Threading.Tasks;
using MedApp.Core;
using MedApp.Core.Repositories;
using MedApp.DAL.Repositories;

namespace MedApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedAppDbContext _context;
        private CaseReportRepository _caseReportRepository;
        private PatientRepository _patientRepository;

        public UnitOfWork(MedAppDbContext context)
        {
            _context = context;
        }

        public ICaseReportRepository CaseReports => _caseReportRepository ??= new CaseReportRepository(_context);

        public IPatientRepository Patients => _patientRepository ??= new PatientRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}