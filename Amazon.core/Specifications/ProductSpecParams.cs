using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Specifications
{
    public class ProductSpecParams
    {
        public const int maxPageSize = 18;

        private int pageSize=5;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value >maxPageSize?maxPageSize:value; }
        }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public string? Sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }
    }
}
