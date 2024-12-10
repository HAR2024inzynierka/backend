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

        public async Task AddTermsForDayAsync(int autoServiceId, DateTime day)
        {
            // Define start and end hours
            var startHour = 8;
            var endHour = 18;

            // Generate terms
            var terms = new List<Term>();
            for (var hour = startHour; hour < endHour; hour++)
            {
                var startDateTime = new DateTime(day.Year, day.Month, day.Day, hour, 0, 0);
                var endDateTime = startDateTime.AddHours(1);

                terms.Add(new Term
                {
                    StartDate = startDateTime,
                    EndDate = endDateTime,
                    Availability = true, // Default availability to true
                    AutoServiceId = autoServiceId
                });
            }

            // Add terms to the repository
            foreach (var term in terms)
            {
                await _termRepository.AddTermAsync(term);
            }
        }
    }
}
