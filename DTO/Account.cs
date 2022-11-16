using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Account
    {
        public int accountNumber { get; set; } // primary key
        public string accountNbOwner { get; set; } // foreign key
        public string type { get; set; }
        public float amount { get; set; }
    }
}
