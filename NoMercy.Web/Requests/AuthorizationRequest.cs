using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercy.Web.Requests
{
    public class AuthorizationRequest : Request
    {
        public AuthorizationRequest()
        {
            requestName = "AuthorizationRequest";
        }
        public string Token { get; set; }
    }
}
