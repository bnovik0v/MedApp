using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedApp.Core.Models;
using MedApp.Core.Repositories;

namespace MedApp.DAL.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(MedAppDbContext context) : base(context) { }

        public async Task<Patient> GetWithCaseReportsByIdAsync(int id)
        {
            return await MyCaseReportDbContext.Patients.Include(a => a.CaseReports)
                                                 .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Patient>> GetAllWithCaseReportsAsync()
        {
            return await MyCaseReportDbContext.Patients.Include(a => a.CaseReports)
                                                 .ToListAsync();
        }

        public async Task<bool> IsExists(int id)
        {
            return await GetByIdAsync(id) is { };
        }

        private MedAppDbContext MyCaseReportDbContext => Context as MedAppDbContext;
    }
}