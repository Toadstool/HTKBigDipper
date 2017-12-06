using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace HTK.Bank.Api
{
    public static class Settings
    {
        public static string DATABASE_FILE_PATH = HostingEnvironment.MapPath(@"~/App_Data/HTK.Bank.db");

    }
}