using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstruFindAPI.API.Configuration.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int Expires { get; set; }
        public string Issuer { get; set; }
        public string ValideOn { get; set; }
    }
}
