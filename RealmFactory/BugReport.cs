using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmEngine
{
    public enum BugSeverity { Unknown, Severe, Bad, Moderate, Mild, Trivial };

    public class BugReport
    {
        public BugSeverity Severity { get; set; }
        public string Description { get; set; }
        public string ReproductionSteps { get; set; }

        public BugReport()
        {
        }
    }
}
