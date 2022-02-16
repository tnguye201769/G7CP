using System;
using System.Collections.Generic;

#nullable disable

namespace G7CP.Models
{
    public partial class Rating
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public short Value { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
