using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Workflows.Reporting
{
    public class ReportTemplatesWorkflow
    {
        private readonly IVantageContext context;

        public ReportTemplatesWorkflow(IVantageContext context)
        {
            this.context = context;
        }

        public IQueryable<ReportTemplate> Templates => context.ReportTemplates;

        public IQueryable<ReportLogo> Logos(string licenseIssuerId, string templateName)
        {
            return context.ReportLogos.Where(l => l.LicenseIssuerId == licenseIssuerId && l.TemplateName == templateName);
        }

        public async Task AddAsync(ReportTemplate template)
        {
            context.ReportTemplates.Add(template);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(ReportTemplate template)
        {
            context.ReportTemplates.Remove(template);
            await context.SaveChangesAsync();
        }

        public async Task AddLogoAsync(ReportLogo logo)
        {
            context.ReportLogos.Add(logo);
            await context.SaveChangesAsync();
        }

        public async Task RemoveLogoAsync(ReportLogo logo)
        {
            context.ReportLogos.Remove(logo);
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(ReportTemplate template)
        {
            return context.SaveChangesAsync();
        }
    }
}