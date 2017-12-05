using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTK.Bank.Api.Models
{
    public class Batch
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public Movement[] Movements { get; set; }
    }
}