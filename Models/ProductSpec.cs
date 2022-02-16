using System;
using System.Collections.Generic;

#nullable disable

namespace G7CP.Models
{
    public partial class ProductSpec
    {
        public ProductSpec()
        {
            ProductSpecDetails = new HashSet<ProductSpecDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductSpecDetail> ProductSpecDetails { get; set; }
    }
}
