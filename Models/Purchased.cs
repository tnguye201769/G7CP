﻿using System;
using System.Collections.Generic;

#nullable disable

namespace G7CP.Models
{
    public partial class Purchased
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
