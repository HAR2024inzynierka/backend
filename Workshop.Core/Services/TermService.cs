using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Klasa serwisowa dla operacji związanych z terminami.
    /// </summary>
    public class TermService : ITermService
	{
		private readonly ITermRepository _termRepository;

        /// <summary>
        /// Konstruktor serwisu TermService.
        /// </summary>
        /// <param name="termRepository">Repozytorium operujące na danych terminów</param>
        public TermService(ITermRepository termRepository)
		{
			_termRepository = termRepository;
		}

		public async Task<Term> GetTermByIdAsync(int termId)
		{
			var term = await _termRepository.GetTermByIdAsync(termId);

            // Jeżeli termin nie istnieje, rzucamy wyjątek
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

            // Jeżeli termin nie istnieje, rzucamy wyjątek
            if (term == null)
            {
                throw new Exception("Term not found");
            }

            // Aktualizujemy dane terminu
            term.StartDate = updateTerm.StartDate;
			term.EndDate = updateTerm.EndDate;
			term.Availability = updateTerm.Availability;

            await _termRepository.UpdateTermAsync(term);
        }

		public async Task DeleteTermAsync(int termId)
		{
            var term = await _termRepository.GetTermByIdAsync(termId);

            // Jeżeli termin nie istnieje, rzucamy wyjątek
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
