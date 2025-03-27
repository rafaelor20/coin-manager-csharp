using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.src.models

{
    public class Session
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}