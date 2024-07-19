using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TimeLogger.Models;

namespace TimeLogger.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<X_JIRA_WORKLOG> X_JIRA_WORKLOG { get; set; }
    }
}