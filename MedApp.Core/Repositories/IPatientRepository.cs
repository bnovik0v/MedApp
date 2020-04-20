using System.Collections.Generic;
using System.Threading.Tasks;
using MedApp.Core.Models;

namespace MedApp.Core.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetWithCaseReportByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllWithCaseReportAsync();
        Task<bool> IsExists(int id);
    }
}