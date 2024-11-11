using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface ITermRepository
	{
		Task AddTermAsync(Term term);
		Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId);
	}
}
