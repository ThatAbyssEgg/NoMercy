using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercy.Web.Responses
{
    public class AuthorizationResponse : Response
    {
        public AuthorizationResponse()
        {
            responseName = "AuthorizationResponse";
        }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Class { get; set; }
    }
}
