using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;
using ThriftinessCore.Specfictions;

namespace ThriftinessRepository.Spec
{
    public class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static async Task<IQueryable<T>> GetQuery(IQueryable<T> InputQuery, ISpecfiction<T> Spec)
        {
            var Query = InputQuery;

            //.Where(P => P.Id == id)
            if (Spec.Criteria is not null)
                Query = Query.Where(Spec.Criteria);

            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}