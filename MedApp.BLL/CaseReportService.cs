using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MedApp.Core;
using MedApp.Core.Models;
using MedApp.Core.Services;

namespace MedApp.BLL
{
    public class CaseReportService : ICaseReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CaseReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CaseReport> CreateCaseReport(CaseReport newCaseReport)
        {
            if (newCaseReport is null)
                throw new NullReferenceException();

            await _unitOfWork.CaseReports.AddAsync(newCaseReport);
            await _unitOfWork.CommitAsync();

            return newCaseReport;
        }

        public async Task<CaseReport> GetCaseReportById(int id)
        {
            return await _unitOfWork.CaseReports.GetWithPatientByIdAsync(id);
        }

        public async Task<IEnumerable<CaseReport>> GetCaseReportsByPatientId(int artistId)
        {
            return await _unitOfWork.CaseReports.GetAllWithPatientByPatientIdAsync(artistId);
        }

        public async Task<IEnumerable<CaseReport>> GetAllWithPatient()
        {
            return await _unitOfWork.CaseReports.GetAllWithPatientAsync();
        }

        public async Task UpdateCaseReport(int id, CaseReport caseReport)
        {
            if (!await _unitOfWork.CaseReports.IsExists(id))
                throw new NullReferenceException();

            if (caseReport.Diagnosis.Length <= 0 || caseReport.Diagnosis.Length > 50 || caseReport.PatientId <= 0)
                throw new InvalidDataException();

            var caseReportToBeUpdated = await GetCaseReportById(id);
            caseReportToBeUpdated.Diagnosis = caseReport.Diagnosis;
            caseReportToBeUpdated.PatientId = caseReport.PatientId;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCaseReport(CaseReport caseReport)
        {
            if (!(await _unitOfWork.CaseReports.IsExists(caseReport.Id)))
                throw new NullReferenceException();

            _unitOfWork.CaseReports.Remove(caseReport);

            await _unitOfWork.CommitAsync();
        }
    }
}