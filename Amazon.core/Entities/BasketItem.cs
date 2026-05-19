using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Entities
{
    public class BasketItem :BaseEntity
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int Quantinty { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
    