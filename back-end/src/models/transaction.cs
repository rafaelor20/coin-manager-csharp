using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.src.models

{
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Entity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}