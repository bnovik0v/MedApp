using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedApp.Core.Models;
using MedApp.Core.Repositories;

namespace MedApp.DAL.Repositories
{
    public class CaseReportRepository : Repository<CaseReport>, ICaseReportRepository
    {
        public CaseReportRepository(MedAppDbContext context) : base(context) { }

        public async Task<CaseReport> GetWithPatientByIdAsync(int id)
        {
            return await MyCaseReportDbContext.CaseReports.Include(m => m.Patient)
                                                .SingleOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task<IEnumerable<CaseReport>> GetAllWithPatientAsync()
        {
            return await MyCaseReportDbContext.CaseReports.Include(m => m.Patient)
                                                .ToListAsync();
        }


        public async Task<IEnumerable<CaseReport>> GetAllWithPatientByPatientIdAsync(int patientId)
        {
            return await MyCaseReportDbContext.CaseReports.Include(m => m.Patient)
                                                .Where(m => m.PatientId == patientId)
                                                .ToListAsync();
        }

        public async Task<bool> IsExists(int id)
        {
            return await GetByIdAsync(id) is { };
        }

        private MedAppDbContext MyCaseReportDbContext => Context as MedAppDbContext;
    }
}