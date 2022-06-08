using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercy.Web.Responses
{
    public class RegistrationResponse : Response
    {
        public RegistrationResponse()
        {
            responseName = "RegistrationResponse";
        }
        public string Token { get; set; }
    }
}
