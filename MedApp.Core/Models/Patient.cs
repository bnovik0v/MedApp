using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MedApp.Core.Models
{
    public class Patient
    {
        public Patient()
        {
            CaseReports = new Collection<Patient>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Patient> CaseReports { get; set; }

    }
}
