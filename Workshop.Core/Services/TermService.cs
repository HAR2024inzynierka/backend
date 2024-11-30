using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
	public class TermService : ITermService
	{
		private readonly ITermRepository _termRepository;

		public TermService(ITermRepository termRepository)
		{
			_termRepository = termRepository;
		}

		public async Task<Term> GetTermByIdAsync(int termId)
		{
			var term = await _termRepository.GetTermByIdAsync(termId);

			if (term == null)
			{
				throw new Exception("Term not found");
			}

            return term;
		}
		public async Task AddTermAsync(Term term)
		{
			await _termRepository.AddTermAsync(term);
		}

		public async Task UpdateTermAsync(Term updateTerm)
		{
            var term = await _termRepository.GetTermByIdAsync(updateTerm.Id);
            if (term == null)
            {
                throw new Exception("Term not found");
            }

            term.StartDate = updateTerm.StartDate;
			term.EndDate = updateTerm.EndDate;
			term.Availability = updateTerm.Availability;

            await _termRepository.UpdateTermAsync(term);
        }

		public async Task DeleteTermAsync(int termId)
		{
            var term = await _termRepository.GetTermByIdAsync(termId);
            if (term == null)
            {
                throw new Exception("Term not found");
            }

			await _termRepository.DeleteTermAsync(term);
        }

		public async Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId)
		{
			return await _termRepository.GetTermsByAutoServiceIdAsync(autoserviceId);
		}
	}
}
