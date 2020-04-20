
namespace MedApp.Core.Models
{
    class CaseReport
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
    }
}
