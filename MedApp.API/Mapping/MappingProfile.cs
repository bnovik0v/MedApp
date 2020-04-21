using AutoMapper;
using MedApp.API.Resources;
using MedApp.Core.Models;

namespace MedApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CaseReport, CaseReportResource>().ReverseMap();
            CreateMap<Patient, PatientResource>().ReverseMap();

            CreateMap<SaveCaseReportResource, CaseReport>();
            CreateMap<SavePatientResource, Patient>();
        }
    }
}