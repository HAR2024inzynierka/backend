using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public async Task AddTermAsync(Term term)
		{
			await _termRepository.AddTermAsync(term);
		}
		public async Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId)
		{
			return await _termRepository.GetTermsByAutoServiceIdAsync(autoserviceId);
		}
	}
}
