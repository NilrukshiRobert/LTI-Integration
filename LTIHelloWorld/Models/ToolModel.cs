using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtiLibrary.Lti1;

namespace LTIHelloWorld.Models
{
    public class ToolModel
    {
        public string ConsumerSecret { get; set; }
        public LtiRequest LtiRequest { get; set; }
    }
}