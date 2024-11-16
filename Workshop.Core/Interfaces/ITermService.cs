using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface ITermService
	{
		Task<Term> GetTermByIdAsync(int termId);
		Task AddTermAsync(Term term);
		Task UpdateTermAsync(Term term);
		Task DeleteTermAsync(int termId);
		Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId);
	}
}
