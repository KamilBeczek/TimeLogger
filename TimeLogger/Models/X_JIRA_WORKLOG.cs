using System.ComponentModel.DataAnnotations;

namespace TimeLogger.Models
{
    public class X_JIRA_WORKLOG
    {
        [Key]
        public int REF { get; set; }
        public string AUTHOR { get; set; }
        public string PROJECT { get; set; }
        public string ISSUE { get; set; }
        public string ISSUESUMMARY { get; set; }
        public string QUALIFICATION { get; set; }
        public double TIMESPENT { get; set; }
        public DateTime WORKLOGDATE { get; set; }
        public DateTime WORKLOGSTART { get; set; }
        public DateTime REGTIMESTAMP { get; set; }
        public string DESCRIPT { get; set; }
        public string COMPONENTS { get; set; }
    }
}
