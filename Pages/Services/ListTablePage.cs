using EDNETLMS.Data;
using EDNETLMS.Models;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class ListTablePage
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public ListTablePage(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public void testing()
		{
			var query = from person in _context.Set<Person>()
						join lead in _context.Set<Lead>()
							on person.PersonID equals lead.LeadID
						select new { person, lead };

			var resultList = query.ToList();

		
		}
	}
}
