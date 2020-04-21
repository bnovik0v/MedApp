using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MedApp.Core.Models
{
    public class Patient
    {
        public Patient()
        {
            CaseReports = new Collection<CaseReport>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<CaseReport> CaseReports { get; set; }

    }
}
