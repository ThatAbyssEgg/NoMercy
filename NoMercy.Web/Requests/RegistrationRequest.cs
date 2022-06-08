using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercy.Web.Requests
{
    public class RegistrationRequest : Request
    {
        public RegistrationRequest()
        {
            requestName = "RegistrationRequest";
        }
        public string Name { get; set; }
        public string Class { get; set; }
    }
}
