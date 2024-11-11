using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
	public class TermRepository : ITermRepository
	{
		private readonly WorkshopDbContext _context;

		public TermRepository(WorkshopDbContext context)
		{
			_context = context;
		}

		public async Task AddTermAsync(Term term)
		{
			await _context.Terms.AddAsync(term);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId)
		{
			return await _context.Terms.Where(t => t.AutoServiceId == autoserviceId).ToListAsync();
		}
	}
}
