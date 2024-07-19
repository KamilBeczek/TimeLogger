namespace TimeLogger.Models
{
    public class WorklogViewModel
    {
        public double ProductiveHours { get; set; }
        public double HelpDeskHours { get; set; }
        public double DevelopmentHours { get; set; }
        public double InternalHours { get; set; }
        public IEnumerable<X_JIRA_WORKLOG> Worklogs { get; set; }
    }
}
