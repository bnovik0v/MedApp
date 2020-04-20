using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MedApp.Core;
using MedApp.Core.Models;
using MedApp.Core.Services;

namespace MedApp.BLL
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Patient> CreatePatient(Patient newPatient)
        {
            if (newPatient is null)
                throw new NullReferenceException();

            await _unitOfWork.Patients.AddAsync(newPatient);
            await _unitOfWork.CommitAsync();

            return newPatient;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _unitOfWork.Patients.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _unitOfWork.Patients.GetAllAsync();
        }

        public async Task UpdatePatient(int id, Patient patient)
        {
            if (!await _unitOfWork.Patients.IsExists(id))
                throw new NullReferenceException();

            if (patient.FullName.Length == 0 || patient.FullName.Length > 50)
                throw new InvalidDataException();

            var patientToBeUpdated = await GetPatientById(id);
            patientToBeUpdated.FullName = patient.FullName;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePatient(Patient patient)
        {
            if (!await _unitOfWork.Patients.IsExists(patient.Id))
                throw new NullReferenceException();

            _unitOfWork.Patients.Remove(patient);

            await _unitOfWork.CommitAsync();
        }
    }
}