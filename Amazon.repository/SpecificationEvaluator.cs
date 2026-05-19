using Amazon.core.Entities;
using Amazon.core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.repository
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            

            if (spec.orderBy is not null)
            {
                query=query.OrderBy(spec.orderBy);
            }
            if (spec.orderByDesc is not null)
            {
                query=query.OrderByDescending(spec.orderByDesc);
            }
            if (spec.isPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (currentQuery, Includes) => (currentQuery.Include(Includes)));
            return query;
        }
    }
}
