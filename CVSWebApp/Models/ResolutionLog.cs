using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public enum Status {
        Pending, Solved , Unresolved
    }

    public class ResolutionLog
    {
        public int ResolutionLogId { get; set; }
        public DateTime DateTime { get; set; }
        public string ResolutionDetails { get; set; }
        public int SurveyId { get; set; }
        public Status? Status { get; set; }
        public int UserId { get; set; }
        public int OutletId { get; set; }
    }
}
