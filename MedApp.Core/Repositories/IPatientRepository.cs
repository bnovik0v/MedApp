using System.Collections.Generic;
using System.Threading.Tasks;
using MedApp.Core.Models;

namespace MedApp.Core.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetWithCaseReportsByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllWithCaseReportsAsync();
        Task<bool> IsExists(int id);
    }
}