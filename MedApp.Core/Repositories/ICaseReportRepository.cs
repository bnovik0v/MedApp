using System.Collections.Generic;
using System.Threading.Tasks;
using MedApp.Core.Models;

namespace MedApp.Core.Repositories
{
    public interface ICaseReportRepository : IRepository<CaseReport>
    {
        Task<CaseReport> GetWithPatientByIdAsync(int id);
        Task<IEnumerable<CaseReport>> GetAllWithPatientAsync();
        Task<IEnumerable<CaseReport>> GetAllWithPatientByPatientIdAsync(int patientId);
        Task<bool> IsExists(int id);
    }
}