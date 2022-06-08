using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercy.Web.Requests
{
    public class Request
    {
        protected string requestName;
        public string RequestName { get => requestName; set => requestName = value; }
    }
}
