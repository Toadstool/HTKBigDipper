using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTK.Bank.Api.Models
{
    public class TestResult
    {
        public string UserName
        {
            get; set;
        }

        public bool Verified
        {
            get;set;
        }

        public double Score
        {
            get;set;
        }
    }
}