using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface ITermRepository
	{
		Task<Term> GetTermByIdAsync(int termId);
		Task AddTermAsync(Term term);
		Task UpdateTermAsync(Term term);
		Task DeleteTermAsync(Term term);
		Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId);
	}
}
