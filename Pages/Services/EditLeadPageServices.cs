using EDNETLMS.Data;
using EDNETLMS.Models;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class EditLeadPageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public EditLeadPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public Person readPersonByID(int PersonId)
		{
			Person newPerson = new Person();

			newPerson = _context.Persons.SingleOrDefault(p => p.PersonID == PersonId);

			return newPerson;
		}
	}
}
