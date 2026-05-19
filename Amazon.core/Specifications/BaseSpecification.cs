using Amazon.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> orderBy { get; set; }
        public Expression<Func<T, object>> orderByDesc { get ; set ; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool isPaginationEnabled { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }


        public void AddOrderBy(Expression<Func<T, object>> expression)
        {
            orderBy= expression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> expression)
        {
            orderByDesc = expression;
        }

        public void applyPagination(int skip,int take)
        {
            isPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
