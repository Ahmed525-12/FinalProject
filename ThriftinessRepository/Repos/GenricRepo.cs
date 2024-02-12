using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;
using ThriftinessCore.Repos;
using ThriftinessCore.Specfictions;
using ThriftinessRepository.Contexts;
using ThriftinessRepository.Spec;

namespace ThriftinessRepository.Repos
{
	public class GenricRepo<T> : IGenricRepo<T> where T : BaseEntity
	{
		private readonly ThriftinessContext _dbContext;

		public GenricRepo(ThriftinessContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(T item)
		=> await _dbContext.Set<T>().AddAsync(item);

		public void DeleteAsync(T item)
		{
			_dbContext.Remove(item);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecfiction<T> Spec)
		{
			return await GenerateSpec(Spec).ToListAsync();
		}

		public async Task<T?> GetbyIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

		public void Update(T item)
		{
			_dbContext.Update(item);
		}

		private IQueryable<T> GenerateSpec(ISpecfiction<T> Spec)
		{
			return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec).Result;
		}
	}
}