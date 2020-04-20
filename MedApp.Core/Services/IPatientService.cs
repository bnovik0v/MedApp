using System.Collections.Generic;
using System.Threading.Tasks;
using MedApp.Core.Models;

namespace MedApp.Core.Services
{
    public interface IPatientService
    {
        Task<Patient> CreatePatient(Patient newPatient);
        Task<Patient> GetPatientById(int id);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task UpdatePatient(int id, Patient patient);
        Task DeletePatient(Patient patient);
    }
}