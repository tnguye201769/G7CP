using System;
using System.Collections.Generic;

#nullable disable

namespace G7CP.Models
{
    public partial class InvoiceStatus
    {
        public InvoiceStatus()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
