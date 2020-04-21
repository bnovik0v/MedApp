namespace MedApp.API.Resources
{
    public class CaseReportResource
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public PatientResource Patient { get; set; }
    }
}