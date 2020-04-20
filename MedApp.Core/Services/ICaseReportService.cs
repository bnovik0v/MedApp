using System.Collections.Generic;
using System.Threading.Tasks;
using MedApp.Core.Models;

namespace MedApp.Core.Services
{
    public interface ICaseReportService
    {
        Task<CaseReport> CreateCaseReport(CaseReport newCaseReport);
        Task<CaseReport> GetCaseReportById(int id);
        Task<IEnumerable<CaseReport>> GetAllWithPatient();
        Task<IEnumerable<CaseReport>> GetCaseReportsByPatientId(int patientId);
        Task UpdateCaseReport(int id, CaseReport caseReport);
        Task DeleteCaseReport(CaseReport caseReport);
    }
}