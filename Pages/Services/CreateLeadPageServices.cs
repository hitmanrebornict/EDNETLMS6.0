using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EDNETLMS.Pages.Services
{
    public class CreateLeadPageServices
    {
        private readonly ApplicationDbContext _context;

        public CreateLeadPageServices(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<List<Institution>> ReadInstitutionListAsync()
        {
            return await _context.Institutions.ToListAsync();
        }


    }
}
