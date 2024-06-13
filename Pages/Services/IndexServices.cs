using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class IndexServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public IndexServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public async Task<List<LeadStatusCount>> ReadLeadStatusCount()
		{
			List<LeadStatusCount> leadStatusCounts = await _context.Leads
													.Where(l => l.PIC == "Phoebe")
													.GroupBy(l => l.LeadStatus)
													.Select(g => new LeadStatusCount
													{
														LeadStatus = g.Key,
														Count = g.Count()
													})
													.ToListAsync();

			return leadStatusCounts;
		}

		public async Task<List<LeadStatusCount>> ReadLeadsDoneStatusCount()
		{
			List<LeadStatusCount> leadStatusCounts = await _context.Leads
														.Where(lead => lead.PIC == "Phoebe")
														.Join(_context.LeadCatchUpStatuses,
															  lead => lead.LeadID,
															  status => status.LeadID,
															  (lead, status) => new { status.Done })
														.GroupBy(s => s.Done)
														.Select(g => new LeadStatusCount
														{
															LeadDone = g.Key ? "Done" : "Follow Up",
															LeadDoneCount = g.Count()
														})
														.ToListAsync();

			return leadStatusCounts;
		}
	}
}